using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Submissions
{
    public interface ISubmissionService : IWriteService<Submission, SubmissionDto, SubmissionEditDto, SubmissionEditDto>
    {
        Task<SubmissionDto> MarkSubmission(Guid id);
    }
}
