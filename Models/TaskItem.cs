using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public enum TaskPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Urgent = 3
    }

    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название задачи обязательно")]
        [StringLength(200, ErrorMessage = "Название не может превышать 200 символов")]
        [Display(Name = "Название")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Описание не может превышать 1000 символов")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Срок выполнения")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Выполнено")]
        public bool IsCompleted { get; set; }

        [Display(Name = "Приоритет")]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        [Display(Name = "Дата создания")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
