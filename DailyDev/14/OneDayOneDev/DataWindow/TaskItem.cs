using OneDayOneDev.DataWindow;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneDayOneDev.DataWindow
{
    [Table("Tasks")]
    public class TaskItem
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column("Title")]
        [StringLength(128)]

        public string Title { get; set; }
        [Column("Completed")]
        public bool Iscompleted { get; set; }
        [Column("CreationDate")]
        public DateTime? CreatedAt { get; set; }
        [Column("UpdateDate")]
        public DateTime? UpdateAt { get; set; }
        [Column("DueDate")]
        public DateTime? DueDate { get; set; }
        [Column("OverDate")]

        public DateTime? OverDate { get; set; }
        [Column("PriorityCode")]
        public TaskPriority Priority { get; set; }

        public TaskItem()
        {

        }
        public TaskItem(int id,string Title, DateTime? CreatedAt, DateTime? dueDate ,bool IsCompleted = false, DateTime? OverDate = null, TaskPriority priority = TaskPriority.MEDIUM)
        { 
            this.id = id;
            this.Title = Title; 
            Iscompleted = IsCompleted;
            this.CreatedAt = CreatedAt;
            DueDate = dueDate;
            this.OverDate = OverDate;
            Priority = priority;
        }
        public TaskItem(string Title, DateTime? CreatedAt, DateTime? dueDate ,bool IsCompleted = false, DateTime? OverDate = null, TaskPriority priority = TaskPriority.MEDIUM)
        { 
        
            this.Title = Title; 
            Iscompleted = IsCompleted;
            this.CreatedAt = CreatedAt;
            DueDate = dueDate;
            this.OverDate = OverDate;
            Priority = priority;
        }
    }
}

public static class TaskItemExtension
{
    public static TaskItem Clone(this TaskItem task)
    {
        var clone = new TaskItem();

        clone.id = task.id;
        clone.Title = task.Title;
        clone.CreatedAt = task.CreatedAt;
        clone.DueDate = task.DueDate;
        clone.OverDate = task.OverDate;
        clone.UpdateAt = task.UpdateAt;
        clone.Priority = task.Priority;
        clone.Iscompleted = task.Iscompleted;


        return clone;

    }
}
