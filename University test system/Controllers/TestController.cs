using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using University_test_system.Models;
using University_test_system.Data;

namespace University_test_system.Controllers;

[Authorize]
public class TestController : Controller
{
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
    
    public async Task<IActionResult> Take(int id)
    {
        var test = await _context.Tests.FindAsync(id);
        if (test == null) return NotFound();
        
        var userId = _userManager.GetUserId(User);
        
        var alreadyTaken = await _context.Attempts
            .AnyAsync(ut => ut.UserId == userId && ut.TestId == id);
        
        if (alreadyTaken)
        {
            TempData["Warning"] = "Ви вже проходили цей тест";
            return RedirectToAction(nameof(Index));
        }
        
        return View(test);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Take(int id, int score)
    {
        var test = await _context.Tests.FindAsync(id);
        if (test == null) return NotFound();
    
        var userId = _userManager.GetUserId(User);
    
        var existing = await _context.Attempts
            .FirstOrDefaultAsync(a => a.UserId == userId && a.TestId == id);
    
        if (existing != null)
        {
            existing.Count++;
            existing.Score = score;
            existing.AttemptDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return RedirectToAction("Result", new { id = existing.Id });
        }
    
        var attempt = new Attempt
        {
            UserId = userId,
            TestId = id,
            Count = 1,
            Score = score,
            AttemptDate = DateTime.UtcNow
        };
    
        _context.Attempts.Add(attempt);
        await _context.SaveChangesAsync();
    
        TempData["Success"] = "Тест завершено";
        return RedirectToAction("Result", new { id = attempt.Id });
    }
    
    public async Task<IActionResult> Result(int id)
    {
        var userTest = await _context.Attempts
            .Include(ut => ut.Test)
            .FirstOrDefaultAsync(ut => ut.Id == id);
        
        if (userTest == null) return NotFound();
    
        return View(userTest);
    }
    
    public async Task<IActionResult> History()
    {
        var userId = _userManager.GetUserId(User);
    
        var history = await _context.Attempts
            .Include(a => a.Test)
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.AttemptDate)
            .ToListAsync();
    
        return View(history);
    }
    
}