using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.Now;
        
        // Связь с задачами
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
