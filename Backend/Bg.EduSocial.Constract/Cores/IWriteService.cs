using System.Data;

namespace Bg.EduSocial.Constract
{
    public interface IWriteService<TEntity, TEntityDto, TEntityEditDto> : IReadService<TEntity, TEntityDto>
    {
        Task<int> InsertAsync(TEntityEditDto entityInsertDto);
        Task<int> InsertManyAsync(List<TEntityEditDto> lstDto);

        Task<int> UpdateManyAsync(List<TEntityEditDto> lstDto);
        Task<int> DeleteManyAsync(List<TEntityEditDto> lstDto);

        Task<int> SubmitManyAsync(List<TEntityEditDto> lstDto);

        Task<int> InsertManyAsync( IDbTransaction transaction, List<TEntityEditDto> lstDto);

        Task<int> UpdateManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto);
        Task<int> DeleteManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto);

        Task<int> SubmitManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto);


        Task<int> UpdateAsync(Guid id, TEntityEditDto entityUpdateDto);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<bool> ValidateBeforeInsert(TEntityEditDto entityInsertDto);
        Task<bool> ValidateBeforeUpdate(Guid id, TEntityEditDto entityUpdateDto);
    }
}
