namespace University_test_system.Models;

public class UserTest
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int TestId { get; set; }
    public DateTime TakenAt { get; set; } = DateTime.UtcNow;
    public int? Score { get; set; }
    
    public User User { get; set; } = null!;
    public Test Test { get; set; } = null!;
}