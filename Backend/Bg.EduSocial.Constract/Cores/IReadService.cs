using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Domain.Cores;

namespace Bg.EduSocial.Constract
{
    public interface IReadService<TEntity, TEntityDto>
    {
        Task<TEntityDto> GetById(Guid id);
        Task<List<TEntityDto>> FilterAsync(List<FilterCondition> filters);
        Task<List<TEntityDto>> GetPagingAsync(PagingParam pagingParam);
    }
}
