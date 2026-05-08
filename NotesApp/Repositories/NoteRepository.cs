using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;
using NotesApp.Models.DTOs;

namespace NotesApp.Repositories;

public class NoteRepository(AppDbContext db) : INoteRepository
{
    private readonly AppDbContext _db = db;

    public async Task<IEnumerable<NoteResponseDto>> GetAllAsync(NoteFilterDto filter)
    {
        var query = _db.Notes
            .Include(note => note.Category)
            .AsQueryable();

        query = query.Where(note => note.IsArchived == filter.Archived);

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(note => note.CategoryId == filter.CategoryId.Value);
        }

        if (filter.IsPinned.HasValue)
        {
            query = query.Where(note => note.IsPinned == filter.IsPinned.Value);
        }

        if (filter.MinPriority.HasValue)
        {
            query = query.Where(note => note.Priority >= filter.MinPriority.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.ToLower();
            query = query.Where(note =>
                note.Title.ToLower().Contains(search) ||
                note.Content.ToLower().Contains(search));
        }

        query = filter.SortBy.ToLower() switch
        {
            "title" => filter.Descending ? query.OrderByDescending(note => note.Title) : query.OrderBy(note => note.Title),
            "priority" => filter.Descending ? query.OrderByDescending(note => note.Priority) : query.OrderBy(note => note.Priority),
            "updatedat" => filter.Descending ? query.OrderByDescending(note => note.UpdatedAt) : query.OrderBy(note => note.UpdatedAt),
            _ => filter.Descending ? query.OrderByDescending(note => note.CreatedAt) : query.OrderBy(note => note.CreatedAt)
        };

        query = query
            .OrderByDescending(note => note.IsPinned)
            .ThenBy(note => note.IsArchived);

        var page = Math.Max(1, filter.Page);
        var pageSize = Math.Clamp(filter.PageSize, 1, 50);

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(note => ToResponse(note))
            .ToListAsync();
    }

    public async Task<NoteResponseDto?> GetByIdAsync(int id)
    {
        return await _db.Notes
            .Include(note => note.Category)
            .Where(note => note.Id == id)
            .Select(note => ToResponse(note))
            .FirstOrDefaultAsync();
    }

    public async Task<Note> CreateAsync(Note note)
    {
        _db.Notes.Add(note);
        await _db.SaveChangesAsync();
        return note;
    }

    public async Task UpdateAsync(Note note)
    {
        note.UpdatedAt = DateTime.UtcNow;
        _db.Notes.Update(note);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Note note)
    {
        _db.Notes.Remove(note);
        await _db.SaveChangesAsync();
    }

    public async Task<Note?> FindAsync(int id)
    {
        return await _db.Notes.FindAsync(id);
    }

    public async Task<int> GetCountByCategoryAsync(int categoryId)
    {
        return await _db.Notes.CountAsync(note => note.CategoryId == categoryId);
    }

    private static NoteResponseDto ToResponse(Note note)
    {
        return new NoteResponseDto
        {
            Id = note.Id,
            CategoryId = note.CategoryId,
            CategoryName = note.Category.Name,
            CategoryColor = note.Category.Color,
            Title = note.Title,
            Content = note.Content,
            Priority = note.Priority,
            IsPinned = note.IsPinned,
            IsArchived = note.IsArchived,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }
}
