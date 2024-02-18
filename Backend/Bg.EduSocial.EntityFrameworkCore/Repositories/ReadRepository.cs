using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.EntityFrameworkCore;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EFCore.Repositories
{
    public class ReadRepository<TEntity>: IReadRepository<TEntity> where TEntity : class
    {
        protected IUnitOfWork<EduSocialDbContext> _unitOfWork;
        private DbSet<TEntity> _entities;
        protected EduSocialDbContext Context { get; set; }
        public ReadRepository(IUnitOfWork<EduSocialDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Context = unitOfWork.GetContext();
        }
        protected virtual DbSet<TEntity> Records { get {
                if (_entities is not null) return _entities;
                var dbSetProperty = _unitOfWork.GetContext().GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(DbSet<TEntity>))
                .FirstOrDefault();

                if (dbSetProperty != null)
                {
                    return dbSetProperty.GetValue(_unitOfWork.GetContext()) as DbSet<TEntity>;
                }
                return Context.Set<TEntity>();
            } 
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Records.ToList();
        }
        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var result = await Records.FindAsync(id);
            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> FilterAsync()
        {
            var data = await Records.Where(r=> true).ToListAsync();
            return data;
        }
    }
}
