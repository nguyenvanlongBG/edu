using AutoMapper;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application
{
    public class TestService : WriteService<Test, TestDto, TestInsertDto, TestUpdateDto>, ITestService
    {
        private ITestRepository _testRepository;
        private IQuestionRepository _questionRepository;
        private IQuestionTestRepository _questionTestRepository;

        public TestService(IUnitOfWork<EduSocialDbContext> unitOfWork, ITestRepository testRepository, IQuestionRepository questionRepository, IQuestionTestRepository questionTestRepository, IMapper mapper) : base(unitOfWork, testRepository, mapper)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _questionTestRepository = questionTestRepository;
        }
        public override async Task<int> InsertAsync(TestInsertDto testDto)
        {
            try
            {
                var test = _mapper.Map<Test>(testDto);
                var statusTest = await _testRepository.InsertAsync(test);
                var questions = test.Questions;
                var questionsTest = new List<QuestionTest>();
                var statusAddQuestions = await _questionRepository.InsertManyAsync(questions.ToArray());
                foreach (Question question in questions)
                {
                    var questionTest = new QuestionTest { QuestionID = question.ID, TestID = test.ID };
                    questionsTest.Add(questionTest);
                }
                var statusAddQuestionsTest = await _questionTestRepository.InsertManyAsync(questionsTest.ToArray());
                if (statusTest == 1 && statusAddQuestions == questions.Count && statusAddQuestionsTest == questionsTest.Count)
                {
                    _unitOfWork.SaveChange();
                    _unitOfWork.Commit();
                    return 1;
                } else
                {
                    _unitOfWork.Rollback();
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }
            finally { _unitOfWork.Dispose(); }
            return 0;
        }
    }
}
