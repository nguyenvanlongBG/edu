using Bg.EduSocial.Constract;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    public class WriteController<TService, TEntity, TEntityDto, TEntityEditDto> : ReadController<TService,TEntity, TEntityDto> where TService : IWriteService<TEntity, TEntityDto, TEntityEditDto>
    {
        public WriteController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpPost()]
        public virtual async Task<IActionResult> InsertAsync(TEntityEditDto entityInsertDto)
        {
            try
            {
                var status = _service.InsertAsync(entityInsertDto);
                return StatusCode(201, status);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
