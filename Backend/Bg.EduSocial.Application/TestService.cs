using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared;
using Bg.EduSocial.Domain.Shared.ModelState;
using Bg.EduSocial.Domain.Shared.Roles;
using Bg.EduSocial.Domain.Tests;
using DocumentFormat.OpenXml.VariantTypes;
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
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();

            var filterQuestionOfTest = new FilterCondition
            {
                Field = "test_id",
                Operator = FilterOperator.Equal,
                Value = testId
            };
            var questionsTest = await questionTestService.FilterAsync(new List<FilterCondition> { filterQuestionOfTest });
            if (questionsTest?.Count > 0)
            {
                var filterQuestion = new List<FilterCondition>
                {
                    new FilterCondition
                    {
                        Field = "question_id",
                        Operator = FilterOperator.In,
                        Value = questionsTest.Select(q => q.question_id).ToList()
                    }
                };
                var questions = await questionService.FilterAsync(filterQuestion);
                if (!(questions?.Count > 0)) return default;
                var optionService = _serviceProvider.GetRequiredService<IOptionService>();

                var questionIds = questions.Select(question => question.question_id).ToList();
                var filterOptionOfTest = new FilterCondition
                {
                    Field = "question_id",
                    Operator = FilterOperator.In,
                    Value = questionIds
                };
                var options = await optionService.FilterAsync(new List<FilterCondition> { filterOptionOfTest });
                var questionsReturn = _mapper.Map<List<QuestionDto>>(questions);
                questionService.MapOptionsToQuestion(questionsReturn, options);
                return questionsReturn;
            }
            return default;
        }

        public async Task<List<QuestionDto>> GetQuestionOfTestEditAsync(Guid testId)
        {
            var questions = await _repo.GetQuestionOfTest(testId);
            if (!(questions?.Count > 0)) return default;
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
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
            questionService.MapOptionsToQuestion(questionsReturn, options);
            questionService.MapResultsToQuestion(questionsReturn, results);
            return questionsReturn;
        }

        public override async Task BeforeCommitAsync(TestEditDto test)
        {
            if (test == null || test.questions?.Count == 0) return;

            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var questionResultService = _serviceProvider.GetRequiredService<IResultQuestionService>();

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
            var resultsHandle = test.questions?
                .SelectMany(question => question.results)
                .Where(r => r.State == ModelState.Insert ||
                                r.State == ModelState.Update ||
                                r.State == ModelState.Delete)
                .ToList();

            var tasks = new List<Task>();

            if (questionsHandle?.Count > 0)
            {
                await questionService.SubmitManyAsync(questionsHandle);
            }
            if (optionsHandle?.Count > 0)
            {
                await optionService.SubmitManyAsync(optionsHandle);
            }
            if (resultsHandle?.Count > 0)
            {
                await questionResultService.SubmitManyAsync(resultsHandle);
            }
            if (questionsHandle?.Count > 0)
            {
               await questionTestService.SubmitManyAsync(
                    questionsHandle.Select(question => new QuestionTestEditDto
                    {
                        question_test_id = Guid.NewGuid(),
                        test_id = test.test_id,
                        question_id = question.question_id,
                        State = question.State
                    }).ToList()
                    );
            };
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

        public async Task<List<ExamDto>> MarkTest(Guid testId)
        {
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();

            // Lấy thông tin Test dựa trên testId
            var testTask = await this.GetById<TestDto>(testId);

            // Thiết lập các điều kiện lọc
            var filterCondition = new FilterCondition
            {
                Field = "test_id",
                Operator = FilterOperator.Equal,
                Value = testId
            };
            var filterExam = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.Equal,
                    Value = testId
                },
                new FilterCondition
                {
                    Field = "status",
                    Operator = FilterOperator.NotEqual,
                    Value = ExamStatus.Marked
                },
            };

            // Gọi các API một cách song song
            var questionsTestTask = await questionTestService.FilterAsync(new List<FilterCondition> { filterCondition });
            var examsTask = await examService.FilterAsync<ExamEditDto>(filterExam);

            // Đợi tất cả các Task hoàn thành
            //await Task.WhenAll(testTask, questionsTestTask, examsTask);

            // Lấy kết quả từ các Task
            var test = testTask;
            var questionsTest = questionsTestTask;
            var exams = examsTask;

            // Kiểm tra điều kiện trả về false sớm
            if (test == null || questionsTest == null || questionsTest.Count == 0 || exams == null || exams.Count == 0)
                return default;

            // Xử lý tiếp theo
            var questionIds = questionsTest.Select(q => q.question_id).ToList();
            var filterQuestions = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.In,
                Value = questionIds
            };

            // Gọi song song các truy vấn lấy thông tin chi tiết các câu hỏi và kết quả
            var questionsTask = await questionService.FilterAsync(new List<FilterCondition> { filterQuestions });
            var resultsTask = await resultQuestionService.FilterAsync(new List<FilterCondition> { filterQuestions });

            // Lấy kết quả từ các Task
            var questions =  questionsTask;
            var results =  resultsTask;

            // Tiếp tục xử lý logic dựa trên kết quả trên
            questionService.MapResultsToQuestion(questions, results);
            test.questions = questions;
            var examIds = exams.Select(e => e.exam_id).ToList();
            var filterAnswer = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "exam_id",
                    Operator = FilterOperator.In,
                    Value = examIds
                }
            };
            var answers = await answerService.FilterAsync<AnswerEditDto>(filterAnswer);
            if (answers?.Count == 0) return default;
            MapAnswersToExam(answers, exams);
            for (var index =0; index < exams.Count; index++)
            {
                examService.MarkExam(exams[index], test);
            }
            await examService.UpdateManyAsync(exams);
            var examsDto = _mapper.Map<List<ExamDto>>(exams);
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            var userIds = exams?.Select(e => e.user_id).ToList();
            var filterUserExam = new FilterCondition
            {
                Field = "user_id",
                Operator = FilterOperator.In,
                Value = userIds
            };
            var usersDoExam = await userService.FilterAsync<UserDto>(new List<FilterCondition> { filterUserExam });
            if (usersDoExam?.Count > 0)
            {
                examsDto.ForEach(exam =>
                {
                    var userDoExam = usersDoExam.FirstOrDefault(e => e.user_id == exam.user_id);
                    if (userDoExam != null)
                    {
                        exam.name = userDoExam.name;
                    }
                });
            }
            return examsDto;
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

        public async Task<List<ExamDto>> GetExamUserHistory(Guid testId)
        {
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var filters = new List<FilterCondition> { 
                new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.Equal,
                    Value = testId
                },
                new FilterCondition
                {
                    Field = "user_id",
                    Operator = FilterOperator.Equal,
                    Value = contextData.user.user_id
                }
            };
            return await examService.FilterAsync<ExamDto>(filters);
        }

        public async Task<TestDoingDto> HandleGetDoTest(Guid testId)
        {
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var test = await examService.LastExam(testId);
            return test;
        }

        public async Task<List<QuestionDto>> GenAutoTest(ParamAutoGenTest param)
        {
            var questionsEntity = new List<QuestionEntity>();
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();

            for (var index = 0; index < param?.chapters.Count; index++)
            {
                var questionsTmp = await questionService.GetRandomQuestion(param.chapters[index]);
                if (questionsTmp?.Count > 0)
                {
                    questionsEntity.AddRange(questionsTmp);
                }
            }
            if (questionsEntity.Count > 0)
            {
                var optionService = _serviceProvider.GetRequiredService<IOptionService>();
                var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();

                var questionIds = questionsEntity.Select(question => question.question_id).ToList();
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
                var questionsReturn = _mapper.Map<List<QuestionDto>>(questionsEntity);
                questionService.MapOptionsToQuestion(questionsReturn, options);
                questionService.MapResultsToQuestion(questionsReturn, results);
                return questionsReturn;
            }
            return default;

        }
        
        public async Task<List<ExamDto>> GetExamMarkAsync(Guid testId)
        {
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var userService = _serviceProvider.GetRequiredService<IUserService>();

            var filterExam = new FilterCondition
            {
                Field = "test_id",
                Operator = FilterOperator.Equal,
                Value = testId
            };

            // Gọi các API một cách song song
            var exams = await examService.FilterAsync<ExamDto>(new List<FilterCondition> { filterExam });
            var userIds = exams?.Select(e => e.user_id).ToList();
            var filterUserExam = new FilterCondition
            {
                Field = "user_id",
                Operator = FilterOperator.In,
                Value = userIds
            };
            var usersDoExam = await userService.FilterAsync<UserDto>(new List<FilterCondition> { filterUserExam });
            if (usersDoExam?.Count > 0)
            {
                exams.ForEach(exam =>
                {
                    var userDoExam = usersDoExam.FirstOrDefault(e => e.user_id == exam.user_id);
                    if (userDoExam != null)
                    {
                        exam.name = userDoExam.name;
                    }
                });
            }
            return exams;
        }

        public async Task<List<TestDto>> GetTestOfUserAsync()
        {
            var user = contextData.user;
            var filterTest = new List<FilterCondition>();

            if (user.role_id == Role.Teacher || user.role_id == Role.Admin)
            {
                filterTest.Add(new FilterCondition
                {
                    Field = "user_id",
                    Operator = FilterOperator.Equal,
                    Value = user.user_id
                });
                return await this.FilterAsync<TestDto>(filterTest);
            }
            else if (user.role_id == Role.Student)
            {
                var examService = _serviceProvider.GetRequiredService<IExamService>();
                var exams = await examService.FilterAsync(new List<FilterCondition>
                {
                    new FilterCondition { Field = "user_id", Operator = FilterOperator.Equal, Value = user.user_id }
                });
                if (exams?.Count > 0)
                {
                    filterTest.Add(new FilterCondition
                    {
                        Field = "test_id",
                        Operator = FilterOperator.Equal,
                        Value = exams.Select(e => e.test_id).ToList()
                    });
                    return await this.FilterAsync<TestDto>(filterTest);
                }
            }
            return default;
        }
    }
}
