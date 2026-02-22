using System.ComponentModel.DataAnnotations;
namespace University_test_system.ViewModels.Tests;

public class AnswerViewModel
{
    [Required(ErrorMessage = "Введіть текст відповіді")]
    public string Text { get; set; } = string.Empty;

    public bool IsTrue { get; set; } = false;
}