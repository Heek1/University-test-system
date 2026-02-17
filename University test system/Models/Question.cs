namespace University_test_system.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
