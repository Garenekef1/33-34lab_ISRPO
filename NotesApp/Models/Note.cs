using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models;

public class Note
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsPinned { get; set; }
    public bool IsArchived { get; set; }

    [Required(ErrorMessage = "Заголовок обязателен")]
    [MaxLength(200, ErrorMessage = "Заголовок не должен быть длиннее 200 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Содержимое обязательно")]
    [MaxLength(5000, ErrorMessage = "Содержимое не должно быть длиннее 5000 символов")]
    public string Content { get; set; } = string.Empty;

    [Range(1, 5, ErrorMessage = "Приоритет должен быть от 1 до 5")]
    public int Priority { get; set; } = 3;
}
