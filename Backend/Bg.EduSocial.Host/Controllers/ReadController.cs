using Bg.EduSocial.Constract;
using Bg.EduSocial.Constract.Base;
using Bg.EduSocial.Domain.Cores;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    public abstract class ReadController<TService, TEntity, TEntityDto>: ControllerBase where TService : IReadService<TEntity, TEntityDto>
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly TService _service;

        public ReadController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _service = serviceProvider.GetRequiredService<TService>();
        }
        [HttpGet("{id}")]
        public virtual async Task<IActionResult?> GetById(Guid id) {
            try
            {
                var response = await _service.GetById<TEntityDto>(id);
                return Ok(response);
            } catch (Exception ex)
            {
                return Ok(default);
            }
        }
        [HttpGet("filter")]
        public virtual async Task<IActionResult?> FilterAsync(List<FilterCondition> filters)
        {
            try
            {
                var response = await _service.FilterAsync(filters);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(default);
            }
        }
        [HttpPost("paging")]
        public virtual async Task<IActionResult?> GetPagingData(PagingParam pagingParam)
        {
            try
            {
                var response = await _service.GetPagingAsync(pagingParam);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(default);
            }
        }
    }
}
