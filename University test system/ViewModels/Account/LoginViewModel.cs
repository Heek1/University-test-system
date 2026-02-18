using System.ComponentModel.DataAnnotations;


namespace University_test_system.ViewModels.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введіть email")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Введіть пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;
    
    [Display(Name = "Запам'ятати мене")]
    public bool RememberMe { get; set; }
}