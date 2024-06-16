using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Http;
using Bg.EduSocial.Domain.Shared.Editor;
using static System.Net.Mime.MediaTypeNames;
using DocumentFormat.OpenXml.VariantTypes;
using Bg.EduSocial.Domain.Shared.Constants;

namespace Bg.EduSocial.Helper.Commons
{
    public static class EditorFunction
    {
        private static XslCompiledTransform _xslTransformMathML;
        private static XslCompiledTransform _xslTransformLatex;
        public static void Initialize()
        {
            // var currentDirectory = Directory.GetCurrentDirectory();
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
        /// <param name="file"></param>
        /// <returns></returns>
        /// Created By: NVLong 4/5/2024
        public static List<object> GetLatexFromFile(IFormFile file)
        {
            if (file is null) return default;
            string fullResult = string.Empty;
            var data = new List<object>();
            using (var stream = file.OpenReadStream())
            using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, false))
            {
                if (doc != null)
                {
                    MainDocumentPart mainPart = doc.MainDocumentPart;
                    if (mainPart != null)
                    {
                        var body = mainPart.Document.Body;
                        if (body == null) return default;
                        foreach (var element in body.Elements())
                        {
                            var items = GetDataFromGraph(element, mainPart);
                            data.AddRange(items);
                        }
                    }
                }
            }
            return data;
        }
        /// <summary>
        /// Lấy dữ liệu từ element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="mainPart"></param>
        /// <returns></returns>
        /// Created By: NVLong 4/5/2024
        public static List<object> GetDataFromGraph(DocumentFormat.OpenXml.OpenXmlElement element, MainDocumentPart mainPart)
        {
            var data = new List<object>();
            var listChild = element.ChildElements.ToList();
            listChild.ForEach(childElement =>
            {
                if (childElement is DocumentFormat.OpenXml.Math.OfficeMath)
                {
                   var latexMath = ConvertXML2Latex(childElement.OuterXml);
                   var itemMath = BuildItem(EditorActionType.INSERT, EditorItemType.FOMUALA, latexMath);
                   data.Add(itemMath);
                }
                else if (childElement is DocumentFormat.OpenXml.Math.Paragraph)
                {   var latexes = GetDataFromGraph(childElement, mainPart);
                    if (latexes != null)
                    {
                        data.AddRange(latexes);
                    }
                }
                else
                {
                    if (childElement is DocumentFormat.OpenXml.Wordprocessing.Run)
                    {
                        var listItem = childElement.ChildElements.ToList();
                        listItem.ForEach(item =>
                        {
                            if (item is DocumentFormat.OpenXml.Wordprocessing.Drawing)
                            {
                                var url = HandlePicture(mainPart, (DocumentFormat.OpenXml.Wordprocessing.Drawing)item);
                                var picture = BuildItem(EditorActionType.INSERT, EditorItemType.IMAGE, url);
                                data.Add(picture);
                            }
                            else if (childElement is DocumentFormat.OpenXml.Wordprocessing.Paragraph)
                            {
                                var items = GetDataFromGraph(childElement, mainPart);
                                data.AddRange(items);
                            }
                            else
                            {
                                var dataText = BuildItem(EditorActionType.NONE, EditorItemType.TEXT, item.InnerText);
                                data.Add(dataText);
                            }
                        });
                    }
                    else if (childElement is DocumentFormat.OpenXml.Wordprocessing.Paragraph)
                    {
                        var items = GetDataFromGraph(childElement, mainPart);
                        data.AddRange(items);
                    }
                }
            });
            return data;
        }
        /// <summary>
        /// Convert dữ liệu từ XML sang Latex
        /// </summary>
        /// <param name="wordDocXml"></param>
        /// <returns></returns>
        /// Created By: NVLong 4/5/2024
        public static string ConvertXML2Latex(string wordDocXml)
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
        public static string ConvertXML2MML(string xmlStr)
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
        public static string ConvertMML2Latex(string officeML)
        {
            string officeLatex = String.Empty;
            using (StringReader tr = new StringReader(officeML))
            {
                // Load the xml of your main document part.
                using (XmlReader reader = XmlReader.Create(tr))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlWriterSettings settingsLatex = _xslTransformLatex.OutputSettings.Clone();
                        // Configure xml writer to omit xml declaration.
                        settingsLatex.ConformanceLevel = ConformanceLevel.Fragment;
                        settingsLatex.OmitXmlDeclaration = true;
                        XmlWriter xwLatex = XmlWriter.Create(ms, settingsLatex);
                        var xmlResolver = new XmlUrlResolver();
                        // The mmtex.xsl file is to convert to Tex 
                        // Transform our OfficeMathML to MathML.
                        _xslTransformLatex.Transform(reader, xwLatex);
                        ms.Seek(0, SeekOrigin.Begin);

                        using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                        {
                            officeLatex = sr.ReadToEnd();
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
        public static string HandlePicture(MainDocumentPart mainPart, DocumentFormat.OpenXml.Wordprocessing.Drawing item)
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
        public static object BuildItem(EditorActionType actionType, EditorItemType itemType, string data, object attributes = null)
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
                            formuala = data
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
