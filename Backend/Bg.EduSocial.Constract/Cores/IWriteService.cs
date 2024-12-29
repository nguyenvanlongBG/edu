using Bg.EduSocial.Domain.Cores;
using System.Data;

namespace Bg.EduSocial.Constract
{
    public interface IWriteService<TEntity, TEntityDto, TEntityEditDto> : IReadService<TEntity, TEntityDto> where TEntity : BaseEntity
    {
        Task<TEntityEditDto> InsertAsync(TEntityEditDto entityInsertDto);
        Task<int> InsertManyAsync(List<TEntityEditDto> lstDto);

        Task<int> UpdateManyAsync(List<TEntityEditDto> lstDto);
        Task<int> DeleteManyAsync(List<TEntityEditDto> lstDto);

        Task HandleBeforeSaveAsync(TEntityEditDto entityHandle);

        Task<int> SubmitManyAsync(List<TEntityEditDto> lstDto);

        Task<int> InsertManyAsync( IDbTransaction transaction, List<TEntityEditDto> lstDto);

        Task<int> UpdateManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto);
        Task<int> DeleteManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto);

        Task<int> SubmitManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto);


        Task<TEntityEditDto> UpdateAsync(TEntityEditDto entityUpdateDto);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<bool> ValidateBeforeInsert(TEntityEditDto entityInsertDto);
        Task<bool> ValidateBeforeUpdate(TEntityEditDto entityUpdateDto);
    }
}
