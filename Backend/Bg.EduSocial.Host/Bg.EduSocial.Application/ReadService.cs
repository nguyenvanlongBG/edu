using AutoMapper;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application
{
    public class ReadService<TEntity, TEntityDto> : IReadService<TEntity, TEntityDto> where TEntity : class
    {
        protected readonly IUnitOfWork<EduSocialDbContext> _unitOfWork;
        private readonly IReadRepository<TEntity> _readRepository;
        protected readonly IMapper _mapper;
        public ReadService(IUnitOfWork<EduSocialDbContext> unitOfWork, IReadRepository<TEntity> readRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _readRepository = readRepository;
            _mapper = mapper;
        }
        public virtual async Task<TEntityDto?> GetById(Guid id)
        {
            var entity = await _readRepository.GetById(id);
            if (entity != null)
            {
                var entityDto = _mapper.Map<TEntityDto>(entity);
                return entityDto;
            }
            return default;
        }

        public async Task<IEnumerable<TEntityDto>> FilterAsync()
        {
            var data = await _readRepository.FilterAsync();
            if (data != null)
            {
                var dataDto = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDto>>(data);
                return dataDto;
            }
            return default;
        }
    }
}
