namespace University_test_system.Models
{
    public class Attempt
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int Count { get; set; }      // кількість спроб
        public int Score { get; set; }  // набрані бали
        public DateTime AttemptDate { get; set; } = DateTime.Now;
    }
}
