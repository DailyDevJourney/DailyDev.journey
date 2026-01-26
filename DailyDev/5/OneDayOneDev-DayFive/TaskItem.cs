using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev_DayFive
{
    public class TaskItem
    {
        public int id { get; set; }
        public string Title { get; set; }
        public bool Iscompleted { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskItem(int id , string Title, DateTime? CreatedAt, DateTime? dueDate ,bool IsCompleted = false)
        { 
        
            this.id = id;
            this.Title = Title; 
            this.Iscompleted = IsCompleted;
            this.CreatedAt = CreatedAt;
            this.DueDate = dueDate;
        }
    }
}
