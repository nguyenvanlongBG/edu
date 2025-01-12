namespace Bg.EduSocial.Domain.Shared.Questions
{
    public enum QuestionType
    {
        SingleChoice = 1,
        MultiChoice = 2,
        FillResult = 3
    }
    public enum QuestionLevel
    {
        Recognition = 1,         // Nhận biết
        Comprehension = 2,       // Thông hiểu
        Application = 3,         // Vận dụng
        AdvancedApplication = 4  // Vận dụng cao
    }
}
