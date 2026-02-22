using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using University_test_system.Models;
using University_test_system.Data;
using University_test_system.ViewModels.Tests;

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
            .ToListAsync();
        return View(tests);
    }

    //Сторінка створення нового тесту
    public async Task<IActionResult> CreateTest()
    {
        var model = new AddTestViewModel
        {
            Subjects = await _context.Subjects.ToListAsync()
        };
        return View(model);
    }

    //Обробка даних з форми створення тесту
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTest(AddTestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Subjects = await _context.Subjects.ToListAsync();
            return View(model);
        }
        //Створюємо новий тест на основі даних з форми
        var test = new Test
        {
            Title = model.Title,
            SubjectId = model.SubjectId,
            Time = model.Time
        };
        //Додаємо тест до бази даних
        _context.Tests.Add(test);
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
        var users = await _context.Users.ToListAsync();
        return View("ViewAllUsers", users);
    }

    //Видалення користувача за Id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        //Забороняємо адміну видаляти самого себе
        var currentUserId = _userManager.GetUserId(User);
        if (id == currentUserId)
        {
            TempData["Error"] = "Ви не можете видалити власний акаунт";
            return RedirectToAction(nameof(ViewAllUsers));
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        //Видаляємо користувача з бази даних
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Користувача видалено";
        return RedirectToAction(nameof(ViewAllUsers));
    }

    //Сторінка керування питаннями тесту
    public async Task<IActionResult> ManageQuestions(int id)
    {
        var test = await _context.Tests
            .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (test == null) return NotFound();
        return View(test);
    }

    //Додавання нового питання до тесту
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddQuestion(int testId, string title)
    {
        var question = new Question
        {
            TestId = testId,
            Title = title,
            Answers = new List<Answer>()
        };

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Питання додано";
        return RedirectToAction(nameof(ManageQuestions), new { id = testId });
    }

    //Додавання відповіді до питання
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
        return RedirectToAction(nameof(ManageQuestions), new { id = testId });
    }

    //Видалення питання
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteQuestion(int id, int testId)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null) return NotFound();

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Питання видалено";
        return RedirectToAction(nameof(ManageQuestions), new { id = testId });
    }
}