using AutoMapper;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.FileQuestion;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain.Shared.Editor;
using Bg.EduSocial.Domain.Shared.FileQuestion;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;

namespace Bg.EduSocial.Application
{
    public class FileQuestionService: IFileQuestionService
    {
        private XslCompiledTransform _xslTransformMathML;
        private XslCompiledTransform _xslTransformLatex;
        private MainDocumentPart mainPart;
        private List<QuestionDto> questions;
        private string _patternQuestion;
        private string _patternOption;
        private string _patternResult;
        private ItemProcessing itemProcessing;
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public FileQuestionService()
        {
            LoadTransformFiles();
        }
        private void InitData(SplitQuestion splitQuestion)
        {
            questions = new List<QuestionDto>();
            _patternQuestion = splitQuestion.RegexQuestion;
            _patternOption = splitQuestion.RegexAnswer;
            _patternResult = splitQuestion.RegexResult;
        }
        private void LoadTransformFiles()
        {
            _xslTransformMathML = new XslCompiledTransform();
            _xslTransformLatex = new XslCompiledTransform();
            var targetDirectoryMathML = System.IO.Path.Combine("..", "Bg.EduSocial.Helper", "Libraries", "omml2mml.xsl"); ;
            _xslTransformMathML.Load(targetDirectoryMathML);
            var targetDirectoryLatex = System.IO.Path.Combine("..", "Bg.EduSocial.Helper", "Libraries", "mmltex.xsl");
            XsltSettings xsltt = new XsltSettings(true, true);
            // Configure xml writer to omit xml declaration.
            var xmlResolver = new XmlUrlResolver();
            // The mmtex.xsl file is to convert to Tex 
            _xslTransformLatex.Load(targetDirectoryLatex, xsltt, xmlResolver);
        }
        /// <summary>
        /// Lấy dữ liệu latex từ file
        /// </summary>
        /// <param name="file">File to be processed</param>
        /// <param name="questionIndicator">Indicator that splits the text into questions</param>
        /// <returns>List of objects representing data from the file</returns>
        /// Created By: NVLong 4/5/2024
        public List<QuestionDto> GetQuestionFromFile(IFormFile file, SplitQuestion splitQuestion)
        {
            if (file == null || splitQuestion ==null) return default;
            InitData(splitQuestion);
            using var stream = file.OpenReadStream();
            using var doc = WordprocessingDocument.Open(stream, false);
            if (doc?.MainDocumentPart?.Document?.Body == null) return default;
            foreach (var element in doc.MainDocumentPart.Document.Body.Elements())
            {
                if (element is DocumentFormat.OpenXml.Wordprocessing.Paragraph)
                {
                    HandleParagraphElement((DocumentFormat.OpenXml.Wordprocessing.Paragraph)element);
                }
                else if (element is DocumentFormat.OpenXml.Math.Paragraph)
                {
                    HandleMathParagraphElement((DocumentFormat.OpenXml.Math.Paragraph)element);
                }
                else if (element is DocumentFormat.OpenXml.Wordprocessing.Table)
                {
                    HandleTableElement((DocumentFormat.OpenXml.Wordprocessing.Table)element);
                }
            }
            return this.questions;
        }


        private static DocumentFormat.OpenXml.Wordprocessing.Paragraph CreateParagraph(string text)
        {
            var paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
            var run = new DocumentFormat.OpenXml.Wordprocessing.Run();
            var textElement = new DocumentFormat.OpenXml.Wordprocessing.Text(text);
            run.Append(textElement);
            paragraph.Append(run);
            return paragraph;
        }
        private void HandleParagraphElement(DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph)
        {
            if (paragraph == null) return;
            foreach (var childElement in paragraph.ChildElements)
            {
                switch (childElement)
                {
                    case DocumentFormat.OpenXml.Wordprocessing.Run run:
                        HandleRunElement(run);
                        break;
                    case DocumentFormat.OpenXml.Math.OfficeMath officeMath:
                        var latexMath = ConvertXML2Latex(officeMath.OuterXml);
                        if (!String.IsNullOrEmpty(latexMath))
                        {
                            var dataItem = BuildItem(EditorActionType.INSERT, EditorItemType.FOMUALA, latexMath);
                            AddContentToLastItem(dataItem);
                        }
                        break;
                }
            }
        }

        private void HandleRunElement(DocumentFormat.OpenXml.Wordprocessing.Run runElement)
        {
            if (runElement == null) return;

            foreach (var item in runElement.ChildElements)
            {
                switch (item)
                {
                    case DocumentFormat.OpenXml.Wordprocessing.Text textItem:
                        ProcessTextItem(textItem.InnerText);
                        break;

                    case DocumentFormat.OpenXml.Wordprocessing.Drawing drawingItem:
                        ProcessDrawingItem(drawingItem);
                        break;
                }
            }
        }

        private void ProcessTextItem(string dataTextElement)
        {
            var questionsMatches = Regex.Matches(dataTextElement, _patternQuestion);

            if (questionsMatches?.Count > 0)
            {
                ProcessTextBeforeNewQuestion(dataTextElement, questionsMatches[0].Index);
                for (var index = 0; index <  questionsMatches.Count; index++)
                {
                    var textQuestion = String.Empty;
                    if (index < questionsMatches.Count -1)
                    {
                        textQuestion = dataTextElement.Substring(questionsMatches[index].Index, questionsMatches[index+1].Length);
                    } else
                    {
                        textQuestion = dataTextElement.Substring(questionsMatches[index].Index, dataTextElement.Length);
                    }
                    ProcessQuestionMatch(textQuestion, questionsMatches[index]);
                }
            } else
            {
                ProcessTextBeforeNewQuestion(dataTextElement, dataTextElement.Length);
            }
        }

        private void ProcessTextBeforeNewQuestion(string textElement, int endIndex)
        {
            var lastQuestion = questions.LastOrDefault();
            if (lastQuestion == null) return;

            var beforeQuestionText = textElement.Substring(0, endIndex);
            var optionsMatch = Regex.Matches(beforeQuestionText, _patternOption);
            var resultsMatch = Regex.Matches(beforeQuestionText, _patternResult);


            int endIndexItemBefore = GetIndexEndItemBefore(beforeQuestionText, optionsMatch, resultsMatch);
            var textItem = beforeQuestionText.Substring(0, endIndexItemBefore);
            if (!string.IsNullOrEmpty(textItem))
            {
                var dataItem = BuildItem(EditorActionType.INSERT, EditorItemType.TEXT, textItem);
                AddContentToLastItem(dataItem);
            }

            HandleOptionsMatches(beforeQuestionText, optionsMatch, resultsMatch);
            HandleResultsMatches(beforeQuestionText, resultsMatch, resultsMatch);
        }

        private void ProcessQuestionMatch(string textQuestion, Match questionMatch)
        {
            var newQuestion = new QuestionDto
            {
                type = Domain.Shared.Questions.QuestionType.SingleChoice,
                question_id = Guid.NewGuid(),
                options = new List<OptionDto>(),
                object_content = new List<object>()
            };
            questions.Add(newQuestion);
            itemProcessing = ItemProcessing.Question;
            var optionsMatch = Regex.Matches(textQuestion, _patternOption);
            var resultsMatch = Regex.Matches(textQuestion, _patternResult);
            var questionContentEndIndex = GetIndexEndItemCurElement(textQuestion, optionsMatch, resultsMatch);
            string questionText = textQuestion.Substring(questionMatch.Index, questionContentEndIndex);
            newQuestion.object_content.Add(BuildItem(EditorActionType.INSERT, EditorItemType.TEXT, questionText));
            HandleOptionsMatches(textQuestion, optionsMatch, resultsMatch);
            HandleResultsMatches(textQuestion, resultsMatch, resultsMatch);
        }
        private void HandleOptionsMatches(string textQuestionElement, MatchCollection optionsMatch, MatchCollection resultsMatch)
        {
            if (optionsMatch?.Count > 0)
            {
                itemProcessing = ItemProcessing.Option;
                var indexEndOption = GetIndexEndItemCurElement(textQuestionElement, optionsMatch, resultsMatch);
                ProcessOptionsMatches(textQuestionElement, optionsMatch, indexEndOption);
            }
        }
        private void ProcessOptionsMatches(string textQuestion, MatchCollection optionsMatch, int indexEndOption)
        {
            var question = questions.LastOrDefault();
            if (question != null && optionsMatch?.Count > 0)
            {
                itemProcessing = ItemProcessing.Option;
                for (var index = 0; index < optionsMatch.Count; index++)
                {
                    var textOption = String.Empty;
                    if (index < optionsMatch.Count - 1)
                    {
                        textOption = textQuestion.Substring(optionsMatch[index].Index, optionsMatch[index + 1].Length);
                    }
                    else
                    {
                        textOption = textQuestion.Substring(optionsMatch[index].Index, indexEndOption);
                    }
                    var dataOption = new List<object> { BuildItem(EditorActionType.INSERT, EditorItemType.TEXT, textOption) };
                    var newOption = new OptionDto
                    {
                        option_question_id = Guid.NewGuid(),
                        question_id = question.question_id,
                        object_content = dataOption
                    };
                    question.options.Add(newOption);
                }
            }
        }
        private void HandleResultsMatches(string textQuestionElement, MatchCollection optionsMatch, MatchCollection resultsMatch)
        {
            if (resultsMatch?.Count > 0)
            {
                itemProcessing = ItemProcessing.Result;
                var indexEndResult = GetIndexEndItemCurElement(textQuestionElement, optionsMatch, resultsMatch);
                ProcessResultsMatches(textQuestionElement, resultsMatch, indexEndResult);
            }
        }
        private void ProcessResultsMatches(string text, MatchCollection resultsMatch, int indexEndResult)
        {
            foreach (Match result in resultsMatch)
            {
                // Xử lý kết quả tại đây (đang để trống vì cần làm thêm phần này)
                itemProcessing = ItemProcessing.Result;
            }
        }

        private int GetIndexEndItemBefore(string textQuestionElement, MatchCollection optionsMatch, MatchCollection resultsMatch)
        {
            if (optionsMatch.Count > 0 && resultsMatch.Count > 0)
            {
                 return Math.Min(optionsMatch[0].Index, resultsMatch[0].Index);
            }
            if (optionsMatch.Count > 0) return optionsMatch[0].Index;
            if (resultsMatch.Count > 0) return resultsMatch[0].Index;
            return textQuestionElement.Length;
        }

        private int GetIndexEndItemCurElement(string textQuestionElement, MatchCollection optionsMatch, MatchCollection resultsMatch)
        {
            int endIndex = textQuestionElement.Length;

            if (optionsMatch.Count > 0 && resultsMatch.Count > 0)
            {
                int firstOptionIndex = optionsMatch[0].Index;
                int firstResultIndex = resultsMatch[0].Index;

                endIndex = itemProcessing switch
                {
                    ItemProcessing.Question => Math.Min(firstOptionIndex, firstResultIndex),
                    ItemProcessing.Option => firstResultIndex > firstOptionIndex ? firstResultIndex : textQuestionElement.Length,
                    ItemProcessing.Result => firstResultIndex > firstOptionIndex ? textQuestionElement.Length : firstResultIndex,
                    _ => textQuestionElement.Length
                };
            }
            else if (optionsMatch.Count > 0)
            {
                endIndex = itemProcessing switch
                {
                    ItemProcessing.Question => optionsMatch[0].Index,
                    ItemProcessing.Option => textQuestionElement.Length,
                    ItemProcessing.Result => optionsMatch[0].Index,
                    _ => textQuestionElement.Length
                };
            }
            else if (resultsMatch.Count > 0)
            {
                endIndex = itemProcessing switch
                {
                    ItemProcessing.Question => resultsMatch[0].Index,
                    ItemProcessing.Option => resultsMatch[0].Index,
                    ItemProcessing.Result => textQuestionElement.Length,
                    _ => textQuestionElement.Length
                };
            }

            return endIndex;
        }

        private int? GetIndexEndQuestion(MatchCollection optionsMatch, MatchCollection resultsMatch)
        {

            if (optionsMatch?.Count > 0 && resultsMatch?.Count > 0)
            {
                 return Math.Min(optionsMatch[0].Index, resultsMatch[0].Index);
            }
            if (optionsMatch?.Count > 0) return optionsMatch[0].Index;
            if (resultsMatch?.Count > 0) return resultsMatch[0].Index;
            return null;
        }

        private void AddContentToLastItem(object dataItem)
        {
            var lastQuestion = questions.LastOrDefault();
            if (lastQuestion != null)
            {
                switch (itemProcessing)
                {
                    case ItemProcessing.Result:
                        // Todo
                        break;
                    case ItemProcessing.Question:
                        if (lastQuestion.object_content == null) lastQuestion.object_content = new List<object>();
                        lastQuestion.object_content.Add(dataItem);
                        break;
                    case ItemProcessing.Option:
                        var lastOption = lastQuestion.options.LastOrDefault();
                        if (lastOption !=null)
                        {
                            if (lastOption.object_content ==null) lastOption.object_content = new List<object>();
                            lastOption?.object_content.Add(dataItem);
                        }
                        break;
                }
            }
        }
        private void AddContentToLastOption(QuestionDto question, object dataItem)
        {
            var lastOption = question.options.LastOrDefault();
            lastOption?.object_content?.Add(dataItem);
        }

        private void ProcessDrawingItem(DocumentFormat.OpenXml.Wordprocessing.Drawing drawingItem)
        {
            var url = HandlePicture(mainPart, drawingItem);
            BuildItem(EditorActionType.INSERT, EditorItemType.IMAGE, url);
        }

        private void HandleMathParagraphElement(DocumentFormat.OpenXml.Math.Paragraph mathParagraph)
        {
            if (mathParagraph == null) return;
            var listChild = mathParagraph.ChildElements.ToList();
            listChild.ForEach(childElement =>
            {
                if (childElement is DocumentFormat.OpenXml.Math.OfficeMath)
                {
                    var latexMath = ConvertXML2Latex(childElement.OuterXml);
                    var itemMath = BuildItem(EditorActionType.INSERT, EditorItemType.FOMUALA, latexMath);
                    AddContentToLastItem(itemMath);
                }
            });
        }

        public void HandleTableElement(DocumentFormat.OpenXml.Wordprocessing.Table tableElement)
        {
            if (tableElement == null) return;
        }
        /// <summary>
        /// Convert dữ liệu từ XML sang Latex
        /// </summary>
        /// <param name="wordDocXml"></param>
        /// <returns></returns>
        /// Created By: NVLong 4/5/2024
        public string ConvertXML2Latex(string wordDocXml)
        {
            var officeML = string.Empty;
            officeML = ConvertXML2MML(wordDocXml);
            var officeLatex = ConvertMML2Latex(officeML);
            return officeLatex;
        }
        /// <summary>
        /// Convert XML sang MathML
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        /// Created By: NVLong 4/5/2024
        public string ConvertXML2MML(string xmlStr)
        {
            var officeML = string.Empty;
            using (StringReader tr = new StringReader(xmlStr))
            {
                // Load the xml of your main document part.
                using (XmlReader reader = XmlReader.Create(tr))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlWriterSettings settingsMathML = _xslTransformMathML.OutputSettings.Clone();
                        // Configure xml writer to omit xml declaration.
                        settingsMathML.ConformanceLevel = ConformanceLevel.Fragment;
                        settingsMathML.OmitXmlDeclaration = true;
                        XmlWriter xwMathML = XmlWriter.Create(ms, settingsMathML);

                        // Transform our OfficeMathML to MathML.
                        _xslTransformMathML.Transform(reader, xwMathML);
                        ms.Seek(0, SeekOrigin.Begin);

                        using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                        {
                            officeML = sr.ReadToEnd();
                        }
                    }
                }
            }
            return officeML;
        }
        /// <summary>
        /// Convert MathML sang Latex
        /// </summary>
        /// <param name="officeML"></param>
        /// <returns></returns>
        /// Created By: NVLong 4/5/2024
        public string ConvertMML2Latex(string officeML)
        {
            string officeLatex = String.Empty;
            using (StringReader tr = new StringReader(officeML))
            {
                // Load the xml of your main document part.
                using (XmlReader reader = XmlReader.Create(tr))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlWriterSettings settingsLatex = this._xslTransformLatex.OutputSettings.Clone();
                        // Configure xml writer to omit xml declaration.
                        settingsLatex.ConformanceLevel = ConformanceLevel.Fragment;
                        settingsLatex.OmitXmlDeclaration = true;
                        XmlWriter xwLatex = XmlWriter.Create(ms, settingsLatex);
                        var xmlResolver = new XmlUrlResolver();
                        // The mmtex.xsl file is to convert to Tex 
                        // Transform our OfficeMathML to MathML.
                        this._xslTransformLatex.Transform(reader, xwLatex);
                        ms.Seek(0, SeekOrigin.Begin);

                        using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                        {
                            officeLatex = sr.ReadToEnd();
                            if (officeLatex.StartsWith("$") && officeLatex.EndsWith("$"))
                            {
                                officeLatex = officeLatex.Trim('$');
                            }
                        }
                    }
                }
            }
            return officeLatex;
        }
        /// <summary>
        /// Xử lý mathml thay thế tag
        /// </summary>
        /// <param name="mathMLOffice"></param>
        /// <returns></returns>
        /// Created By: NVLong 4/5/2024
        public static string HandleMathML(string mathMLOffice)
        {
            string searchString = "<mml:";
            string replaceString = "<";
            StringBuilder sb = new StringBuilder(mathMLOffice);
            sb.Replace(searchString, replaceString);
            string modifiedString = sb.ToString();
            searchString = "</mml:";
            replaceString = "</";
            sb = new StringBuilder(modifiedString);
            sb.Replace(searchString, replaceString);
            modifiedString = sb.ToString();
            searchString = "xmlns:mml";
            replaceString = "xmlns";
            sb = new StringBuilder(modifiedString);
            sb.Replace(searchString, replaceString);
            modifiedString = sb.ToString();
            return modifiedString;
        }
        /// <summary>
        /// Xử lý dữ liệu ảnh
        /// </summary>
        /// <param name="mainPart"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// Created By: NVLong 4/5/2024
        private static string HandlePicture(MainDocumentPart mainPart, DocumentFormat.OpenXml.Wordprocessing.Drawing item)
        {
            string elementPicture = string.Empty;
            var currentDirectory = Directory.GetCurrentDirectory();
            var blip = item.Descendants<Blip>().FirstOrDefault();
            if (blip != null)
            {
                string imagePartId = blip.Embed.Value;
                var imagePart = (ImagePart)mainPart.GetPartById(imagePartId);
                if (imagePart != null)
                {
                    var partUri = imagePart.Uri.ToString();
                    string extension = System.IO.Path.GetExtension(partUri);
                    var pictureFileName = Guid.NewGuid().ToString() + extension;
                    var imagePath = System.IO.Path.Combine(currentDirectory, "TestSFolder", pictureFileName);
                    using (var imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        imagePart.GetStream().CopyTo(imageStream);
                        elementPicture = $"<picture><img src=\"https://localhost:7195/api/Picture/{pictureFileName}\" alt=\"Math Equation\"></picture>";
                    }

                }
            }
            return elementPicture;
        }
        /// <summary>
        /// Build dữ liệu để client có thể hiểu
        /// </summary>
        /// <param name="actionType"></param> Hành động 
        /// <param name="itemType"></param> Kiểu dữ liệu
        /// <param name="data"></param> Dữ liệu cần build
        /// <param name="attributes"></param> Thuộc tính của dữ liệu đó
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// Created By: NVLong 4/5/2024
        private object BuildItem(EditorActionType actionType, EditorItemType itemType, string data, object attributes = null)
        {
            if (actionType == EditorActionType.NONE)
            {
                if (itemType == EditorItemType.TEXT)
                {
                    return new
                    {
                        text = data
                    };
                }
                else if (itemType == EditorItemType.IMAGE)
                {
                    return new
                    {
                        image = data
                    };
                }
                else if (itemType == EditorItemType.FOMUALA)
                {
                    return new
                    {
                        formula = data
                    };
                }
            }
            else if (actionType == EditorActionType.INSERT)
            {
                if (itemType == EditorItemType.TEXT)
                {
                    return new
                    {
                        insert = data
                    };
                }
                else if (itemType == EditorItemType.IMAGE)
                {
                    return new
                    {
                        insert = new
                        {
                            image = data
                        }
                    };
                }
                else if (itemType == EditorItemType.FOMUALA)
                {
                    return new
                    {
                        insert = new
                        {
                            formula = data
                        }
                    };
                }
            }
            else if (actionType == EditorActionType.DELETE)
            {

            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException($"'{nameof(data)}' cannot be null or empty.", nameof(data));
            }

            if (attributes is null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            return new object();
        }
    }
}
