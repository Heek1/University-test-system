using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using University_test_system.Models;
using University_test_system.Data;
using University_test_system.ViewModels.Tests;
using Microsoft.AspNetCore.Identity;
namespace University_test_system.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    //Контролер для адміністрування тестів: створення, редагування, видалення
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    public AdminController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    //Головна сторінка адміна - список тестів
    public async Task<IActionResult> Index()
    {
        var tests = await _context.Tests
            .Include(t => t.Subject)
            .Include(t => t.Questions)
            .Include(t => t.TestFaculties)
            .ToListAsync();

        return View(tests);
    }

    //Сторінка створення нового тесту
    public async Task<IActionResult> CreateTest()
    {
        var model = new AddTestViewModel
        {
            Subjects = await _context.Subjects.ToListAsync(),
            Faculties = await _context.Faculties.ToListAsync()
        };
        return View(model);
    }
    //Обробка даних з форми створення тесту
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTest(AddTestViewModel model)
    {
        if (!model.SelectedFacultyIds.Any())
        {
            ModelState.AddModelError("SelectedFacultyIds", "Оберіть хоча б один факультет");
        }
        if (!ModelState.IsValid)
        {
            model.Subjects = await _context.Subjects.ToListAsync();
            model.Faculties = await _context.Faculties.ToListAsync();
            return View(model);
        }
        //Створюємо новий тест на основі даних з форми
        var test = new Test
        {
            Title = model.Title,
            SubjectId = model.SubjectId,
            Time = model.Time,
            Level = model.Level,
            MaxAttempts = model.MaxAttempts
        };
        //Додаємо тест до бази даних
        _context.Tests.Add(test);
        await _context.SaveChangesAsync();
        
        foreach (var facultyId in model.SelectedFacultyIds)
        {
            _context.TestFaculties.Add(new TestFaculty
            {
                TestId = test.Id,
                FacultyId = facultyId
            });
        }

        await _context.SaveChangesAsync();
        
        TempData["Success"] = "Тест створено";
        return RedirectToAction(nameof(Index));
    }

    //Сторінка видалення тесту
    public async Task<IActionResult> DeleteTest(int id)
    {
        var test = await _context.Tests.FindAsync(id);
        if (test == null) return NotFound();
        _context.Tests.Remove(test);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Тест видалено";
        return RedirectToAction(nameof(Index));
    }
    //Редагування тесту через Id тесту
    public async Task<IActionResult> EditTest(int id)
    {
        //Знаходимо тест за Id
        var test = await _context.Tests.FindAsync(id);
        if (test == null) return NotFound();
        //Створюємо модель для передачі даних у вигляд
        var model = new AddTestViewModel
        {
            Title = test.Title,
            SubjectId = test.SubjectId,
            Time = test.Time,
            Subjects = await _context.Subjects.ToListAsync()
        };
        return View(model);
    }
    //Збереження змін після редагування тесту
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTest(int id, AddTestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Subjects = await _context.Subjects.ToListAsync();
            return View(model);
        }
        //Знаходимо тест за Id і оновлюємо дані
        var test = await _context.Tests.FindAsync(id);
        if (test == null) return NotFound();

        test.Title = model.Title;
        test.SubjectId = model.SubjectId;
        test.Time = model.Time;

        await _context.SaveChangesAsync();
        TempData["Success"] = "Тест оновлено";
        return RedirectToAction(nameof(Index));
    }
    //Сторінка зі списком всіх користувачів
    public async Task<IActionResult> ViewAllUsers()
    {
        var users = await _context.Users
            .Include(u => u.Faculty)
            .ToListAsync();
        return View("ViewAllUsers", users);
    }
    //Видалення користувача за Id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        
        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            TempData["Error"] = "Не можна видалити адміністратора";
            return RedirectToAction(nameof(ViewAllUsers));
        }
        
        //Видаляємо користувача з бази даних
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Користувача видалено";
        return RedirectToAction(nameof(ViewAllUsers));
    }
    
    public async Task<IActionResult> ManageQuestions(int id)
    {
        var test = await _context.Tests
            .Include(t => t.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (test == null) return NotFound();

        return View(test);
    }

    public async Task<IActionResult> AddQuestion(int id)
    {
        var test = await _context.Tests.FindAsync(id);
        if (test == null) return NotFound();

        var model = new AddQuestionViewModel
        {
            TestId = id
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddQuestion(AddQuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            TempData["Error"] = string.Join(", ", errors);
            return View(model);
        }

        var question = new Question
        {
            Title = model.Title,
            TestId = model.TestId,
            Answers = model.Answers.Select(a => new Answer
            {
                Text = a.Text,
                IsTrue = a.IsTrue
            }).ToList()
        };

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Питання додано";
        return RedirectToAction("ManageQuestions", new { id = model.TestId });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAnswer(int questionId, int testId, string text, bool isTrue)
    {
        var answer = new Answer
        {
            QuestionId = questionId,
            Text = text,
            IsTrue = isTrue
        };
        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Відповідь додано";
        return RedirectToAction("ManageQuestions", new { id = testId });
    }
}