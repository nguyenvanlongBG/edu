using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract.Tests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.FileQuestion
{
    public interface IFileQuestionService
    {
        List<QuestionDto> GetQuestionFromFile(IFormFile file, SplitQuestion splitQuestion);
    }
}
