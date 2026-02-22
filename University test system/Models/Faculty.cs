namespace University_test_system.Models;

public class Faculty
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User>? Users { get; set; }
}