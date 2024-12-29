using Bg.EduSocial.Constract.Report;
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
        [HttpGet("test")]
        public virtual async Task<IActionResult> ReportTest()
        {
            var reportService = _serviceProvider.GetRequiredService<IReportService>();
            var response = await reportService.ReportTest();
            return Ok(response);
        }
    }
}
