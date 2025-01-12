using Bg.EduSocial.Constract.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        protected readonly IServiceProvider _serviceProvider;
        public ReportController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        [HttpPost("test")]
        [Authorize]
        public virtual async Task<IActionResult> ReportTest([FromBody] ReportParam param)
        {
            var reportService = _serviceProvider.GetRequiredService<IReportService>();
            var response = await reportService.ReportTest(param);
            return Ok(response);
        }
        [HttpPost("chapter")]
        [Authorize]
        public virtual async Task<IActionResult> ReportChapter([FromBody] ReportParam param)
        {
            var reportService = _serviceProvider.GetRequiredService<IReportService>();
            var response = await reportService.ReportChapter(param);
            return Ok(response);
        }
        [HttpPost("level")]
        [Authorize]
        public virtual async Task<IActionResult> ReportLevel([FromBody] ReportParam param)
        {
            var reportService = _serviceProvider.GetRequiredService<IReportService>();
            var response = await reportService.ReportLevel(param);
            return Ok(response);
        }
    }
}
