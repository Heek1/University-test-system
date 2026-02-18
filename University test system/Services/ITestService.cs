using University_test_system.Models;
using University_test_system.ViewModels.Tests;

namespace University_test_system.Services;

public interface ITestService
{
    Task<List<Subject>> GetAllSubjectsAsync();
    Task<Test> CreateTestAsync(AddTestViewModel model);
    Task<Test?> GetTestByIdAsync(int id);
}