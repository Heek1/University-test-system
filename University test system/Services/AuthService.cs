// University test system\Services\AuthService.cs
using Microsoft.AspNetCore.Identity;
using University_test_system.Models;
using University_test_system.ViewModels.Account;

namespace University_test_system.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));

        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            RegisteredAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return result;

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }

        await _userManager.AddToRoleAsync(user, "User");

        await _signInManager.SignInAsync(user, isPersistent: false);

        return IdentityResult.Success;
    }
}