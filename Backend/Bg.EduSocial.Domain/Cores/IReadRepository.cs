using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Cores
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> FilterAsync();
        Task<TEntity> GetById(Guid id);
    }
}
