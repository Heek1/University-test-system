using Microsoft.AspNetCore.Identity;
using University_test_system.Models;
using University_test_system.ViewModels.Account;

namespace University_test_system.Services;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterViewModel model);
}