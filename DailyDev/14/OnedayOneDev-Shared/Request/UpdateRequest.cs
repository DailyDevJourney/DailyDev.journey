using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnedayOneDev_Shared.Request
{
    public class UpdateRequest
    {
        [Required]
        public int identifiant { get; set; }
        public string? NewTitle { get; set; }
        public string? NewDueDate { get; set; }
        public bool NewIscompleted { get; set; } = false;
        public  TaskPriority priority { get; set; }
    }
}
