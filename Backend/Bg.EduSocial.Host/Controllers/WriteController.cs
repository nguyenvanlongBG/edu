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
            var result = await _service.InsertAsync(entityInsertDto);
            return StatusCode(201, result);
        }
        [HttpPut()]
        public virtual async Task<IActionResult> UpdateAsync(TEntityEditDto entityUpdateDto)
        {
            var result = await _service.UpdateAsync(entityUpdateDto);
            return StatusCode(201, result);
        }
    }
}
