using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneDayOneDev.Api.Request
{
    public class TaskCreationRequest
    {
        [Required]
        public string Title { get; set; }

        public DateTime? DueDate { get; set; } = null;
      
        public TaskPriority Priority { get; set; } = TaskPriority.MEDIUM;

    }
}
