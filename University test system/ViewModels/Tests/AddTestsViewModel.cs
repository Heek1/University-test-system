using System.ComponentModel.DataAnnotations;
using University_test_system.Models;

namespace University_test_system.ViewModels.Tests;

public class AddTestViewModel
{
    [Required(ErrorMessage = "Введіть назву тесту")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Оберіть предмет")]
    public int SubjectId { get; set; }
    
    [Required(ErrorMessage = "Вкажіть час")]
    [Range(1, 300, ErrorMessage = "Час від 1 до 300 хвилин")]
    public int Time { get; set; }
    
    public List<Subject> Subjects { get; set; } = new(); // для випадаючого списку предметів (якщо Віктор зможе зробити було б класно, щоб це було не просто список, а SelectListItem з вибраним значенням)
}