using AutoMapper;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application
{
    public class WriteService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ReadService<TEntity, TEntityDto>, IWriteService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> where TEntity : BaseEntity
    {
        private readonly IWriteRepository<TEntity> _writeRepository;
        public WriteService(IUnitOfWork<EduSocialDbContext> unitOfWork, IWriteRepository<TEntity> writeRepository, IMapper mapper) : base(unitOfWork, (IWriteRepository<TEntity>)writeRepository, mapper)
        {
            _writeRepository = writeRepository;
        }
        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần xóa</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            if (entityCreateDto == null) throw new ArgumentNullException();
            var resultValidate = await this.ValidateBeforeInsert(entityCreateDto);
            int status = 0;
            if (resultValidate)
            {
                var entity = _mapper.Map<TEntity>(entityCreateDto);
                _unitOfWork.BeginTransaction();
                try
                {
                    entity.CreatedBy = "Admin";
                    entity.CreatedDate = DateTime.Now;
                    status = await _writeRepository.InsertAsync(entity);
                    _unitOfWork.SaveChange();
                    _unitOfWork.Commit();
                } catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                } finally
                {
                    _unitOfWork.Dispose();
                }
            }
            return status;
        }

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            if (entityUpdateDto == null) throw new ArgumentNullException();
            var isValid = await ValidateBeforeUpdate(id, entityUpdateDto);
            int status = 0;
            if (isValid)
            {
                _unitOfWork.BeginTransaction();
                try
                {
                    var entity = _mapper.Map<TEntity>(entityUpdateDto);
                    entity.ModifiedBy = "Admin";
                    entity.ModifiedDate = DateTime.Now;
                    status = await _writeRepository.UpdateAsync(id, entity);
                    if (status > 0) {
                        _unitOfWork.SaveChange();
                        _unitOfWork.Commit();
                    }
                } catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                } finally
                {
                    _unitOfWork.Dispose();
                }
            }
            return status;
        }

        /// <summary>
        /// Validate trước khi thêm 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần validate</param>
        /// <returns></returns>
        public virtual async Task<bool> ValidateBeforeInsert(TEntityCreateDto entityCreateDto)
        {
            return true;
        }
        /// <summary>
        /// Validate trước khi cập nhật dữ liệu cho bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Đối tượng dữ liệu cần validate</param>
        /// <returns></returns>
        public virtual async Task<bool> ValidateBeforeUpdate(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            return true;
        }
    }
}
