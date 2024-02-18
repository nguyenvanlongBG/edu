using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Cores
{
    public interface IWriteService<TEntity, TEntityDto, TEntityInsertDto, TEntityUpdateDto> : IReadService<TEntity, TEntityDto>
    {
        Task<int> InsertAsync(TEntityInsertDto entityInsertDto);
        Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<bool> ValidateBeforeInsert(TEntityInsertDto entityInsertDto);
        Task<bool> ValidateBeforeUpdate(Guid id, TEntityUpdateDto entityUpdateDto);
    }
}
