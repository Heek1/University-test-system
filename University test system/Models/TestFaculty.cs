namespace University_test_system.Models;

public class TestFaculty
{
    public int TestId { get; set; }
    public Test Test { get; set; }
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
}