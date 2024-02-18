using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Submissions;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class SubmissionAnswerRepository : WriteRepository<SubmissionAnswer>, ISubmissionAnswerRepository
    {
        public SubmissionAnswerRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
