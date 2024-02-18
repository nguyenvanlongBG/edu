using Bg.EduSocial.Constract.Cores;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    public class WriteController<TEntity, TEntityDto, TEntityInsertDto, TEntityUpdateDto> : ReadController<TEntity, TEntityDto>
    {
        private readonly IWriteService<TEntity, TEntityDto, TEntityInsertDto, TEntityUpdateDto> _writeService;
        public WriteController(IWriteService<TEntity, TEntityDto, TEntityInsertDto, TEntityUpdateDto> writeService) : base(writeService)
        {
            _writeService = writeService;
        }
        [HttpPost()]
        public virtual async Task<IActionResult> InsertAsync(TEntityInsertDto entityInsertDto)
        {
            try
            {
                var status = _writeService.InsertAsync(entityInsertDto);
                return StatusCode(201, status);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
