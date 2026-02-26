using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University_test_system.Data;
using University_test_system.Models;
using University_test_system.Services;
using University_test_system.ViewModels.Account;

namespace University_test_system.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public AccountController(IAuthService authService, ApplicationDbContext context, UserManager<User> userManager)
    {
        _authService = authService;
        _userManager = userManager;
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

    [Authorize(Roles = Roles.SuperAdmin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAdmin(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Faculties = await _context.Faculties.ToListAsync();
            return View(model);
        }

        var user = new User
        {
            Email = model.Email,
            DisplayName = model.DisplayName,
            EmailConfirmed = true,            
            FacultyId = model.FacultyId
        };

        var result = await _authService.RegisterAsync(model);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Roles.Admin);
            TempData["Success"] = "Адміна створено";
            return RedirectToAction("ViewAllUsers", "Admin");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }

    //[Authorize(Roles = Roles.SuperAdmin)]
    //[HttpPost]
    //public async Task<IActionResult> CreateAdmin(string email, string password)
    //{
    //    var user = new User { UserName = email, Email = email, EmailConfirmed = true };
    //    var result = await _userManager.CreateAsync(user, password);

    //    if (result.Succeeded)
    //    {
    //        await _userManager.AddToRoleAsync(user, Roles.Admin);
    //        TempData["Success"] = "Адміна створено";
    //        return RedirectToAction("ViewAllUsers", "Admin");
    //    }

    //    foreach (var error in result.Errors)
    //        ModelState.AddModelError(string.Empty, error.Description);

    //    return View();
    //}

    // Обробка виходу користувача
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}