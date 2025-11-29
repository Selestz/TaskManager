using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "IsCompleted", "Priority", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 29, 14, 45, 51, 8, DateTimeKind.Local).AddTicks(9850), "Познакомиться с основами ASP.NET Core MVC", new DateTime(2025, 12, 6, 14, 45, 51, 8, DateTimeKind.Local).AddTicks(9850), false, 2, "Изучить ASP.NET Core" },
                    { 2, new DateTime(2025, 11, 28, 14, 45, 51, 8, DateTimeKind.Local).AddTicks(9860), "Добавить Entity Framework и создать миграции", new DateTime(2025, 12, 2, 14, 45, 51, 8, DateTimeKind.Local).AddTicks(9850), true, 1, "Настроить базу данных" },
                    { 3, new DateTime(2025, 11, 27, 14, 45, 51, 8, DateTimeKind.Local).AddTicks(9860), "Разработать удобный пользовательский интерфейс", new DateTime(2025, 12, 13, 14, 45, 51, 8, DateTimeKind.Local).AddTicks(9860), false, 0, "Создать интерфейс" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
