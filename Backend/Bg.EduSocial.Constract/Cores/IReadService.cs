using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Domain.Cores;

namespace Bg.EduSocial.Constract
{
    public interface IReadService<TEntity, TEntityDto>
    {
        Task<T> GetById<T>(Guid id);
        Task<List<TEntityDto>> FilterAsync(List<FilterCondition> filters);
        Task<List<T>> FilterAsync<T>(List<FilterCondition> filters);

        Task<List<TEntityDto>> GetPagingAsync(PagingParam pagingParam);
        Task<int> GetSummaryData(PagingParam pagingParam);

    }
}
