using AutoMapper;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bg.EduSocial.Application
{
    public class TestService : WriteService<ITestRepository,TestEntity, TestDto, TestEditDto>, ITestService
    {
        public TestService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<QuestionDto>> GetQuestionOfTest(Guid testId)
        {
            var questions = await _repo.GetQuestionOfTest(testId);
            var optionService = _serviceProvider.GetRequiredService<IOptionService>();
            var questionIds = questions.Select(question => question.question_id).ToList();
            var filterOptionOfTest = new FilterCondition
            {
                Field = "question_id",
                Operator = FilterOperator.Equal,
                Value = questionIds
            };
            var options = await optionService.FilterAsync(new List<FilterCondition> { filterOptionOfTest });
            return _mapper.Map<List<QuestionDto>>(questions);
        }

        public Task<List<QuestionDto>> ReadQuestionFromFile(IFormFile file, string regexStr)
        {
            //EditorFunction.GetLatexFromFile(file, regexStr);
            return default;
        }
    }
}
