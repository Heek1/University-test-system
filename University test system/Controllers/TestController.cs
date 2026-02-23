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
        var user = await _userManager.GetUserAsync(User);

        IQueryable<Test> query = _context.Tests
            .Include(t => t.Subject)
            .Include(t => t.Questions)
            .Include(t => t.TestFaculties);

        if (user.FacultyId.HasValue)
        {
            query = query.Where(t =>
                t.TestFaculties.Any(tf => tf.FacultyId == user.FacultyId));
        }

        var tests = await query.ToListAsync();
        
        var attempts = await _context.Attempts
            .Where(a => a.UserId == user.Id)
            .ToDictionaryAsync(a => a.TestId, a => a.AttemptsCount);

        ViewBag.Attempts = attempts;
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

        var attempt = await _context.Attempts
            .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TestId == id);
        
        if (attempt != null && attempt.AttemptsCount >= test.MaxAttempts)
        {
            TempData["Warning"] = $"Ви вичерпали всі спроби для цього тесту ({test.MaxAttempts})";
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
            existing.AttemptDate = DateTime.Now;
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
            AttemptDate = DateTime.Now
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
        // Отримуємо спробу за її ID, включаючи інформацію про тест, предмет та користувача
        var attempt = await _context.Attempts
            .Include(ut => ut.Test)
            .ThenInclude(t => t.Subject)
            .Include(ut => ut.Test)
            .ThenInclude(t => t.Questions)
            .Include(ut => ut.User)
            .FirstOrDefaultAsync(ut => ut.Id == id);

        if (attempt == null) return NotFound();

        return View(attempt);
    }
    
    // Сторінка історії проходження тестів користувача
    public async Task<IActionResult> History()
    {
        var userId = _userManager.GetUserId(User);

        // Отримуємо всі спроби користувача, включаючи інформацію про тести, відсортовані за датою
        var history = await _context.Attempts
            .Include(a => a.Test)
            .ThenInclude(t => t.Subject)
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