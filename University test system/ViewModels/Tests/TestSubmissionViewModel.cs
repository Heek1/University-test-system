namespace University_test_system.ViewModels.Tests
{
    public class TestSubmissionViewModel
    {
        public int TestId { get; set; }
        public Dictionary<int, int> QuestionAnswers { get; set; } = new Dictionary<int, int>();  // Ключ - Id питання, Значення - Id відповіді
    }
}
