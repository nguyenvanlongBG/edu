using AutoMapper;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bg.EduSocial.Application
{
    public class ReadService<TRepository, TEntity, TEntityDto> : IReadService<TEntity, TEntityDto>
        where TRepository : IReadRepository<TEntity> // TRepository phải triển khai IReadRepository<TEntity>
    where TEntity : class // TEntity phải là một lớp
    {
        protected readonly IUnitOfWork<EduSocialDbContext> _unitOfWork;
        protected readonly TRepository _repo;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _serviceProvider;
        public ReadService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork<EduSocialDbContext>>();
            _repo = _serviceProvider.GetRequiredService<TRepository>();
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }
        public virtual async Task<TEntityDto?> GetById(Guid id)
        {
            var entity = await _repo.GetById(id);
            if (entity != null)
            {
                var entityDto = _mapper.Map<TEntityDto>(entity);
                return entityDto;
            }
            return default;
        }

        public async Task<List<TEntityDto>> FilterAsync(List<FilterCondition> filters)
        {
            var data = await _repo.FilterAsync(filters);
            if (data != null)
            {
                var dataDto = _mapper.Map<List<TEntity>, List<TEntityDto>>(data);
                return dataDto;
            }
            return default;
        }
        public virtual async Task<List<TEntityDto>> GetPagingAsync(PagingParam pagingParam)
        {
            var data = await _repo.GetPagingAsync(pagingParam.skip, pagingParam.take, pagingParam.filters);
            if (data != null)
            {
                var dataDto = _mapper.Map<List<TEntity>, List<TEntityDto>>(data);
                return dataDto;
            }
            return default;
        }
    }
}
