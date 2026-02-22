using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using University_test_system.ViewModels.Account;
using University_test_system.Services;
using University_test_system.Data;
using Microsoft.EntityFrameworkCore;

namespace University_test_system.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;
    private readonly ApplicationDbContext _context;

    public AccountController(IAuthService authService, ApplicationDbContext context)
    {
        _authService = authService;
        _context = context;
    }
    
    public IActionResult AccessDenied()
    {
        return View();
    }

    // Сторінка входу
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true) // якщо вже залогінений
            return RedirectToAction("Index", "Home");
        return View();
    }

    // Обробка даних з форми входу
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _authService.LoginAsync(model);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Невірний логін або пароль");
        return View(model);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register()
    {
        var model = new RegisterViewModel
        {
            Faculties = await _context.Faculties.ToListAsync()
        };
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Faculties = await _context.Faculties.ToListAsync();
            return View(model);
        }

        var result = await _authService.RegisterAsync(model);

        if (result.Succeeded)
        {
            TempData["Success"] = "Користувача створено";
            return RedirectToAction("ViewAllUsers", "Admin");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }

    // Обробка виходу користувача
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}