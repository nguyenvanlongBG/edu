using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Cores
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> FilterAsync(List<FilterCondition> filters);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetPagingAsync(int skip, int take, List<FilterCondition> filters);
    }
}
