using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    //Головна сторінка адміна - список тестів
    public async Task<IActionResult> Index()
    {
        var tests = await _context.Tests
            .Include(t => t.Subject)
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
}