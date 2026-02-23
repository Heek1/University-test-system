using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University_test_system.Data;
using University_test_system.Models;
using University_test_system.ViewModels.Tests;

namespace University_test_system.Controllers;

[Authorize]
public class TestController : Controller
{
    //Контролер для проходження тестів студентами та перегляду результатів
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    
    public TestController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var tests = await _context.Tests.ToListAsync();
        return View(tests);
    }

    // Сторінка проходження тесту
    public async Task<IActionResult> Take(int id)
    {
        //var test = await _context.Tests.FindAsync(id);

        var test = await _context.Tests
        .Include(t => t.Questions)
            .ThenInclude(q => q.Answers)
        .FirstOrDefaultAsync(t => t.Id == id);

        if (test == null) return NotFound();
        
        var userId = _userManager.GetUserId(User);

        // Перевіряємо, чи користувач вже проходив цей тест
        var alreadyTaken = await _context.Attempts
            .AnyAsync(ut => ut.UserId == userId && ut.TestId == id);
        
        if (alreadyTaken)
        {
            TempData["Warning"] = "Ви вже проходили цей тест";
            return RedirectToAction(nameof(Index));
        }
        
        return View(test);
    }

    // Обробка результатів тесту
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Take(TestSubmissionViewModel submission)
    {
        // Отримуємо тест з питаннями та відповідями
        var test = await _context.Tests
            .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(t => t.Id == submission.TestId);

        if (test == null) return NotFound();

        var userId = _userManager.GetUserId(User);

        // Підрахунок балів
        int score = 0;

        if (submission.QuestionAnswers == null || submission.QuestionAnswers.Count == 0)
        {
            submission.QuestionAnswers = new Dictionary<int, int>();
        }

        foreach (var entry in submission.QuestionAnswers)
        {
            var question = test.Questions.FirstOrDefault(q => q.Id == entry.Key);
            var chosen = question?.Answers.FirstOrDefault(a => a.Id == entry.Value);
            if (chosen?.IsTrue == true) score++;
        }

        // Перевірка чи вже є спроба
        var existing = await _context.Attempts
            .FirstOrDefaultAsync(a => a.UserId == userId && a.TestId == submission.TestId);

        // Якщо вже є спроба, оновлюємо її
        if (existing != null)
        {
            existing.AttemptsCount++;
            existing.Score = score;
            existing.AttemptDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return RedirectToAction("Result", new { id = existing.Id });
        }

        // Створюємо нову спробу
        var attempt = new Attempt
        {
            UserId = userId,
            TestId = submission.TestId,
            AttemptsCount = 1,
            Score = score,
            AttemptDate = DateTime.UtcNow
        };

        // Додаємо спробу до бази даних
        _context.Attempts.Add(attempt);
        await _context.SaveChangesAsync();

        // Виводимо повідомлення про успішне завершення тесту
        TempData["Success"] = "Тест завершено";
        return RedirectToAction("Result", new { id = attempt.Id });
    }

    // Обробка результатів тесту
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Take(int id, int score)
    //{
    //    var test = await _context.Tests.FindAsync(id);
    //    if (test == null) return NotFound();
    
    //    var userId = _userManager.GetUserId(User);

    //    // Перевіряємо, чи користувач вже проходив цей тест
    //    var existing = await _context.Attempts
    //        .FirstOrDefaultAsync(a => a.UserId == userId && a.TestId == id);

    //    // Якщо вже є спроба, оновлюємо її
    //    if (existing != null)
    //    {
    //        existing.AttemptsCount++;
    //        existing.Score = score;
    //        existing.AttemptDate = DateTime.UtcNow;
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction("Result", new { id = existing.Id });
    //    }

    //    // Створюємо нову спробу
    //    var attempt = new Attempt
    //    {
    //        UserId = userId,
    //        TestId = id,
    //        AttemptsCount = 1,
    //        Score = score,
    //        AttemptDate = DateTime.UtcNow
    //    };

    //    // Додаємо спробу до бази даних
    //    _context.Attempts.Add(attempt);
    //    await _context.SaveChangesAsync();
    
    //    TempData["Success"] = "Тест завершено";
    //    return RedirectToAction("Result", new { id = attempt.Id });
    //}

    // Сторінка результатів тесту
    public async Task<IActionResult> Result(int id)
    {
        // Отримуємо спробу за її ID, включаючи інформацію про тест
        var userTest = await _context.Attempts
            .Include(ut => ut.Test)
            .FirstOrDefaultAsync(ut => ut.Id == id);
        
        if (userTest == null) return NotFound();
    
        return View(userTest);
    }

    // Сторінка історії проходження тестів користувача
    public async Task<IActionResult> History()
    {
        var userId = _userManager.GetUserId(User);

        // Отримуємо всі спроби користувача, включаючи інформацію про тести, відсортовані за датою
        var history = await _context.Attempts
            .Include(a => a.Test)
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.AttemptDate)
            .ToListAsync();
    
        return View(history);
    }

    // Сторінка перегляду рейтингу студентів за певним тестом
    public async Task<IActionResult> Leaderboard(int testId)
    {        
        // Отримуємо всі спроби, включаючи інформацію про користувачів та тести
        var leaderboard = await _context.Attempts
            .Include(a => a.User)
            .Include(a => a.Test)
            .Where(a => a.TestId == testId) // Фільтруємо за тестом
            .OrderByDescending(a => a.Score) // Сортуємо за балами
            .ThenBy(a => a.AttemptDate) // Якщо бали однакові, сортуємо за датою
            .ToListAsync();
    
        return View(leaderboard);
    }
}