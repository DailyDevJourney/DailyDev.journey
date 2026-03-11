
using OnedayOneDev_Shared.Identification;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnedayOneDev_Shared.Request
{
    public class CreationRequest
    {
        [Required]
        public string Title { get; set; }

        public string? DueDate { get; set; } = null;
      
        public TaskPriority Priority { get; set; } = TaskPriority.MEDIUM;

    }

    public class UserCreationRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string? Password { get; set; } = null;

        public UserRole Role { get; set; } = UserRole.USER;

    }
}
