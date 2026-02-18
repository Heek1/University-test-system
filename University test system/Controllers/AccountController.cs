using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using University_test_system.Models;
using University_test_system.ViewModels.Account;

namespace University_test_system.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;  // керує користувачами
    private readonly SignInManager<User> _signInManager;  // керує входом/виходом
    
    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
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

        // Створюємо нового користувача на основі даних з форми
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            RegisteredAt = DateTime.UtcNow
        };
        
        var result = await _userManager.CreateAsync(user, model.Password);  //збереження в БД

        // Якщо створення користувача успішне, призначаємо йому роль "User" та виконуємо вхід
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User"); // призначає роль
            await _signInManager.SignInAsync(user, isPersistent: false); // автоматичний вхід після реєстрації
            return RedirectToAction("Index", "Home");
        }

        // Якщо виникли помилки при створенні користувача, додаємо їх до ModelState для відображення на сторінці
        foreach (var error in result.Errors) // показує помилки біля поля
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        
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

        // Виконуємо спробу входу користувача з вказаними даними
        var result = await _signInManager.PasswordSignInAsync(
            model.Email, 
            model.Password, 
            model.RememberMe, 
            lockoutOnFailure: false);
        
        if (result.Succeeded)
            return RedirectToAction("Index", "Home");
    
        ModelState.AddModelError(string.Empty, "Невірний email або пароль"); // помилка не біля визначеного поля, а загальна
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