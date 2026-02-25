
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.ResultData;

namespace OnedayOneDev_Shared.Service.Interface
{
    public interface ITaskService
    {

         Result<TaskItem> CreateNewTask(string? TaskTitle, string? DueDate, TaskPriority priority = TaskPriority.MEDIUM);
         Result<TaskItem> CreateNewTask(TaskItem TaskToadd);
        Result<TaskItem> DeleteTask(int identifiant);
        IEnumerable<TaskItem> GetEndedTasks();
        List<TaskItem> GetIncomingTask();
        List<TaskItem> GetLateTask();
        IEnumerable<TaskItem> GetNonEndedTasks();
        int GetNumberOfEndedTask();
        int GetNumberOfNonEndedTask();
        IEnumerable<TaskItem> GetSortedList();
        TaskItem? GetTaskById(int id);
        TaskItem? GetTaskByTitle(string Recherche);
        IEnumerable<TaskItem>? GetTaskList(Filter _filter = null);
        List<TaskItem> GetTaskThatEndTodayAndAreOver();
        List<TaskItem> GetTaskThatEndTodayAndNotOver();
        Result<TaskItem> SetTaskCompleted(int identifiant);
        Result<TaskItem> SetTaskImcompleted(int identifiant);
        Result<TaskItem> UpdateTask(int identifiant, string NewTitle, string NewDueDate, bool NewIscompleted, TaskPriority priority);
    }
}