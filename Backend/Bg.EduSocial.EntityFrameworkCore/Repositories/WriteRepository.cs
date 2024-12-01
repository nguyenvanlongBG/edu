using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EFCore.Repositories
{
    public abstract class WriteRepository<TEntity> : ReadRepository<TEntity>, IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        protected WriteRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<int> DeleteManyAsync(List<TEntity> entities)
        {
            var keyProperty = typeof(TEntity).GetProperties()
        .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (keyProperty == null)
            {
                throw new InvalidOperationException($"Class {typeof(TEntity).Name} không có thuộc tính được đánh dấu [Key].");
            }

            // Lấy giá trị từ thuộc tính [Key]
            var targetIDs = new List<object>();
            foreach (var entity in entities)
            {
                var keyValue = keyProperty.GetValue(entity);
                if (keyValue != null)
                {
                    targetIDs.Add(keyValue);
                }
            }

            // Lọc các bản ghi trong `Records` dựa trên giá trị của thuộc tính [Key]
            var entitiesToDelete = Records
                .Where(entity => targetIDs.Contains(keyProperty.GetValue(entity)))
                .ToList();
            Records.RemoveRange(entitiesToDelete);
            return await Context.SaveChangesAsync();
        }

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            var result = await Records.AddAsync(entity);
            if (result is not null) return 1;
            return 0;
        }

        public virtual async Task<int> InsertManyAsync(List<TEntity> entities)
        {
            await Records.AddRangeAsync(entities);
            return Context.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            var entityToUpdate = await Records.FindAsync(id);
            if (entityToUpdate is not null)
            {
                Context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                return Context.SaveChanges();
            }
            return 0;
        }

        public virtual async Task<int> UpdateManyAsync(List<TEntity> entities)
        {
            var keyProperty = typeof(TEntity).GetProperties()
        .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (keyProperty == null)
            {
                throw new InvalidOperationException($"Class {typeof(TEntity).Name} không có thuộc tính được đánh dấu [Key].");
            }

            // Lấy giá trị từ thuộc tính [Key]
            var targetIDs = new List<object>();
            foreach (var entity in entities)
            {
                var keyValue = keyProperty.GetValue(entity);
                if (keyValue != null)
                {
                    targetIDs.Add(keyValue);
                }
            }

            // Lọc các bản ghi trong `Records` dựa trên giá trị của thuộc tính [Key]
            var entitiesToUpdate = Records
                .Where(entity => targetIDs.Contains(keyProperty.GetValue(entity)))
                .ToList();

            // Cập nhật giá trị
            foreach (var entityUpdate in entitiesToUpdate)
            {
                var entity = entities.First(e =>
                    keyProperty.GetValue(e).Equals(keyProperty.GetValue(entityUpdate))
                );
                if (entity != null)
                {
                    Context.Entry(entityUpdate).CurrentValues.SetValues(entity);
                }
            }

            return await Context.SaveChangesAsync();
        }
    }
}
