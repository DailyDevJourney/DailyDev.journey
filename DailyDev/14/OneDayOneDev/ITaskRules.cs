using OneDayOneDev.DataWindow;

namespace OneDayOneDev
{
    public interface ITaskRules
    {
        bool CanBeCompleted(TaskItem task);
        bool CanBeDeleted(TaskItem task, DateTime ReferenceDate);
        bool IsTaskLate(TaskItem task, DateTime ReferenceDate);
    }
}