namespace University_test_system.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public string AvailableFor { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int Time { get; set; }
        public List<Question> Questions { get; set; }
        public List<Attempt> Attempts { get; set; }
    }
}
