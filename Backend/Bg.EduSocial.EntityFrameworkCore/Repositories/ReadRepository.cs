using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.EntityFrameworkCore;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Bg.EduSocial.Helper.Commons;
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
            var expression = BuildExpression(parameter, condition);

            if (expression != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(expression, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        private Expression? BuildExpression(ParameterExpression parameter, FilterCondition condition)
        {
            // Nếu có SubConditions, xử lý logic AND/OR
            if (condition.SubConditions?.Any() == true)
            {
                Expression? combined = null;

                foreach (var subCondition in condition.SubConditions)
                {
                    var subExpression = BuildExpression(parameter, subCondition);

                    if (subExpression != null)
                    {
                        combined = combined == null
                            ? subExpression
                            : condition.LogicalOperator == LogicalOperator.AND
                                ? Expression.AndAlso(combined, subExpression)
                                : Expression.OrElse(combined, subExpression);
                    }
                }

                return combined;
            }

            var property = Expression.Property(parameter, condition.Field);

            // Chuyển đổi giá trị cho các toán tử khác
            if (condition.Operator == FilterOperator.In)
            {
                return BuildInExpression(property, condition.Value);
            }

            var valueCondition = CommonFunction.ConvertToType(condition.Value, property.Type);
            var constant = Expression.Constant(valueCondition);

            return condition.Operator switch
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
                _ => throw new NotSupportedException($"Operator '{condition.Operator}' is not supported.")
            };
        }

        private Expression BuildInExpression(MemberExpression property, object? value)
        {
            if (value is IEnumerable enumerable)
            {
                // Lấy kiểu dữ liệu của property
                var elementType = property.Type;

                // Chuyển đổi các giá trị trong danh sách sang kiểu tương ứng
                var convertedValues = enumerable.Cast<object>()
                    .Select(v => Convert.ChangeType(v, elementType))
                    .ToList();

                // Chuyển các giá trị đã chuyển đổi thành List<elementType>
                var listType = typeof(List<>).MakeGenericType(elementType);
                var resultList = Activator.CreateInstance(listType);

                // Dùng Reflection để thêm từng phần tử vào danh sách
                foreach (var item in convertedValues)
                {
                    listType.GetMethod("Add").Invoke(resultList, new[] { item });
                }

                // Tạo mảng hằng số chứa danh sách giá trị
                var constantArray = Expression.Constant(resultList, typeof(List<>).MakeGenericType(elementType));
                // Lấy phương thức Contains phù hợp với kiểu phần tử
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(elementType);

                // Tạo biểu thức Contains
                return Expression.Call(containsMethod, constantArray, property);
            }

            throw new ArgumentException("Value for 'In' operator must be an IEnumerable.");
        }


        private Expression EnsureString(Expression expression)
        {
            return expression.Type == typeof(string)
                ? expression
                : Expression.Call(expression, typeof(object).GetMethod("ToString")!);
        }

    }
}
