using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain.Tests;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : WriteController<Test, TestDto, TestInsertDto, TestUpdateDto>
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService) : base(testService)
        {
            _testService = testService;
        }
    }
}
