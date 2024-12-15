using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.EntityFrameworkCore;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EFCore.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
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
        public virtual List<TEntity> GetAll()
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

        public virtual async Task<List<TEntity>> FilterAsync(List<FilterCondition> filterConditions)
        {
            var query = Records.AsQueryable();
            if (filterConditions != null && filterConditions.Count > 0)
            {
                filterConditions.ForEach(filter =>
                {
                    query = this.ApplyCondition(query, filter);
                });
            }
            var data = await query.ToListAsync();
            return data;
        }
        public virtual async Task<List<TEntity>> GetPagingAsync(int skip, int take, List<FilterCondition> filters)
        {
            var query = Records.AsQueryable();
            if (filters != null && filters.Count > 0)
            {
                filters.ForEach(filter =>
                {
                    query = this.ApplyCondition(query, filter);
                });
            }
            var data = await query.Skip(skip).Take(take).ToListAsync();
            return data;
        }

        private IQueryable<T> ApplyCondition<T>(IQueryable<T> query, FilterCondition condition)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, condition.Field);

            // Chuyển đổi giá trị constant sang kiểu dữ liệu của property
            var constant = Expression.Constant(
                Convert.ChangeType(condition.Value, property.Type)
            );

            Expression? comparison = condition.Operator switch
            {
                FilterOperator.Equal => Expression.Equal(property, constant),
                FilterOperator.NotEqual => Expression.NotEqual(property, constant),
                FilterOperator.GreaterThan => Expression.GreaterThan(property, constant),
                FilterOperator.LessThan => Expression.LessThan(property, constant),
                FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, constant),
                FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(property, constant),
                FilterOperator.Contains => Expression.Call(
                    EnsureString(property),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    constant
                ),
                FilterOperator.StartsWith => Expression.Call(
                    EnsureString(property),
                    typeof(string).GetMethod("StartsWith", new[] { typeof(string) }),
                    constant
                ),
                FilterOperator.EndsWith => Expression.Call(
                    EnsureString(property),
                    typeof(string).GetMethod("EndsWith", new[] { typeof(string) }),
                    constant
                ),
                FilterOperator.In => Expression.Call(
                    typeof(Enumerable),
                    "Contains",
                    new Type[] { property.Type },
                    Expression.Constant(((IEnumerable)condition.Value).Cast<object>().ToList()),
                    property
                ),
                _ => throw new NotSupportedException($"Operator '{condition.Operator}' is not supported.")
            };

            var lambda = Expression.Lambda<Func<T, bool>>(comparison!, parameter);

            return query.Where(lambda);
        }

        // Hàm đảm bảo rằng property là kiểu string
        private static Expression EnsureString(Expression property)
        {
            if (property.Type != typeof(string))
            {
                throw new InvalidOperationException($"Property '{property}' must be a string for this operator.");
            }
            return property;
        }
    }
}
