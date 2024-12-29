using Bg.EduSocial.Constract.Exams;
using Bg.EduSocial.Constract.Report;
using Bg.EduSocial.Constract.Tests;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Cores;
using Microsoft.Extensions.DependencyInjection;

namespace Bg.EduSocial.Application
{
    public class ReportService: IReportService
    {
        protected readonly IServiceProvider _serviceProvider;
        public ReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Dictionary<decimal, int>> ReportTest()
        {
            var testService = _serviceProvider.GetRequiredService<ITestService>();
            var examService = _serviceProvider.GetRequiredService<IExamService>();
            var filters = new List<FilterCondition>();
            var tests = await testService.FilterAsync(filters);
            var testIds = tests.Select(t => t.test_id).ToList();
            var filterExam = new List<FilterCondition>
            {
                new FilterCondition
                {
                    Field = "test_id",
                    Operator = FilterOperator.In,
                    Value = testIds
                }
            };   
            var exams = await examService.FilterAsync<ExamDto>(filterExam);
            if (exams == null || exams.Count == 0)
                return new Dictionary<decimal, int>();

            // Tạo ra dictionary để lưu điểm số và số lượng bài thi tương ứng
            return exams
                .GroupBy(exam => exam.point)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}
