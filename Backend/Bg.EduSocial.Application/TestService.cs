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
using MySqlX.XDevAPI;

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
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var optionService = _serviceProvider.GetRequiredService<IOptionService>();
            var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();

            var filtersQuestionTest = new List<FilterCondition>
            {
                new FilterCondition {
                    Field = "test_id",
                    Operator = FilterOperator.Equal,
                    Value = testId
                },
            };
            var resultsQuestionTest = await questionTestService.FilterAsync(filtersQuestionTest);
            if (resultsQuestionTest == null || resultsQuestionTest.Count == 0) return default;

            var filterQuestions = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "question_id",
                    Operator = FilterOperator.In,
                    Value = resultsQuestionTest.Select(q => q.question_id).ToList()
                }
            };

            var questions = await questionService.FilterAsync(filterQuestions);
            var questionIds = questions.Select(question => question.question_id).ToList();
            var filterOptionOfTest = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.In,
                Value = questionIds
            };
            var options = await optionService.FilterAsync(new List<FilterCondition> { filterOptionOfTest });
            var filterResultOfTest = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.In,
                Value = questionIds
            };
            

            var results = await resultQuestionService.FilterAsync(new List<FilterCondition> { filterResultOfTest });
            var questionsReturn = _mapper.Map<List<QuestionDto>>(questions);
            questionService.MapOptionsToQuestion(questionsReturn, options);
            questionService.MapResultsToQuestion(questionsReturn, results);
            questionService.MapQuestionTestToQuestion(questionsReturn, resultsQuestionTest);

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
                var filtersQuestionTest = new List<FilterCondition> {
                    new FilterCondition
                    {
                        Field = "test_id",
                        Operator = FilterOperator.Equal,
                        Value = test.test_id
                    },
                    new FilterCondition
                    {
                        Field = "question_id",
                        Operator = FilterOperator.In,
                        Value = questionsHandle.Select(q => q.question_id).ToList()
                    },
                };
                var questionsTesthandle = await questionTestService.FilterAsync(filtersQuestionTest);
                questionsHandle.Where(q => q.State != ModelState.Insert).ToList().ForEach(question => {
                    var questionTmp = questionsTesthandle.FirstOrDefault(q => q.question_id == question.question_id);
                    if (questionTmp != null)
                    {
                        questionTmp.point = question.point;
                        questionTmp.State = question.State;
                    }
                });
                var questionTestAdd = questionsHandle.Where(q => q.State == ModelState.Insert).Select(question => new QuestionTestEditDto
                {
                    question_test_id = Guid.NewGuid(),
                    test_id = test.test_id,
                    question_id = question.question_id,
                    State = question.State
                }).ToList();
                questionsTesthandle.AddRange(questionTestAdd);
                await questionTestService.SubmitManyAsync(questionsTesthandle);
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
            // Thiết lập các điều kiện lọc
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
            var examsTask = await examService.FilterAsync<ExamEditDto>(filterExam);

            // Đợi tất cả các Task hoàn thành
            //await Task.WhenAll(testTask, questionsTestTask, examsTask);

            // Lấy kết quả từ các Task
            var test = await prepareTestMark(testId);
            var exams = examsTask;

            // Kiểm tra điều kiện trả về false sớm
            if (test == null)
                return default;
            await examService.prepareExamMark(exams);
            
            for (var index =0; index < exams.Count; index++)
            {
                examService.MarkExam(exams[index], test);
            }
            var answersUpdate = exams.SelectMany(e => e.answers).Where(a => a.State == ModelState.Insert || a.State == ModelState.Update).ToList();
            await answerService.UpdateManyAsync(answersUpdate);
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


        public async Task<TestDto> prepareTestMark(Guid testId)
        {
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();

            // Lấy thông tin Test dựa trên testId
            var testTask = await this.GetById<TestDto>(testId);

            // Thiết lập các điều kiện lọc
            var filterCondition = new FilterCondition
            {
                Field = "test_id",
                Operator = FilterOperator.Equal,
                Value = testId
            };

            // Gọi các API một cách song song
            var questionsTestTask = await questionTestService.FilterAsync(new List<FilterCondition> { filterCondition });

            // Đợi tất cả các Task hoàn thành
            //await Task.WhenAll(testTask, questionsTestTask, examsTask);

            // Lấy kết quả từ các Task
            var test = testTask;
            var questionsTest = questionsTestTask;

            // Kiểm tra điều kiện trả về false sớm
            if (test == null || questionsTest == null || questionsTest.Count == 0)
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
            var questions = questionsTask;
            var results = resultsTask;

            // Tiếp tục xử lý logic dựa trên kết quả trên
            questionService.MapResultsToQuestion(questions, results);
            questionService.MapQuestionTestToQuestion(questions, questionsTestTask);

            test.questions = questions;
            return test;
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
                },
                new FilterCondition
                {
                    Field = "status",
                    Operator = FilterOperator.Equal,
                    Value = ExamStatus.Marked
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
                    Operator = FilterOperator.In,
                    Value = questionIds
                };
                var options = await optionService.FilterAsync(new List<FilterCondition> { filterOptionOfTest });
                var filterResultOfTest = new FilterCondition
                {
                    Field = "question_id",
                    Operator = FilterOperator.In,
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
                        Operator = FilterOperator.In,
                        Value = exams.Select(e => e.test_id).ToList()
                    });
                    return await this.FilterAsync<TestDto>(filterTest);
                }
            }
            return default;
        }

        public async Task<List<Dictionary<string, object>>> UsersCorrection(Guid testId, Guid questionId)
        {
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            var result = new List<Dictionary<string, object>>();
            var examFilters = new List<FilterCondition> {
                new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.Equal,  
                    Value = testId
                },
                new FilterCondition {
                    Field = "question_ids_attention",
                    Operator = FilterOperator.Contains,
                    Value = questionId
                },
            };
            var examsHandle = await examService.FilterAsync(examFilters);
            if (!(examsHandle?.Count > 0)) return result;
            var filtersUser = new List<FilterCondition> {
                new FilterCondition
                {
                    Field = "user_id",
                    Operator = FilterOperator.In,
                    Value = examsHandle.Select(e => e.user_id).ToList()  
                }
            };
            var usersHandle = await userService.FilterAsync(filtersUser);
            result.Add(new Dictionary<string, object>
            {
                { "question_id", questionId },
                { "users", usersHandle?.Select(u => u.name).ToList() },
            });
            return result;
        }
    }
}
