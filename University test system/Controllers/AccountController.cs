using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using University_test_system.Models;
using University_test_system.ViewModels.Account;
using University_test_system.Services;

namespace University_test_system.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;
    private readonly SignInManager<User> _signInManager;  // керує входом/виходом

    public AccountController(
        IAuthService authService,
        SignInManager<User> signInManager)
    {
        _authService = authService;
        _signInManager = signInManager;
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

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError(string.Empty, "Невірний email або пароль");
        return View(model);
    }

    // Обробка виходу користувача
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}