using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.ModelState;
using Bg.EduSocial.Domain.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bg.EduSocial.Application
{
    public class TestService : WriteService<ITestRepository, TestEntity, TestDto, TestEditDto>, ITestService
    {
        public TestService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<QuestionDto>> GetQuestionOfTest(Guid testId)
        {
            var questions = await _repo.GetQuestionOfTest(testId);
            if (!(questions?.Count > 0)) return default;
            var optionService = _serviceProvider.GetRequiredService<IOptionService>();
            var questionIds = questions.Select(question => question.question_id).ToList();
            var filterOptionOfTest = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.Equal,
                Value = questionIds
            };
            var options = await optionService.FilterAsync(new List<FilterCondition> { filterOptionOfTest });
            var questionsReturn = _mapper.Map<List<QuestionDto>>(questions);
            MapOptionsToQuestion(questionsReturn, options);
            return questionsReturn;
        }

        public async Task<List<QuestionDto>> GetQuestionOfTestEditAsync(Guid testId)
        {
            var questions = await _repo.GetQuestionOfTest(testId);
            if (!(questions?.Count > 0)) return default;
            var optionService = _serviceProvider.GetRequiredService<IOptionService>();
            var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();

            var questionIds = questions.Select(question => question.question_id).ToList();
            var filterOptionOfTest = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.Equal,
                Value = questionIds
            };
            var options = await optionService.FilterAsync(new List<FilterCondition> { filterOptionOfTest });
            var filterResultOfTest = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.Equal,
                Value = questionIds
            };
            var results = await resultQuestionService.FilterAsync(new List<FilterCondition> { filterResultOfTest });
            var questionsReturn = _mapper.Map<List<QuestionDto>>(questions);
            MapOptionsToQuestion(questionsReturn, options);
            MapResultsToQuestion(questionsReturn, results);
            return questionsReturn;
        }

        private void MapOptionsToQuestion(List<QuestionDto> questions, List<OptionDto> options)
        {
            if (questions?.Count > 0 && options?.Count > 0) { 
                // Tạo một dictionary để nhóm các options theo QuestionId
                var optionsByQuestionId = options.GroupBy(o => o.question_id)
                                                 .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var question in questions)
                {
                    // Lấy ra danh sách options từ dictionary bằng QuestionId của câu hỏi
                    if (optionsByQuestionId.TryGetValue(question.question_id, out var relevantOptions))
                    {
                        question.options = relevantOptions;
                    }
                }
            }
        }


        public void MapResultsToQuestion(List<QuestionDto> questions, List<ResultQuestionDto> results)
        {
            if (questions.Count > 0 && results?.Count > 0) {
                // Tạo một dictionary để nhóm các options theo QuestionId
                var resultsByQuestionId = results.GroupBy(o => o.question_id)
                                                 .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var question in questions)
                {
                    // Lấy ra danh sách options từ dictionary bằng QuestionId của câu hỏi
                    if (resultsByQuestionId.TryGetValue(question.question_id, out var relevantResults))
                    {
                        question.results = relevantResults;
                    }
                }
            }
        }

        public override async Task BeforeCommitAsync(TestEditDto test)
        {
            if (test == null || test.questions?.Count == 0) return;

            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var optionService = _serviceProvider.GetRequiredService<IOptionService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();
            var questionsHandle = test.questions?
                .Where(question => question.State == ModelState.Insert ||
                                   question.State == ModelState.Update ||
                                   question.State == ModelState.Delete)
                .ToList();

            var optionsHandle = test.questions?
                .SelectMany(question => question.options)
                .Where(option => option.State == ModelState.Insert ||
                                option.State == ModelState.Update ||
                                option.State == ModelState.Delete)
                .ToList();

            var tasks = new List<Task>();

            if (questionsHandle?.Count > 0)
            {
                tasks.Add(questionService.SubmitManyAsync(questionsHandle));
            }
            if (questionsHandle?.Count > 0)
            {
                tasks.Add(questionTestService.SubmitManyAsync(
                    questionsHandle.Select(question => new QuestionTestEditDto
                    {
                        question_test_id = Guid.NewGuid(),
                        test_id = test.test_id,
                        question_id = question.question_id
                    }).ToList()
                    ));
            }
            if (optionsHandle?.Count > 0)
            {
                tasks.Add(optionService.SubmitManyAsync(optionsHandle));
            }

            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks);
            }
        }

        public Task<List<QuestionDto>> ReadQuestionFromFile(IFormFile file, string regexStr)
        {
            //EditorFunction.GetLatexFromFile(file, regexStr);
            return default;
        }

        public async Task<TestDto> GetTestDetail(Guid testId)
        {
            // Khởi tạo các Task mà không chờ đợi chúng hoàn thành ngay lập tức
            var test = await this.GetById<TestDto>(testId);
            var questionOfTest = await this.GetQuestionOfTest(testId);

            // Chờ đợi cả hai Task hoàn thành

            // Lấy kết quả từ các Task
            if (test != null)
            {
                test.questions = questionOfTest;
            }
            return test;
        }

        public async Task<bool> MarkTest(Guid testId)
        {
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();

            // Lấy thông tin Test dựa trên testId
            var testTask = this.GetById<TestDto>(testId);

            // Thiết lập các điều kiện lọc
            var filterCondition = new FilterCondition
            {
                Field = "test_id",
                Operator = FilterOperator.Equal,
                Value = testId
            };

            // Gọi các API một cách song song
            var questionsTestTask = questionTestService.FilterAsync(new List<FilterCondition> { filterCondition });
            var examsTask = this.FilterAsync<ExamEditDto>(new List<FilterCondition> { filterCondition });

            // Đợi tất cả các Task hoàn thành
            await Task.WhenAll(testTask, questionsTestTask, examsTask);

            // Lấy kết quả từ các Task
            var test = await testTask;
            var questionsTest = await questionsTestTask;
            var exams = await examsTask;

            // Kiểm tra điều kiện trả về false sớm
            if (test == null || questionsTest == null || questionsTest.Count == 0 || exams == null || exams.Count == 0)
                return false;

            // Xử lý tiếp theo
            var questionIds = questionsTest.Select(q => q.question_id).ToList();
            var filterQuestions = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.In,
                Value = questionIds
            };

            // Gọi song song các truy vấn lấy thông tin chi tiết các câu hỏi và kết quả
            var questionsTask = questionService.FilterAsync(new List<FilterCondition> { filterQuestions });
            var resultsTask = resultQuestionService.FilterAsync(new List<FilterCondition> { filterQuestions });
            await Task.WhenAll(questionsTask, resultsTask);

            // Lấy kết quả từ các Task
            var questions = await questionsTask;
            var results = await resultsTask;

            // Tiếp tục xử lý logic dựa trên kết quả trên
            MapResultsToQuestion(questions, results);
            test.questions = questions;
            var examIds = exams.Select(e => e.exam_id).ToList();
            var filterAnswer = new FilterCondition
            {
                Field = "exam_id",
                Operator = FilterOperator.In,
                Value = examIds
            };
            var answers = await answerService.FilterAsync<AnswerEditDto>(new List<FilterCondition> { filterAnswer });
            if (answers?.Count == 0) return true;
            MapAnswersToExam(answers, exams);
            for (var index =0; index < exams.Count; index++)
            {
                examService.MarkExam(exams[index], test);
            }

            return true;
        }

        private void MapAnswersToExam(List<AnswerEditDto> answers, List<ExamEditDto> exams)
        {
            if (answers.Count > 0 && exams?.Count > 0)
            {
                // Tạo một dictionary để nhóm các options theo QuestionId
                var answerByExamId = answers.GroupBy(o => o.exam_id)
                                                 .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var exam in exams)
                {
                    // Lấy ra danh sách options từ dictionary bằng QuestionId của câu hỏi
                    if (answerByExamId.TryGetValue(exam.exam_id, out var answerByExam))
                    {
                        exam.answers = answerByExam;
                    }
                }
            }
        }
    }
}
