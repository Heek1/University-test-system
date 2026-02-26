using Microsoft.AspNetCore.Identity;
using University_test_system.Models;

namespace University_test_system.Data
{
    public class SeedData
    {
        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Створюємо ролі, якщо їх немає
            if (!await roleManager.RoleExistsAsync(Roles.SuperAdmin))
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin));

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));

            if (!await roleManager.RoleExistsAsync(Roles.User))
                await roleManager.CreateAsync(new IdentityRole(Roles.User));

            // Створюємо супер-адміна
            var superAdminEmail = "superadmin@knu.ua";
            var superAdminPassword = "superadmin123"; // Встановіть надійний пароль

            var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

            if (superAdmin == null)
            {
                var newSuperAdmin = new User
                {
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newSuperAdmin, superAdminPassword); // пароль
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newSuperAdmin, Roles.SuperAdmin);
                }
            }
        }
    }
}
