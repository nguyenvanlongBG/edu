using Bg.EduSocial.Constract.Cores;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    public abstract class ReadController<TEntity, TEntityDto>: ControllerBase
    {
        protected readonly IReadService<TEntity, TEntityDto> _readService;

        public ReadController(IReadService<TEntity, TEntityDto> readService)
        {
            _readService = readService;
        }
        [HttpGet("{id}")]
        public virtual async Task<IActionResult?> GetById(Guid id) {
            try
            {
                var response = await _readService.GetById(id);
                return Ok(response);
            } catch (Exception ex)
            {
                return Ok(default);
            }
        }
        [HttpGet("filter")]
        public virtual async Task<IActionResult?> FilterAsync(Guid id)
        {
            try
            {
                var response = await _readService.FilterAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(default);
            }
        }
    }
}
