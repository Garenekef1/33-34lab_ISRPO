using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models.DTOs;

public class CreateNoteDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Нужно указать категорию")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Заголовок обязателен")]
    [MaxLength(200, ErrorMessage = "Заголовок не должен быть длиннее 200 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Содержимое обязательно")]
    [MaxLength(5000, ErrorMessage = "Содержимое не должно быть длиннее 5000 символов")]
    public string Content { get; set; } = string.Empty;

    [Range(1, 5, ErrorMessage = "Приоритет должен быть от 1 до 5")]
    public int Priority { get; set; } = 3;
}

public class UpdateNoteDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Нужно указать категорию")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Заголовок обязателен")]
    [MaxLength(200, ErrorMessage = "Заголовок не должен быть длиннее 200 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Содержимое обязательно")]
    [MaxLength(5000, ErrorMessage = "Содержимое не должно быть длиннее 5000 символов")]
    public string Content { get; set; } = string.Empty;

    [Range(1, 5, ErrorMessage = "Приоритет должен быть от 1 до 5")]
    public int Priority { get; set; } = 3;
}

public class NoteResponseDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryColor { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Priority { get; set; }
    public bool IsPinned { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
