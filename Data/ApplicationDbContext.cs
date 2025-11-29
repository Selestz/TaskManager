using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed данные для демонстрации
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = 1,
                    Title = "Изучить ASP.NET Core",
                    Description = "Познакомиться с основами ASP.NET Core MVC",
                    DueDate = DateTime.Now.AddDays(7),
                    Priority = TaskPriority.High,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Настроить базу данных",
                    Description = "Добавить Entity Framework и создать миграции",
                    DueDate = DateTime.Now.AddDays(3),
                    Priority = TaskPriority.Medium,
                    IsCompleted = true,
                    CreatedAt = DateTime.Now.AddDays(-1)
                },
                new TaskItem
                {
                    Id = 3,
                    Title = "Создать интерфейс",
                    Description = "Разработать удобный пользовательский интерфейс",
                    DueDate = DateTime.Now.AddDays(14),
                    Priority = TaskPriority.Low,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now.AddDays(-2)
                }
            );
        }
    }
}
