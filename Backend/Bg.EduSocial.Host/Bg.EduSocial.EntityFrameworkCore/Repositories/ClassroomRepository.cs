using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Classes;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class ClassroomRepository : WriteRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
