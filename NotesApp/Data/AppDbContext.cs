using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Note>()
            .HasOne(note => note.Category)
            .WithMany(category => category.Notes)
            .HasForeignKey(note => note.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Note>().HasIndex(note => note.CategoryId);
        modelBuilder.Entity<Note>().HasIndex(note => note.CreatedAt);

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Работа",
                Description = "Рабочие заметки и задачи",
                Color = "#e74c3c",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 2,
                Name = "Учёба",
                Description = "Материалы по обучению",
                Color = "#3498db",
                CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 3,
                Name = "Личное",
                Description = "Личные заметки",
                Color = "#2ecc71",
                CreatedAt = new DateTime(2026, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Note>().HasData(
            new Note
            {
                Id = 1,
                CategoryId = 1,
                Title = "План проекта",
                Content = "Составить список задач по проекту",
                Priority = 4,
                IsPinned = true,
                IsArchived = false,
                CreatedAt = new DateTime(2026, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 4, 0, 0, 0, DateTimeKind.Utc)
            },
            new Note
            {
                Id = 2,
                CategoryId = 2,
                Title = "Конспект по LINQ",
                Content = "Повторить Where, Select, Include и ToListAsync",
                Priority = 5,
                IsPinned = false,
                IsArchived = false,
                CreatedAt = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc)
            },
            new Note
            {
                Id = 3,
                CategoryId = 2,
                Title = "EF Core",
                Content = "Разобраться с миграциями и связями",
                Priority = 4,
                IsPinned = true,
                IsArchived = false,
                CreatedAt = new DateTime(2026, 1, 6, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 6, 0, 0, 0, DateTimeKind.Utc)
            },
            new Note
            {
                Id = 4,
                CategoryId = 3,
                Title = "Покупки",
                Content = "Купить продукты на неделю",
                Priority = 2,
                IsPinned = false,
                IsArchived = false,
                CreatedAt = new DateTime(2026, 1, 7, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 7, 0, 0, 0, DateTimeKind.Utc)
            },
            new Note
            {
                Id = 5,
                CategoryId = 1,
                Title = "Архивная заметка",
                Content = "Старая рабочая заметка",
                Priority = 1,
                IsPinned = false,
                IsArchived = true,
                CreatedAt = new DateTime(2026, 1, 8, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 8, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
