using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            var result = await Records.AddAsync(entity);
            if (result is not null) return 1;
            return 0;
        }

        public virtual async Task<int> InsertManyAsync(TEntity[] entities)
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

        public virtual async Task<int> UpdateManyAsync(TEntity[] entities)
        {
            var targetIDs = new List<Guid>();
            foreach (var entity in entities)
            {
                targetIDs.Add(entity.ID);
            }
            var entitiesToUpdate = Records.Where(entity => targetIDs.Contains(entity.ID)).ToList();
            foreach (var entityUpdate in entitiesToUpdate)
            {
                var entity = entities.First(entity => entity.ID == entityUpdate.ID);
                if (entity != null)
                {
                    Context.Entry(entityUpdate).CurrentValues.SetValues(entity);
                }
            }
            return Context.SaveChanges();
        }
    }
}
