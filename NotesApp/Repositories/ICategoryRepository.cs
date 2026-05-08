using NotesApp.Models;
using NotesApp.Models.DTOs;

namespace NotesApp.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> GetByIdWithNotesAsync(int id);
    Task<CategoryResponseDto> CreateAsync(Category category);
    Task<CategoryResponseDto> UpdateAsync(Category category);
    Task DeleteAsync(Category category);
    Task<bool> ExistsAsync(int id);
    Task<bool> HasNotesAsync(int id);
}
