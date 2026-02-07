namespace OneDayOneDev
{
    public enum TaskPriority
    {
        LOW = 1, MEDIUM = 2, HIGH = 3
    }

    public static class TaskPriorityExtension
    {
        public static int GetNumber(this TaskPriority priority)
        {
            return (int)priority;
        }

        public static string GetString(this TaskPriority priority)
        {
            switch (priority)
            {
                case TaskPriority.LOW: return $"{priority.GetNumber()} - {Enum.GetName(typeof(TaskPriority), priority)} ";
                case TaskPriority.MEDIUM: return $"{priority.GetNumber()} - {Enum.GetName(typeof(TaskPriority), priority)}  ";
                case TaskPriority.HIGH: return $"{priority.GetNumber()} - {Enum.GetName(typeof(TaskPriority), priority)}  ";
                

            }

            return "";
        }
    }
}
