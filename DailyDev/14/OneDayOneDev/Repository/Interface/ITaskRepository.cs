using OneDayOneDev.DataWindow;
using OneDayOneDev.Resultdata;

namespace OneDayOneDev.Repository.Interface
{
    public interface ITaskRepository
    {
        Result<TaskItem> AddTask(TaskItem task);
        Result<TaskItem> DeleteTask(int id);
        IEnumerable<TaskItem>? GetAllTask();
        IEnumerable<TaskItem>? GetDoneTasks();
        IEnumerable<TaskItem>? GetOrderTasks();
        TaskItem? GetTaskById(int id);
        TaskItem? GetTaskByTitle(string Title);
        IEnumerable<TaskItem>? GetUnDoneTasks();
        int? HasTasks();
        Result<TaskItem> SetTaskCompleted(int id);
        Result<TaskItem> SetTaskImcompleted(int id);
        Result<TaskItem> UpdateTask(int id, TaskItem Newtask);
    }
}