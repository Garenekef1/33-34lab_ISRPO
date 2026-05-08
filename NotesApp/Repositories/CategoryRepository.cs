using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;
using NotesApp.Models.DTOs;

namespace NotesApp.Repositories;

public class CategoryRepository(AppDbContext db) : ICategoryRepository
{
    private readonly AppDbContext _db = db;

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        return await _db.Categories
            .Select(category => new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Color = category.Color,
                CreatedAt = category.CreatedAt,
                NotesCount = category.Notes.Count(note => !note.IsArchived)
            })
            .OrderBy(category => category.Name)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _db.Categories.FindAsync(id);
    }

    public async Task<Category?> GetByIdWithNotesAsync(int id)
    {
        return await _db.Categories
            .Include(category => category.Notes.Where(note => !note.IsArchived))
            .FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<CategoryResponseDto> CreateAsync(Category category)
    {
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
        return ToResponse(category, 0);
    }

    public async Task<CategoryResponseDto> UpdateAsync(Category category)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();
        var notesCount = await _db.Notes.CountAsync(note => note.CategoryId == category.Id && !note.IsArchived);
        return ToResponse(category, notesCount);
    }

    public async Task DeleteAsync(Category category)
    {
        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Categories.AnyAsync(category => category.Id == id);
    }

    public async Task<bool> HasNotesAsync(int id)
    {
        return await _db.Notes.AnyAsync(note => note.CategoryId == id);
    }

    private static CategoryResponseDto ToResponse(Category category, int notesCount)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Color = category.Color,
            CreatedAt = category.CreatedAt,
            NotesCount = notesCount
        };
    }
}
