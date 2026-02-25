
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnedayOneDev_Shared.Request
{
    public class TaskCreationRequest
    {
        [Required]
        public string Title { get; set; }

        public string? DueDate { get; set; } = null;
      
        public TaskPriority Priority { get; set; } = TaskPriority.MEDIUM;

    }
}
