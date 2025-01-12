using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Constract.Chapter;
using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.QuestionChapters;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Report;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared;
using Bg.EduSocial.Domain.Shared.Questions;
using Bg.EduSocial.Domain.Shared.Roles;
using Microsoft.Extensions.DependencyInjection;

namespace Bg.EduSocial.Application
{
    public class ReportService: IReportService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IContextService _contextService;
        public ReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _contextService = _serviceProvider.GetRequiredService<IContextService>();
        }
        protected ContextData contextData
        {
            get
            {
                var contextData = _contextService.GetContextData();
                return contextData ?? new ContextData();
            }
        }

        public async Task<List<Dictionary<string, object>>> ReportChapter(ReportParam param)
        {
            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var chapterService = _serviceProvider.GetRequiredService<IChapterService>();
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();


            var user = contextData.user;
            var chapters = await chapterService.FilterAsync(new List<FilterCondition>());
            var filtersTest = new List<FilterCondition>();

            if (param != null && param.TestIds?.Count > 0)
            {
                filtersTest.Add(new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.In,
                    Value = param.TestIds
                });
            }
            else
            {
                var testOfUser = await testService.GetTestOfUserAsync();
                if (testOfUser?.Count > 0)
                {
                    filtersTest.Add(new FilterCondition
                    {
                        Field = "test_id",
                        Operator = FilterOperator.In,
                        Value = testOfUser?.Select(test => test.test_id).ToList()
                    });
                } else
                {
                    return default;
                }
            }
            var tests = await testService.FilterAsync(filtersTest);
            if (param!= null && param.ClassIds?.Count > 0 && (user.role_id == Role.Admin || user.role_id == Role.Teacher))
            {
                var enrollmentClassService = _serviceProvider.GetRequiredService<IEnrollmentClassService>();
                var filterClassroom = new List<FilterCondition> { new FilterCondition { Field = "classroom_id", Operator = FilterOperator.In, Value = param.ClassIds } };
                var userInClassrooms = await enrollmentClassService.FilterAsync<EnrollmentClassDto>(filterClassroom);
                if (filtersTest?.Count > 0)
                {
                    filtersTest.Add(new FilterCondition
                    {
                        Field = "user_id",
                        Operator = FilterOperator.In,
                        Value = userInClassrooms?.Select(u => u.user_id).ToList(),
                    });
                }
            };
            filtersTest?.Add(new FilterCondition
            {
                Field = "status",
                Operator = FilterOperator.Equal,
                Value = ExamStatus.Marked
            });

            var exams = await examService.FilterAsync<ExamDto>(filtersTest);
            if (exams == null || exams.Count == 0)
                return new List<Dictionary<string, object>>();
            var filterAnswerExam = new List<FilterCondition> 
            {
                new FilterCondition
                {
                    Field = "exam_id",
                    Operator = FilterOperator.In,
                    Value = exams.Select(e => e.exam_id).ToList(),
                }
            };
            var answersHandle = await answerService.FilterAsync<AnswerDto>(filterAnswerExam);
            if (answersHandle == null || answersHandle.Count == 0) return new List<Dictionary<string, object>>();

            var filterQuestionTest = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.In,
                    Value = tests?.Select(e => e.test_id).ToList(),
                }
            };
            var questionsTest = await questionTestService.FilterAsync(filterQuestionTest);
            if (!(questionsTest?.Count > 0)) return default;
            var filterQuestion = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "question_id",
                    Operator = FilterOperator.In,
                    Value = questionsTest?.Select(e => e.question_id).ToList(),
                }
            };
            var questionsHandle = await questionService.FilterAsync(filterQuestion);

            var result = chapters.Select(chapter =>
            {
                decimal totalPointChapter = 0;
                decimal totalPointCorrectChapter = 0;
                decimal totalPointInCorrectChapter = 0;
                decimal totalPointUnAttemptChapter = 0;


                tests.ForEach(test =>
                {
                    var examOfThisTest = exams.Where(e => e.test_id == test.test_id).ToList();
                    var questionTestOfThisTest = questionsTest.Where(q => q.test_id == test.test_id).ToList();
                    var questionIdsInTest = questionTestOfThisTest.Select(q => q.question_id).ToList();
                    var questionChapterOfThisTest = questionsHandle.Where(q => questionIdsInTest.Contains(q.question_id) && q.chapter_ids != null && q.chapter_ids.Contains(chapter.chapter_id.ToString())).ToList();
                    if (questionChapterOfThisTest?.Count > 0)
                    {
                        var questionIdsChapter = questionChapterOfThisTest?.Select(q => q.question_id).ToList();
                        if (questionIdsChapter?.Count > 0)
                        {
                            decimal totalPointChapterThisTest = questionTestOfThisTest.Where(question => questionIdsChapter.Contains(question.question_id))?.Sum(q => q.point) ?? 0;
                            totalPointChapter += (examOfThisTest?.Count ?? 0) * totalPointChapterThisTest;
                            examOfThisTest?.ForEach(e =>
                            {
                                var answerOfChapter = answersHandle?.Where(a => questionIdsChapter?.Count > 0 && questionIdsChapter.Contains(a.question_id)).ToList();
                                var totalAnswerCorrect = answerOfChapter?.Sum(a => a.point) ?? 0;
                                totalPointCorrectChapter += totalAnswerCorrect;
                                totalPointInCorrectChapter += (totalPointChapterThisTest - totalAnswerCorrect);
                            });
                        }

                    }
                });
                totalPointUnAttemptChapter = (totalPointChapter - totalPointCorrectChapter - totalPointInCorrectChapter) > 0 ? (totalPointChapter - totalPointCorrectChapter - totalPointInCorrectChapter) : 0;
                // Lấy tất cả questionChapter thuộc chapter này
                return new Dictionary<string, object>
                {
                    { "chapter_id", chapter.chapter_id },
                    { "chapter_name", chapter.name },
                    { "correct", totalPointCorrectChapter },
                    { "incorrect", totalPointInCorrectChapter },
                    { "unattempted",  totalPointUnAttemptChapter}

                };
            }).ToList();
            return result;
        }


        public async Task<List<Dictionary<string, object>>> ReportLevel(ReportParam param)
        {
            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();


            var user = contextData.user;
            var filtersTest = new List<FilterCondition>();

            if (param != null && param.TestIds?.Count > 0)
            {
                filtersTest.Add(new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.In,
                    Value = param.TestIds
                });
            }
            else
            {
                var testOfUser = await testService.GetTestOfUserAsync();
                if (testOfUser?.Count > 0)
                {
                    filtersTest.Add(new FilterCondition
                    {
                        Field = "test_id",
                        Operator = FilterOperator.In,
                        Value = testOfUser?.Select(test => test.test_id).ToList()
                    });
                }
                else
                {
                    return default;
                }
            }
            var tests = await testService.FilterAsync(filtersTest);
            if (!(tests?.Count > 0)) return default;
            if (param != null && param.ClassIds?.Count > 0 && (user.role_id == Role.Admin || user.role_id == Role.Teacher))
            {
                var enrollmentClassService = _serviceProvider.GetRequiredService<IEnrollmentClassService>();
                var filterClassroom = new List<FilterCondition> { new FilterCondition { Field = "classroom_id", Operator = FilterOperator.In, Value = param.ClassIds } };
                var userInClassrooms = await enrollmentClassService.FilterAsync<EnrollmentClassDto>(filterClassroom);
                if (filtersTest?.Count > 0)
                {
                    filtersTest.Add(new FilterCondition
                    {
                        Field = "user_id",
                        Operator = FilterOperator.In,
                        Value = userInClassrooms?.Select(u => u.user_id).ToList(),
                    });
                }
            };
            filtersTest?.Add(new FilterCondition
            {
                Field = "status",
                Operator = FilterOperator.Equal,
                Value = ExamStatus.Marked
            });

            var exams = await examService.FilterAsync<ExamDto>(filtersTest);
            if (exams == null || exams.Count == 0)
                return new List<Dictionary<string, object>>();
            var filterAnswerExam = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "exam_id",
                    Operator = FilterOperator.In,
                    Value = exams.Select(e => e.exam_id).ToList(),
                }
            };
            var answersHandle = await answerService.FilterAsync<AnswerDto>(filterAnswerExam);
            if (answersHandle == null || answersHandle.Count == 0) return new List<Dictionary<string, object>>();
            var filterQuestionTest = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.In,
                    Value = tests?.Select(e => e.test_id).ToList(),
                }
            };
            var questionsTest = await questionTestService.FilterAsync(filterQuestionTest);
            if (!(questionsTest?.Count > 0)) return default;
            var filterQuestion = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "question_id",
                    Operator = FilterOperator.In,
                    Value = questionsTest?.Select(e => e.question_id).ToList(),
                }
            };
            var questionsHandle = await questionService.FilterAsync(filterQuestion);
            var result = Enum.GetValues(typeof(QuestionLevel))
            .Cast<QuestionLevel>()
            .Select(level =>
            {
                decimal totalPointChapter = 0;
                decimal totalPointCorrectChapter = 0;
                decimal totalPointInCorrectChapter = 0;
                decimal totalPointUnAttemptChapter = 0;


                tests.ForEach(test =>
                {
                    var examOfThisTest = exams.Where(e => e.test_id == test.test_id).ToList();
                    var questionTestOfThisTest = questionsTest.Where(q => q.test_id == test.test_id).ToList();
                    var questionIdsInTest = questionTestOfThisTest.Select(q => q.question_id).ToList();
                    var questionLevelOfThisTest = questionsHandle.Where(q => questionIdsInTest.Contains(q.question_id) && q.level == level).ToList();
                    if (questionLevelOfThisTest?.Count > 0)
                    {
                        var questionIdsLevel = questionLevelOfThisTest?.Select(q => q.question_id).ToList();
                        if (questionIdsLevel?.Count > 0)
                        {
                            decimal totalPointLevelThisTest = questionTestOfThisTest.Where(question => questionIdsLevel.Contains(question.question_id))?.Sum(q => q.point) ?? 0;
                            totalPointChapter += (examOfThisTest?.Count ?? 0) * totalPointLevelThisTest;
                            examOfThisTest?.ForEach(e =>
                            {
                                var answerOfChapter = answersHandle?.Where(a => questionIdsLevel?.Count > 0 && questionIdsLevel.Contains(a.question_id)).ToList();
                                var totalAnswerCorrect = answerOfChapter?.Sum(a => a.point) ?? 0;
                                totalPointCorrectChapter += totalAnswerCorrect;
                                totalPointInCorrectChapter += (totalPointLevelThisTest - totalAnswerCorrect);
                            });
                        }

                    }
                });
                totalPointUnAttemptChapter = (totalPointChapter - totalPointCorrectChapter - totalPointInCorrectChapter) > 0 ? (totalPointChapter - totalPointCorrectChapter - totalPointInCorrectChapter) : 0;
                // Lấy tất cả questionChapter thuộc chapter này
                return new Dictionary<string, object>
                {
                    { "level", level },
                    { "level_name",  this.GetNameLeve(level)},
                    { "correct", totalPointCorrectChapter },
                    { "incorrect", totalPointInCorrectChapter },
                    { "unattempted",  totalPointUnAttemptChapter}

                };
            }).ToList();
            return result;
        }
        private string GetNameLeve(QuestionLevel level)
        {
            switch (level)
            {
                case QuestionLevel.Recognition:
                    return "Nhận biết";
                case QuestionLevel.Comprehension:
                    return "Thông hiểu";
                case QuestionLevel.Application:
                    return "Vận dụng";
                case QuestionLevel.AdvancedApplication:
                    return "Vận dụng cáo";
            }
            return string.Empty;
        }

        public async Task<List<Dictionary<string, object>>> ReportTest(ReportParam param)
        {
            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var user = contextData.user;
            var filtersTest = new List<FilterCondition>();
            if (param != null && param.TestIds?.Count > 0)
            {
                filtersTest.Add(new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.In,
                    Value = param.TestIds
                });
            }
            else
            {
                var testOfUser = await testService.GetTestOfUserAsync();
                if (testOfUser?.Count > 0)
                {
                    filtersTest.Add(new FilterCondition
                    {
                        Field = "test_id",
                        Operator = FilterOperator.In,
                        Value = testOfUser?.Select(test => test.test_id).ToList()
                    });
                }
            }

            var tests = await testService.FilterAsync(filtersTest);
            if (param.ClassIds?.Count > 0 && (user.role_id == Role.Admin || user.role_id == Role.Teacher))
            {
                var enrollmentClassService = _serviceProvider.GetRequiredService<IEnrollmentClassService>();
                var filterClassroom = new List<FilterCondition> { new FilterCondition { Field = "classroom_id", Operator = FilterOperator.In, Value = param.ClassIds } };
                var userInClassrooms = await enrollmentClassService.FilterAsync<EnrollmentClassDto>(filterClassroom);
                if (userInClassrooms?.Count > 0)
                {
                    filtersTest.Add(new FilterCondition
                    {
                        Field = "user_id",
                        Operator = FilterOperator.In,
                        Value = userInClassrooms?.Select(u => u.user_id).ToList(),
                    });
                }
            };
               
            var exams = await examService.FilterAsync<ExamDto>(filtersTest);
            if (exams == null || exams.Count == 0)
                return new List<Dictionary<string, object>>();

            // Tạo ra dictionary để lưu điểm số và số lượng bài thi tương ứng
            var result = new List<Dictionary<string, object>>();
            // Group exams by user_id and test_id
            var groupedExams = exams
                .GroupBy(e => e.user_id)
                .ToDictionary(
                    g => g.Key, // Key: user_id
                    g => g
                        .GroupBy(e => e.test_id) // Group by test_id
                        .ToDictionary(
                            testGroup => testGroup.Key, // Key: test_id
                            testGroup => testGroup.Select(e => e.point).ToList() // Value: list of points
                        )
                );
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            var userIds = exams?.Select(e => e.user_id).Distinct().ToList();
            var filterUserExam = new FilterCondition
            {
                Field = "user_id",
                Operator = FilterOperator.In,
                Value = userIds
            };
            var usersDoExam = await userService.FilterAsync<UserDto>(new List<FilterCondition> { filterUserExam });
            // Convert the grouped data into the desired format
            foreach (var student in groupedExams)
            {
                var studentScores = new Dictionary<string, object>
                {
                    { "user_id", student.Key.ToString() }
                };
                var userDo = usersDoExam?.FirstOrDefault( u => u.user_id ==  student.Key );
                if (userDo != null)
                {
                    studentScores["name"] = userDo.name;
                }
                foreach (var test in student.Value)
                {
                    studentScores[test.Key.ToString()] = test.Value;
                }

                result.Add(studentScores);
            }
            return result;
        }
    }
}
