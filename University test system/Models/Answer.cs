namespace University_test_system.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        public string Text { get; set; }
        public bool IsTrue { get; set; }
    }
}
