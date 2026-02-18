using Microsoft.EntityFrameworkCore;
using University_test_system.Data;
using University_test_system.Models;
using University_test_system.ViewModels.Tests;

namespace University_test_system.Services;

public class TestService : ITestService
{
    private readonly ApplicationDbContext _context;

    public TestService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Subject>> GetAllSubjectsAsync()
    {
        return await _context.Subjects.ToListAsync();
    }

    public async Task<Test> CreateTestAsync(AddTestViewModel model)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        var subjectExists = await _context.Subjects.AnyAsync(s => s.Id == model.SubjectId);
        if (!subjectExists)
            throw new InvalidOperationException("Subject not found.");

        var test = new Test
        {
            Title = model.Title,
            SubjectId = model.SubjectId,
            Time = model.Time
        };

        _context.Tests.Add(test);
        await _context.SaveChangesAsync();

        return test;
    }

    public async Task<Test?> GetTestByIdAsync(int id)
    {
        return await _context.Tests.FindAsync(id);
    }
}