using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain.Tests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Tests
{
    public interface ITestService : IWriteService<Test, TestDto, TestInsertDto, TestUpdateDto>
    {
        /// <summary>
        /// Lấy danh sách câu hỏi từ File 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        /// Created By: NVLong 11/5/2024
        Task<List<QuestionDto>> ReadQuestionFromFile(IFormFile file, string regexStr);
    }
}
