namespace Bg.EduSocial.Constract.Report
{
    public interface IReportService
    {
        Task<Dictionary<decimal, int>> ReportTest();
    }
}
