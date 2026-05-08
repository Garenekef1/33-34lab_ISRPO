using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models;

public class Category
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Название категории обязательно")]
    [MaxLength(100, ErrorMessage = "Название категории не должно быть длиннее 100 символов")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Описание не должно быть длиннее 500 символов")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(7)]
    public string Color { get; set; } = "#3498db";

    public List<Note> Notes { get; set; } = new();
}
