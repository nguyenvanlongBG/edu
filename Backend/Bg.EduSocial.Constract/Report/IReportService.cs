namespace Bg.EduSocial.Constract.Report
{
    public interface IReportService
    {
        Task<List<Dictionary<string, object>>> ReportTest(ReportParam param);
        Task<List<Dictionary<string, object>>> ReportChapter(ReportParam param);
        Task<List<Dictionary<string, object>>> ReportLevel(ReportParam param);
    }
}
