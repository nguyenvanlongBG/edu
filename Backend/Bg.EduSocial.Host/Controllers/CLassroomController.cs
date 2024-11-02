using Bg.EduSocial.Constract.Classrooms;
using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Submissions;
using Bg.EduSocial.Domain.Tests;
using Microsoft.AspNetCore.Mvc;

namespace Bg.EduSocial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CLassroomController : WriteController<Classroom, ClassroomDto, ClassroomEditDto, ClassroomEditDto>
    {
        private readonly IClassroomService _classroomService;
        public CLassroomController(IClassroomService classroomService) : base(classroomService)
        {
            _classroomService = classroomService;
        }
        [HttpGet("test")]
        public bool Test()
        {
            var str = "";
            var lst = new List<Test>
            {
                 new Test{
                     Name = "A",
                     ID = new Guid(),
           StartTime = new DateTime(),
           FinishTime = new DateTime()
                 },
            };
            var sum = (from account in lst
                       where account.Name.ToString().ToLower() == "a"
                       select account.Name).ToList();
            return true;
        }
    }
}
