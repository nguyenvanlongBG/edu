using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Microsoft.Extensions.DependencyInjection;

namespace Bg.EduSocial.Application
{
    public class QuestionService:  WriteService<IQuestionRepository,QuestionEntity, QuestionDto, QuestionEditDto>, IQuestionService
    {
        public QuestionService(IServiceProvider serviceProvider): base(serviceProvider)
        { 
        }
        public override async Task HandleBeforeSaveAsync(QuestionEditDto questionEditDto)
        {
            if (questionEditDto != null && questionEditDto.object_content != null)
            {
                var convertService = _serviceProvider.GetRequiredService<IConvertService>();
                questionEditDto.content = convertService.Serialize(questionEditDto.object_content);
            }
            Task.CompletedTask.Wait();
        }
        public override async Task<List<QuestionDto>> GetPagingAsync(PagingParam pagingParam)
        {
            var data = await _repo.GetPagingAsync(pagingParam.skip, pagingParam.take, pagingParam.filters);
            if (data?.Count > 0)
            {
                var dataDto = _mapper.Map<List<QuestionEntity>, List<QuestionDto>>(data);
                var optionService = _serviceProvider.GetRequiredService<IOptionService>();
                var questionIds = data.Select(question => question.question_id).ToList();
                var filterOptionOfQuestions = new FilterCondition
                {
                    Field = "question_id",
                    Operator = FilterOperator.In,
                    Value = questionIds
                };
                var options = await optionService.FilterAsync(new List<FilterCondition> { filterOptionOfQuestions });
                if (options?.Count > 0)
                {
                    dataDto.ForEach(question =>
                    {
                        var optionsOfQuestion = options.Where(option => option.question_id == question.question_id).ToList();
                        question.options = optionsOfQuestion;
                    });
                }
                return dataDto;
            }
            return default;
        }
        public async Task HandleAddQuestion(List<QuestionDto> questions)
        {
            var questionEntities = _mapper.Map<List<QuestionDto>, List<QuestionEntity>>(questions);
            var options = questions.SelectMany(q => q.options).ToList();
            var optionEnties = new List<OptionEditDto>();
            if (options != null &&  options.Count > 0)
            {
                optionEnties = _mapper.Map<List<OptionDto>, List< OptionEditDto>>(options);
            }
            await _repo.InsertManyAsync(questionEntities);
            var optionService = _serviceProvider.GetRequiredService<IOptionService>();
            if (optionEnties?.Count > 0)
            {
                await optionService.InsertManyAsync(optionEnties);
            }
        }
    }
}
