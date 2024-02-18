using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Cores
{
    public interface IWriteRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(Guid ID, TEntity entity);
        Task<int> InsertManyAsync(TEntity[] entities);
        Task<int> UpdateManyAsync(TEntity[] entities);
    }
}
