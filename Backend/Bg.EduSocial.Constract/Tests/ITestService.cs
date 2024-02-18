using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Tests
{
    public interface ITestService : IWriteService<Test, TestDto, TestInsertDto, TestUpdateDto>
    {
    
    }
}
