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
            FacultyId = model.FacultyId,
            RegisteredAt = DateTime.UtcNow,
            DisplayName = model.DisplayName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return result;

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }

        await _userManager.AddToRoleAsync(user, "User");

        return IdentityResult.Success;
    }
    
    public async Task<SignInResult> LoginAsync(LoginViewModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));

        // 1. шукаємо користувача по email
        var user = await _userManager.FindByEmailAsync(model.Email);

        // 2. якщо не знайшли — це username
        if (user == null)
            user = await _userManager.FindByNameAsync(model.Email);

        // 3. якщо взагалі не існує
        if (user == null)
            return SignInResult.Failed;

        // 4. вхід
        return await _signInManager.PasswordSignInAsync(
            user.UserName,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);
    }
    
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}