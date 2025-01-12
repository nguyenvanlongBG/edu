using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Shared.ModelState;
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

        public override async Task BeforeCommitAsync(QuestionEditDto question)
        {
            if (question == null) return;

            var questionService = _serviceProvider.GetRequiredService<IQuestionService>();
            var optionService = _serviceProvider.GetRequiredService<IOptionService>();
            var questionTestService = _serviceProvider.GetRequiredService<IQuestionTestService>();
            
            var optionsHandle = question?
                .options
                .Where(option => option.State == ModelState.Insert ||
                                option.State == ModelState.Update ||
                                option.State == ModelState.Delete)
                .ToList();
            var resultsHandle = question?
                .results
                .Where(r => r.State == ModelState.Insert ||
                                r.State == ModelState.Update ||
                                r.State == ModelState.Delete)
                .ToList();

            var tasks = new List<Task>();

            if (optionsHandle?.Count > 0)
            {
                tasks.Add(optionService.SubmitManyAsync(optionsHandle));
            }
            if (resultsHandle?.Count > 0)
            {
                var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();
                tasks.Add(resultQuestionService.SubmitManyAsync(resultsHandle));
            }
            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks);
            }
        }

        public async Task<List<QuestionEntity>> GetRandomQuestion(ChapterGenQuestionConfig param)
        {
            return await _repo.GetRandomQuestion(param.chapter_id, param.recognition, param.comprehension, param.application, param.advanced_application);
        }

        public async Task<List<QuestionEditDto>> InsertQuestionLibrary(List<QuestionEditDto> questions)
        {
            var questionsHandle = questions?
                .Where(question => question.State == ModelState.Insert)
                .ToList();

            var optionsHandle = questions?
                .SelectMany(question => question.options)
                .Where(option => option.State == ModelState.Insert ||
                                option.State == ModelState.Update ||
                                option.State == ModelState.Delete)
                .ToList();
            var resultsHandle = questions?
                .SelectMany(question => question.results)
                .Where(r => r.State == ModelState.Insert ||
                                r.State == ModelState.Update ||
                                r.State == ModelState.Delete)
                .ToList();

            var tasks = new List<Task>();

            if (questionsHandle?.Count > 0)
            {
                tasks.Add(this.SubmitManyAsync(questionsHandle));
            }
            if (optionsHandle?.Count > 0)
            {
                var optionService = _serviceProvider.GetRequiredService<IOptionService>();
                tasks.Add(optionService.SubmitManyAsync(optionsHandle));
            }
            if (resultsHandle?.Count > 0)
            {
                var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();
                tasks.Add(resultQuestionService.SubmitManyAsync(resultsHandle));
            }
            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks);
            }

            return questions;
        }

        public async Task<List<QuestionDto>> PagingQuestionLibraryAsync(PagingParam pagingParam)
        {
            var questions = await _repo.GetPagingAsync(pagingParam.skip, pagingParam.take, pagingParam.filters);
            if (!(questions?.Count > 0)) return default;
            var optionService = _serviceProvider.GetRequiredService<IOptionService>();
            var resultQuestionService = _serviceProvider.GetRequiredService<IResultQuestionService>();

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
            MapOptionsToQuestion(questionsReturn, options);
            MapResultsToQuestion(questionsReturn, results);
            return questionsReturn;
        }
        public void MapOptionsToQuestion(List<QuestionDto> questions, List<OptionDto> options)
        {
            if (questions?.Count > 0 && options?.Count > 0)
            {
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
            if (questions.Count > 0 && results?.Count > 0)
            {
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
    }
}
