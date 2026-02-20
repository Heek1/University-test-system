using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using University_test_system.Models;
using University_test_system.ViewModels.Account;
using University_test_system.Services;

namespace University_test_system.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    // Сторінка реєстрації
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true) // якщо вже залогінений
            return RedirectToAction("Index", "Home");

        return View();
    }

    // Обробка даних з форми реєстрації
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _authService.RegisterAsync(model);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
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

    // Обробка виходу користувача
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}