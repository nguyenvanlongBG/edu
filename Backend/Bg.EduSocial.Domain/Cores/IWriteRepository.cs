namespace Bg.EduSocial.Domain.Cores
{
    public interface IWriteRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(Guid ID, TEntity entity);
        Task<int> InsertManyAsync(List<TEntity> entities);
        Task<int> UpdateManyAsync(List<TEntity> entities);
        Task<int> DeleteManyAsync(List<TEntity> entities);

    }
}
