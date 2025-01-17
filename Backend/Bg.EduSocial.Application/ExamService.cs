﻿using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Answers;
using Bg.EduSocial.Constract.ExamNotes;
using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Exams;
using Bg.EduSocial.Domain.Shared;
using Bg.EduSocial.Domain.Shared.ModelState;
using Bg.EduSocial.Domain.Shared.Questions;
using Microsoft.Extensions.DependencyInjection;

namespace Bg.EduSocial.Application
{
    public class ExamService : WriteService<IExamRepo, ExamEntity, ExamDto, ExamEditDto>, IExamService
    {
        public ExamService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ExamEditDto> DoExam(ExamEditDto exam)
        {
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();
            var answers = exam.answers;
            await answerService.SubmitManyAsync(answers);
            return exam;
        }

        public async Task<ExamEditDto> SubmitExam(ExamEditDto exam)
        {
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();
            var answers = exam.answers;
            await answerService.SubmitManyAsync(answers);
            var examEditDto = await this.GetById<ExamEditDto>(exam.exam_id);
            examEditDto.status = ExamStatus.Finish;
            await this.UpdateAsync(examEditDto);
            return exam;
        }

        public ExamEditDto MarkExam(ExamEditDto exam, TestDto test)
        {
            var answers = exam.answers;
            var questions = test.questions;
            if (!(questions?.Count > 0) || !(answers?.Count > 0))
            {
                exam.point = 0;
                exam.status = ExamStatus.Marked;
                return exam;
            };
            foreach (var question in questions)
            {
                var answer = answers?.FirstOrDefault(a => a.question_id ==  question.question_id);
                var resultsQuestion = question?.results?.Where(r => r.question_id == question.question_id).ToList();
                if (answer != null && resultsQuestion?.Count > 0)
                {
                    switch (question.type)
                    {
                        case QuestionType.SingleChoice:
                            if (answer.content == resultsQuestion[0].content)
                            {
                                answer.State = ModelState.Update;
                                answer.point = question.point;
                            } else
                            {
                                answer.State = ModelState.Update;
                                answer.point = 0;
                            }
                            break;
                        case QuestionType.MultiChoice:
                            var isCorrect = CompareGuidStrings(answer.content, resultsQuestion[0].content);
                            if (isCorrect)
                            {
                                answer.State = ModelState.Update;
                                answer.point = question.point;
                            } else
                            {
                                answer.State = ModelState.Update;
                                answer.point = 0;
                            }
                            break;

                    }
                }
            }
            exam.point = answers.Sum(a => a.point);
            exam.status = ExamStatus.Marked;
            return exam;
        }
        public static bool CompareGuidStrings(string str1, string str2)
        {
            // Tách chuỗi và chuyển đổi thành List<Guid>
            var guids1 = str1.Split(',').Select(guid => Guid.Parse(guid.Trim())).ToList();
            var guids2 = str2.Split(',').Select(guid => Guid.Parse(guid.Trim())).ToList();

            // Sử dụng HashSet để loại bỏ các phần tử trùng lặp và không quan tâm đến thứ tự
            var set1 = new HashSet<Guid>(guids1);
            var set2 = new HashSet<Guid>(guids2);

            // So sánh hai HashSet
            return set1.SetEquals(set2);
        }

        /// <summary>
        /// Làm bài thi mới
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public async Task<ExamEditDto> NewExam(Guid testId)
        {
            var newExam = new ExamEditDto
            {
                exam_id = Guid.NewGuid(),
                test_id = testId,
                user_id = contextData.user.user_id,
                status = ExamStatus.Doing,
                point = 0,
                created_date = DateTime.Now
            };
            await this.InsertAsync(newExam);
            return newExam;
        }
       
        public async Task<TestDto> TestOfExam(Guid examId)
        {
            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var exam = await this.GetById<ExamDto>(examId);
            if (exam != null)
            {
                var test = await testService.GetTestDetail(exam.test_id);
                test.exam = exam;
                return test;
            }
            return default;
        }

        public async Task<ExamEditDto> MarkExam(Guid examId)
        {
            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();

            var examUpdate = await this.GetById<ExamEditDto>(examId);
            // Lấy kết quả từ các Task
            var test = await testService.prepareTestMark(examUpdate.test_id);
            // Kiểm tra điều kiện trả về false sớm
            if (test == null)
                return default;
            await prepareExamMark(new List<ExamEditDto> { examUpdate });

            this.MarkExam(examUpdate, test);
            var answersUpdate = examUpdate?.answers.Where(a => a.State == ModelState.Insert || a.State == ModelState.Update).ToList();
            await answerService.UpdateManyAsync(answersUpdate);
            await this.UpdateAsync(examUpdate);
            return examUpdate;
        }
        public async Task<List<ExamEditDto>> prepareExamMark(List<ExamEditDto> exams)
        {
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();
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
            return exams;
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
        public async Task<TestDto> HistoryExam(Guid examId)
        {
            var test = await this.TestOfExam(examId);
            var questions = test.questions;
            if (questions == null || questions?.Count == 0) return default;
            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();
            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var examNoteService = _serviceProvider.GetRequiredService<IExamNoteService>();
            var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();
            
            var filterAnswer = new FilterCondition
            {
                Field = "exam_id",
                Operator = FilterOperator.Equal,
                Value = examId
            };
            var answers = await answerService.FilterAsync<AnswerDto>(new List<FilterCondition> { filterAnswer });

            var questionIds = questions.Select(q => q.question_id).ToList();
            var filterQuestions = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.In,
                Value = questionIds
            };

            // Gọi song song các truy vấn lấy thông tin chi tiết các câu hỏi và kết quả
            var results = await resultQuestionService.FilterAsync(new List<FilterCondition> { filterQuestions });

            var filtersExamNote = new List<FilterCondition>
            {
                new FilterCondition {
                    Field = "exam_id",
                    Operator = FilterOperator.Equal,
                    Value = examId
                }
            };
            if (test?.exam != null)
            {
                var notes = await examNoteService.FilterAsync(filtersExamNote);
                test.exam.notes = notes;
            }
            // Tiếp tục xử lý logic dựa trên kết quả trên
            questionService.MapResultsToQuestion(questions, results);


            foreach (var question in questions)
            {
                var answerOfQuestion = answers.FirstOrDefault(a => a.question_id == question.question_id);
                if (answerOfQuestion != null)
                {
                    question.point = answerOfQuestion.point;
                    question.answer = answerOfQuestion;
                }
            }
            return test;
        }

        public async Task<TestDto> HandleDoingExam(ExamEntity examDoing)
        {
            var test = await this.TestOfExam(examDoing.exam_id);
            // Tính toán thời điểm kết thúc dựa trên created_date và duration
            var questions = test.questions;
            if (questions == null || questions?.Count == 0) return default;
            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var answerService = _serviceProvider.GetRequiredService<IAnswerService>();
            var filterAnswer = new FilterCondition
            {
                Field = "exam_id",
                Operator = FilterOperator.Equal,
                Value = examDoing.exam_id
            };
            var answers = await answerService.FilterAsync<AnswerDto>(new List<FilterCondition> { filterAnswer });


            foreach (var question in questions)
            {
                var answerOfQuestion = answers.FirstOrDefault(a => a.question_id == question.question_id);
                if (answerOfQuestion != null)
                {
                    question.answer = answerOfQuestion;
                }
            }
            return test;
        }

        public async Task<TestDoingDto> LastExam(Guid testId)
        {
            var user = contextData.user;
            var examDoing = await _repo.ExamDoing(testId, user.user_id);
            TestDto test = null;
            if (examDoing == null)
            {
                var newExam = await NewExam(testId);
                test = await this.TestOfExam(testId);
                return new TestDoingDto
                {
                    test = test,
                    exam_id = newExam.exam_id
                };
            }
            else
            {
                test= await HandleDoingExam(examDoing);
                return new TestDoingDto
                {
                    test = test,
                    exam_id = examDoing.exam_id
                };
            }
        }

        public async Task<ExamEditDto> NoteExam(ExamEditDto exam)
        {
            var examUpdate = await this.GetById<ExamEditDto>(exam.exam_id);
            examUpdate.question_ids_attention = exam.question_ids_attention;
            if (exam.notes?.Count() > 0)
            {
                var examNoteService = _serviceProvider.GetRequiredService<IExamNoteService>();
                await examNoteService.SubmitManyAsync(exam.notes);
            }
            return await this.UpdateAsync(examUpdate);
        }
    }
}
