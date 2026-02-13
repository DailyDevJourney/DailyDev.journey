using OneDayOneDev.DataWindow;

namespace OneDayOneDev.Service.Interface
{
    public interface ITaskService
    {
        TaskRules taskRules { get; set; }

        OperationResult CreateNewTask(string? TaskTitle, string? DueDate, TaskPriority priority = TaskPriority.MEDIUM);
        OperationResult CreateNewTask(TaskItem TaskToadd);
        OperationResult DeleteTask(int identifiant);
        IEnumerable<TaskItem> GetEndedTasks();
        List<TaskItem> GetIncomingTask();
        List<TaskItem> GetLateTask();
        IEnumerable<TaskItem> GetNonEndedTasks();
        int GetNumberOfEndedTask();
        int GetNumberOfNonEndedTask();
        IEnumerable<TaskItem> GetSortedList();
        TaskItem? GetTaskById(int id);
        TaskItem? GetTaskByTitle(string Recherche);
        IEnumerable<TaskItem> GetTaskList();
        List<TaskItem> GetTaskThatEndTodayAndAreOver();
        List<TaskItem> GetTaskThatEndTodayAndNotOver();
        OperationResult SetTaskCompleted(int identifiant);
        OperationResult SetTaskImcompleted(int identifiant);
        OperationResult UpdateTask(int identifiant, string NewTitle, string NewDueDate, bool NewIscompleted, TaskPriority priority);
    }
}