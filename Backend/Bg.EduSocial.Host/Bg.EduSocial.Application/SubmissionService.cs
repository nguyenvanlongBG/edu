using AutoMapper;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Submissions;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Shared.Modes;
using Bg.EduSocial.Domain.Shared.Questions;
using Bg.EduSocial.Domain.Submissions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application
{
    public class SubmissionService : WriteService<Submission, SubmissionDto, SubmissionEditDto, SubmissionEditDto>, ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ISubmissionAnswerRepository _submissionAnswerRepository;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        public SubmissionService(IUnitOfWork<EduSocialDbContext> unitOfWork, ITestRepository testRepository, ISubmissionRepository submissionRepository, ISubmissionAnswerRepository submissionAnswerRepository, IQuestionRepository questionRepository, IMapper mapper) : base(unitOfWork, submissionRepository, mapper)
        {
            _testRepository = testRepository;
            _submissionRepository = submissionRepository;
            _submissionAnswerRepository = submissionAnswerRepository;
            _questionRepository = questionRepository;
        }
        public override async Task<int> InsertAsync(SubmissionEditDto submissionEditDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var submission = _mapper.Map<Submission>(submissionEditDto);
                await _submissionRepository.InsertAsync(submission);
                _unitOfWork.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return 0;
        }

        public override async Task<int> UpdateAsync(Guid id, SubmissionEditDto submissionEditDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var answersInsertDto = new List<SubmissionAnswerDto>();
                var answersUpdateDto = new List<SubmissionAnswerDto>();
                foreach (var submissionAnswer in submissionEditDto.SubmissionAnswers)
                {
                    if (submissionAnswer.EditMode == EditMode.ADD)
                    {
                        answersInsertDto.Add(submissionAnswer);
                    } else if (submissionAnswer.EditMode == EditMode.EDIT)
                    {
                        answersUpdateDto.Add(submissionAnswer);
                    }
                }
                var answersInsert = _mapper.Map<List<SubmissionAnswerDto>, List<SubmissionAnswer>>(answersInsertDto);
                var answersUpdate = _mapper.Map<List<SubmissionAnswerDto>, List<SubmissionAnswer>>(answersUpdateDto);
                var numberSubmissionAnswerInsertSuccess = await _submissionAnswerRepository.InsertManyAsync(answersInsert.ToArray());
                var numberSubmissionAnswerUpdateSuccess = await _submissionAnswerRepository.UpdateManyAsync(answersUpdate.ToArray());
                _unitOfWork.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return 0;
        }
        public async Task<SubmissionDto> MarkSubmission(Guid id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var submission = await _submissionRepository.GetById(id);
                var questions = await _questionRepository.GetByTestID(submission.TestID);
                var numQuestionCorrect = 0;
                foreach (var answer in submission.SubmissionAnswers)
                {
                    var question = questions.Where(q => q.ID == answer.QuestionID).FirstOrDefault();
                    if (question != null)
                    {
                        switch (question.QuestionType)
                        {
                            case QuestionType.CHOOSE_MULTI_RESULT:
                                var results = question.ResultsIDs.Split(',');
                                var answersUser = answer.Answer.Split(',');
                                if (results.Length == answersUser.Length)
                                {
                                    var isCorrect = true;
                                    for (int i = 0; i < results.Length; i++)
                                    {
                                        if (!(results.Contains(answersUser[i]) && answersUser.Contains(results[i])))
                                        {
                                            isCorrect = false;
                                            break;
                                        }
                                    }
                                    if (isCorrect) numQuestionCorrect += 1;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            case QuestionType.CHOOSE_1_RESULT:
                                if (answer.Answer == question.ResultsIDs)
                                {
                                    numQuestionCorrect += 1;
                                }
                                break;
                            case QuestionType.FILL_ANSWER:
                                var resultsFilAnswer = question.Answers.ToArray();
                                // Nếu trùng bất kì đáp án nào thì break
                                for (int ind = 0; ind < resultsFilAnswer.Length; ind++)
                                {
                                    if (answer.Answer == resultsFilAnswer[ind].Description)
                                    {
                                        numQuestionCorrect += 1;
                                        break;
                                    }
                                }
                                break;
                            default: break;

                        }
                    }
                }
                submission.Point = numQuestionCorrect;
                await _submissionRepository.UpdateAsync(submission.ID, submission);
                _unitOfWork.Commit();
                var submissioDto = _mapper.Map<SubmissionDto>(submission); 
                return submissioDto;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return default;
        }

    }
}
