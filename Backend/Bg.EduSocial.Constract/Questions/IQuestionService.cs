using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;

namespace Bg.EduSocial.Constract.Questions
{
    public interface IQuestionService: IWriteService<QuestionEntity, QuestionDto, QuestionEditDto>
    {
        Task HandleAddQuestion(List<QuestionDto> questions);
        Task<List<QuestionEntity>> GetRandomQuestion(ChapterGenQuestionConfig param);
        Task<List<QuestionEditDto>> InsertQuestionLibrary(List<QuestionEditDto> questions);
        Task<List<QuestionDto>> PagingQuestionLibraryAsync(PagingParam pagingParam);
        void MapOptionsToQuestion(List<QuestionDto> questions, List<OptionDto> options);
        void MapResultsToQuestion(List<QuestionDto> questions, List<ResultQuestionDto> results);
    }
}
