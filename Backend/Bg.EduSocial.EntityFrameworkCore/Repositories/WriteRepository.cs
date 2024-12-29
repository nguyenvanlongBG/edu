using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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
            if (entities == null || entities.Count == 0) return 0;
            var keyProperty = typeof(TEntity).GetProperties()
        .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (keyProperty == null)
            {
                throw new InvalidOperationException($"Class {typeof(TEntity).Name} không có thuộc tính được đánh dấu [Key].");
            }
            var keyPropertyName = keyProperty.Name; // Lấy tên của thuộc tính khóa
            var entitiesToDelete = new List<TEntity>();
            foreach (var entityUpdate in entities)
            {
                var keyValue = entityUpdate.GetType().GetProperty(keyPropertyName).GetValue(entityUpdate);
                var entity = await Records.FindAsync(keyValue);
                if (entity != null)
                {
                    entitiesToDelete.Add(entity);
                }
                
            }
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
            if (entities == null || !entities.Any()) return 0;

            var keyProperty = typeof(TEntity).GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (keyProperty == null)
                throw new InvalidOperationException($"Class {typeof(TEntity).Name} không có thuộc tính được đánh dấu [Key].");

            var keyPropertyName = keyProperty.Name; // Lấy tên của thuộc tính khóa

            foreach (var entityUpdate in entities)
            {
                var keyValue = entityUpdate.GetType().GetProperty(keyPropertyName).GetValue(entityUpdate);
                var entity = Records.FindAsync(keyValue);
                if (entity != null)
                {
                    Context.Entry(entity).CurrentValues.SetValues(entityUpdate);
                }
            }

            return await Context.SaveChangesAsync();
        }
    }
}
