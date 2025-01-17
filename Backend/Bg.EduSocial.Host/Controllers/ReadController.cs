﻿using Bg.EduSocial.Constract;
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
            var response = await _service.GetById<TEntityDto>(id);
            return Ok(response);
        }
        [HttpPost("filter")]
        public virtual async Task<IActionResult?> FilterAsync(List<FilterCondition> filters)
        {
            var response = await _service.FilterAsync(filters);
            return Ok(response);
        }
        [HttpPost("paging")]
        public virtual async Task<IActionResult?> GetPagingData(PagingParam pagingParam)
        {
            var response = await _service.GetPagingAsync(pagingParam);
            return Ok(response);
        }
        [HttpPost("summary")]
        public virtual async Task<IActionResult?> GetSummayData(PagingParam pagingParam)
        {
            var response = await _service.GetSummaryData(pagingParam);
            return Ok(response);
        }
    }
}
