using Microsoft.AspNetCore.Mvc;
using NotesApp.Helpers;
using NotesApp.Models;
using NotesApp.Models.DTOs;
using NotesApp.Repositories;

namespace NotesApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryRepository repo) : ControllerBase
{
    private readonly ICategoryRepository _repo = repo;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CategoryResponseDto>>>> GetAll()
    {
        var categories = await _repo.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<CategoryResponseDto>>.Ok(categories));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> GetById(int id)
    {
        var category = await _repo.GetByIdWithNotesAsync(id);

        if (category is null)
        {
            return NotFound(ApiError.NotFound($"Категория с id={id} не найдена"));
        }

        var response = new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Color = category.Color,
            CreatedAt = category.CreatedAt,
            NotesCount = category.Notes.Count
        };

        return Ok(ApiResponse<CategoryResponseDto>.Ok(response));
    }

    [HttpGet("{id:int}/notes")]
    public async Task<ActionResult<ApiResponse<object>>> GetWithNotes(int id)
    {
        var category = await _repo.GetByIdWithNotesAsync(id);

        if (category is null)
        {
            return NotFound(ApiError.NotFound($"Категория с id={id} не найдена"));
        }

        var response = new
        {
            category.Id,
            category.Name,
            category.Description,
            category.Color,
            notes = category.Notes.Select(note => new
            {
                note.Id,
                note.Title,
                note.Priority,
                note.IsPinned,
                note.CreatedAt
            })
        };

        return Ok(ApiResponse<object>.Ok(response));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> Create([FromBody] CreateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiError.BadRequest("Ошибка валидации", GetModelErrors()));
        }

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            Color = dto.Color,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repo.CreateAsync(category);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, ApiResponse<CategoryResponseDto>.Created(created));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> Update(int id, [FromBody] UpdateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiError.BadRequest("Ошибка валидации", GetModelErrors()));
        }

        var category = await _repo.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound(ApiError.NotFound($"Категория с id={id} не найдена"));
        }

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.Color = dto.Color;

        var updated = await _repo.UpdateAsync(category);

        return Ok(ApiResponse<CategoryResponseDto>.Ok(updated, "Категория обновлена"));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _repo.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound(ApiError.NotFound($"Категория с id={id} не найдена"));
        }

        if (await _repo.HasNotesAsync(id))
        {
            return BadRequest(ApiError.BadRequest("Нельзя удалить категорию, в которой есть заметки"));
        }

        await _repo.DeleteAsync(category);

        return NoContent();
    }

    private List<string> GetModelErrors()
    {
        return ModelState.Values
            .SelectMany(value => value.Errors)
            .Select(error => error.ErrorMessage)
            .ToList();
    }
}
