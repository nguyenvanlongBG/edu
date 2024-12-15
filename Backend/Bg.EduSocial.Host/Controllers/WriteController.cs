using Bg.EduSocial.Constract;
using Bg.EduSocial.Domain.Cores;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    public class WriteController<TService, TEntity, TEntityDto, TEntityEditDto> : ReadController<TService,TEntity, TEntityDto> where TService : IWriteService<TEntity, TEntityDto, TEntityEditDto> where TEntity : BaseEntity
    {
        public WriteController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpPost()]
        public virtual async Task<IActionResult> InsertAsync(TEntityEditDto entityInsertDto)
        {
            try
            {
                var result = await _service.InsertAsync(entityInsertDto);
                return StatusCode(201, result);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
