using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Domain;
using Microsoft.AspNetCore.Http;

namespace Bg.EduSocial.Constract.Tests
{
    public interface ITestService : IWriteService<TestEntity, TestDto, TestEditDto>
    {
        /// <summary>
        /// Lấy danh sách câu hỏi từ File 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        /// Created By: NVLong 11/5/2024
        Task<List<QuestionDto>> ReadQuestionFromFile(IFormFile file, string regexStr);

        /// <summary>
        /// Lấy danh sách câu hỏi của đề 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        /// Created By: NVLong 11/5/2024
        Task<List<QuestionDto>> GetQuestionOfTest(Guid testId);
        /// <summary>
        /// Lấy danh sách câu hỏi của đề 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        /// Created By: NVLong 11/5/2024
        Task<TestDto> GetTestDetail(Guid testId);
        /// <summary>
        /// Lấy danh sách câu hỏi của đề 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        /// Created By: NVLong 11/5/2024
        Task<List<QuestionDto>> GetQuestionOfTestEditAsync(Guid testId);
        /// <summary>
        /// Lấy danh sách bài thi cho lịch sử làm bài
        /// </summary>
        /// <param name="file"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        /// Created By: NVLong 11/5/2024
        Task<List<ExamDto>> GetExamUserHistory(Guid testId);
        Task<bool> MarkTest(Guid testId);
        void MapResultsToQuestion(List<QuestionDto> questions, List<ResultQuestionDto> results);
    }
}
