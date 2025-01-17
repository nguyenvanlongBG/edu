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

        public virtual async Task<int> GetSummaryAsync(List<FilterCondition> filters)
        {
            var query = Records.AsQueryable();
            if (filters != null && filters.Count > 0)
            {
                filters.ForEach(filter =>
                {
                    query = this.ApplyCondition(query, filter);
                });
            }
            var data = await query.ToListAsync();

            return data.Count;
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

            // Lấy thuộc tính của tham số
            var property = Expression.Property(parameter, condition.Field);

            // Xử lý riêng cho toán tử "In"
            if (condition.Operator == FilterOperator.In)
            {
                return BuildInExpression(property, condition.Value);
            }

            // Sử dụng ExpressionBuilder để tạo biểu thức dựa trên toán tử
            return BuildFilterExpression(property, condition.Value, condition.Operator);
        }





        public Expression BuildFilterExpression(
        Expression property,
        object? value,
        FilterOperator filterOperator)
        {
            // Chuyển đổi giá trị sang kiểu của thuộc tính
            var valueCondition = CommonFunction.ConvertToType(value, property.Type);
            var constant = Expression.Constant(valueCondition, property.Type);

            // Tạo biểu thức dựa trên toán tử
            return filterOperator switch
            {
                FilterOperator.Equal => BuildEqualityExpression(property, constant, true),
                FilterOperator.NotEqual => BuildEqualityExpression(property, constant, false),
                FilterOperator.GreaterThan => Expression.GreaterThan(property, constant),
                FilterOperator.LessThan => Expression.LessThan(property, constant),
                FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, constant),
                FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(property, constant),
                FilterOperator.Contains => BuildStringMethodExpression(property, "Contains", constant),
                FilterOperator.StartsWith => BuildStringMethodExpression(property, "StartsWith", constant),
                FilterOperator.EndsWith => BuildStringMethodExpression(property, "EndsWith", constant),
                _ => throw new NotSupportedException($"Operator '{filterOperator}' is not supported.")
            };
        }

        private Expression BuildEqualityExpression(Expression property, Expression constant, bool isEqual)
        {
            // Nếu property là Nullable, sử dụng HasValue và Value
            if (IsNullableType(property.Type))
            {
                var hasValue = Expression.Property(property, "HasValue");
                var valueProperty = Expression.Property(property, "Value");
                var equality = Expression.Equal(valueProperty, constant);

                return isEqual ? Expression.AndAlso(hasValue, equality) : Expression.Not(equality);
            }

            // Trường hợp không phải Nullable
            return isEqual ? Expression.Equal(property, constant) : Expression.NotEqual(property, constant);
        }

        private Expression BuildStringMethodExpression(Expression property, string methodName, Expression constant)
        {
            // Đảm bảo property là kiểu string
            var stringProperty = EnsureString(property);
            var method = typeof(string).GetMethod(methodName, new[] { typeof(string) });

            if (method == null)
                throw new InvalidOperationException($"String method '{methodName}' not found.");

            return Expression.Call(stringProperty, method, constant);
        }

        private bool IsNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
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
