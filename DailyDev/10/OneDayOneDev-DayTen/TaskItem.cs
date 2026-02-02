namespace OneDayOneDev_DayEleven
{
    public class TaskItem
    {
        public int id { get; set; }
        public string Title { get; set; }
        public bool Iscompleted { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }

        public DateTime? OverDate { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskItem(int id , string Title, DateTime? CreatedAt, DateTime? dueDate ,bool IsCompleted = false, DateTime? OverDate = null, TaskPriority priority = TaskPriority.MEDIUM)
        { 
        
            this.id = id;
            this.Title = Title; 
            this.Iscompleted = IsCompleted;
            this.CreatedAt = CreatedAt;
            this.DueDate = dueDate;
            this.OverDate = OverDate;
            this.Priority = priority;
        }
    }
}
