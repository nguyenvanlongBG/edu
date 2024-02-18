using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class QuestionTestRepository : WriteRepository<QuestionTest>, IQuestionTestRepository
    {
        public QuestionTestRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
