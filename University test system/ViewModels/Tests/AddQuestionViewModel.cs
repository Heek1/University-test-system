using System.ComponentModel.DataAnnotations;
namespace University_test_system.ViewModels.Tests;

public class AddQuestionViewModel
{
    public int TestId { get; set; }

    [Required(ErrorMessage = "Введіть текст питання")]
    [StringLength(500)]
    public string Title { get; set; } = string.Empty;

    public List<AnswerViewModel> Answers { get; set; } = new()
    {
        new AnswerViewModel(),
        new AnswerViewModel(),
        new AnswerViewModel(),
        new AnswerViewModel()
    };
    
    public int? CorrectAnswerIndex { get; set; }
}