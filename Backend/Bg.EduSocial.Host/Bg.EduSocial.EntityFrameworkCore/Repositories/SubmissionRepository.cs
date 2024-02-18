using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Submissions;
using Bg.EduSocial.Domain.Tests;
using Bg.EduSocial.EFCore.Repositories;
using Bg.EduSocial.EntityFrameworkCore.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EntityFrameworkCore.Repositories
{
    public class SubmissionRepository : WriteRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(IUnitOfWork<EduSocialDbContext> unitOfWork) : base(unitOfWork)
        {
        }
        public override async Task<Submission> GetById(Guid id)
        {
            var submission = await Context.Submissions.Where(s => s.ID == id).Include(submission => submission.SubmissionAnswers).FirstOrDefaultAsync();
            if (submission != null)
            {
                return submission;
            }
            return default;
        }
    }
}
