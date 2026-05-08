using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotesApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsPinned = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsArchived = table.Column<bool>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "#e74c3c", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Рабочие заметки и задачи", "Работа" },
                    { 2, "#3498db", new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Материалы по обучению", "Учёба" },
                    { 3, "#2ecc71", new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Личные заметки", "Личное" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedAt", "IsArchived", "IsPinned", "Priority", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Составить список задач по проекту", new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), false, true, 4, "План проекта", new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, "Повторить Where, Select, Include и ToListAsync", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), false, false, 5, "Конспект по LINQ", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2, "Разобраться с миграциями и связями", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), false, true, 4, "EF Core", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 3, "Купить продукты на неделю", new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), false, false, 2, "Покупки", new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 1, "Старая рабочая заметка", new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, false, 1, "Архивная заметка", new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CategoryId",
                table: "Notes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CreatedAt",
                table: "Notes",
                column: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
