using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Cores;
using Bg.EduSocial.Domain.Shared.ModelState;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bg.EduSocial.Application
{
    public class WriteService<TRepository,TEntity, TEntityDto, TEntityEditDto> : ReadService<TRepository, TEntity, TEntityDto>, IWriteService<TEntity, TEntityDto, TEntityEditDto> 
        where TRepository: IWriteRepository<TEntity>
        where TEntity: BaseEntity
        where TEntityEditDto: TEntity,IRecordState
    {
        public WriteService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
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
        /// Xử lý trước khi thêm mới
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        public virtual async Task HandleBeforeSaveAsync(TEntityEditDto entityHandle)
        {
           if (entityHandle != null && entityHandle.State == ModelState.Insert)
            {
                entityHandle.created_date = DateTime.Now;
                entityHandle.modified_date = DateTime.Now;
            } else if (entityHandle != null && entityHandle.State == ModelState.Update)
            {
                entityHandle.modified_date = DateTime.Now;
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<TEntityEditDto> InsertAsync(TEntityEditDto entityCreateDto)
        {
            if (entityCreateDto == null) throw new ArgumentNullException();
            var resultValidate = await this.ValidateBeforeInsert(entityCreateDto);
            int status = 0;
            if (!resultValidate) throw new ArgumentNullException();

            await HandleBeforeSaveAsync(entityCreateDto);
            var entity = _mapper.Map<TEntity>(entityCreateDto);
            _unitOfWork.BeginTransaction();
            try
            {
                status = await _repo.InsertAsync(entity);
                _unitOfWork.SaveChange();
                _unitOfWork.Commit();
            } catch (Exception ex)
            {
                _unitOfWork.Rollback();
            } finally
            {
                _unitOfWork.Dispose();
            }
            return entityCreateDto;
        }

        public async Task<int> InsertManyAsync(List<TEntityEditDto> lstDto)
        {
            var entitíe = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(lstDto);
            if (entitíe?.Count > 0)
            {
                return await _repo.InsertManyAsync(entitíe);
            }
            return 0;
        }
        public async Task<int> UpdateManyAsync(List<TEntityEditDto> lstDto)
        {
            var entitíe = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(lstDto);
            if (entitíe?.Count > 0)
            {
                return await _repo.UpdateManyAsync(entitíe);
            }
            return 0;
        }

        public async Task<int> DeleteManyAsync(List<TEntityEditDto> lstDto)
        {
            var entitíe = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(lstDto);
            if (entitíe?.Count > 0)
            {
                return await _repo.DeleteManyAsync(entitíe);
            }
            return 0;
        }

        public async Task<int> SubmitManyAsync(List<TEntityEditDto> lstDto)
        {
            var dtoInsert = lstDto.Where(item => item.State == ModelState.Insert).ToList();
            var dtoUpdate = lstDto.Where(item => item.State == ModelState.Update).ToList();
            var dtoDelete = lstDto.Where(item => item.State == ModelState.Update).ToList();

            var entitiedInsert = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(dtoInsert);
            var entitiedUpdate = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(dtoUpdate);
            var entitiedDelete = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(dtoDelete);

            using var transaction = await _unitOfWork.GetTransaction();
            var resultInsert = await _repo.InsertManyAsync(entitiedInsert);
            var resultUpdate = await _repo.UpdateManyAsync(entitiedUpdate);
            var resultDelete = await _repo.DeleteManyAsync(entitiedDelete);
            return (resultInsert + resultUpdate + resultDelete);
        }

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<TEntityEditDto> UpdateAsync(Guid id, TEntityEditDto entityUpdateDto)
        {
            if (entityUpdateDto == null) throw new ArgumentNullException();
            var isValid = await ValidateBeforeUpdate(id, entityUpdateDto);
            int status = 0;
            if (!isValid) throw new ArgumentNullException();
            await HandleBeforeSaveAsync(entityUpdateDto);
            _unitOfWork.BeginTransaction();
            try
            {
                var entity = _mapper.Map<TEntity>(entityUpdateDto);
                status = await _repo.UpdateAsync(id, entity);
                if (status > 0)
                {
                    _unitOfWork.SaveChange();
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return entityUpdateDto;
        }


        /// <summary>
        /// Validate trước khi thêm 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần validate</param>
        /// <returns></returns>
        public virtual async Task<bool> ValidateBeforeInsert(TEntityEditDto entityCreateDto)
        {
            return true;
        }

        /// <summary>
        /// Validate trước khi cập nhật dữ liệu cho bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Đối tượng dữ liệu cần validate</param>
        /// <returns></returns>
        public virtual async Task<bool> ValidateBeforeUpdate(Guid id, TEntityEditDto entityUpdateDto)
        {
            return true;
        }

        public async Task<int> InsertManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto)
        {
            var entitíe = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(lstDto);
            if (entitíe?.Count > 0)
            {
                return await _repo.InsertManyAsync(entitíe);
            }
            return 0;
        }

        public async Task<int> UpdateManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto)
        {
            var entitíe = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(lstDto);
            if (entitíe?.Count > 0)
            {
                return await _repo.UpdateManyAsync(entitíe);
            }
            return 0;
        }

        public async Task<int> DeleteManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto)
        {
            var entitíe = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(lstDto);
            if (entitíe?.Count > 0)
            {
                return await _repo.DeleteManyAsync(entitíe);
            }
            return 0;
        }

        public async Task<int> SubmitManyAsync(IDbTransaction transaction, List<TEntityEditDto> lstDto)
        {
            var dtoInsert = lstDto.Where(item => item.State == ModelState.Insert).ToList();
            var dtoUpdate = lstDto.Where(item => item.State == ModelState.Update).ToList();
            var dtoDelete = lstDto.Where(item => item.State == ModelState.Update).ToList();

            var entitiedInsert = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(dtoInsert);
            var entitiedUpdate = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(dtoUpdate);
            var entitiedDelete = _mapper.Map<List<TEntityEditDto>, List<TEntity>>(dtoDelete);

            var resultInsert = await _repo.InsertManyAsync(entitiedInsert);
            var resultUpdate = await _repo.UpdateManyAsync(entitiedUpdate);
            var resultDelete = await _repo.DeleteManyAsync(entitiedDelete);
            return (resultInsert + resultUpdate + resultDelete);
        }
    }
}
