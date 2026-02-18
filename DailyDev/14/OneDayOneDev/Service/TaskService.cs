using Microsoft.VisualBasic;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Repository;
using OneDayOneDev.Repository.Interface;
using OneDayOneDev.Resultdata;
using OneDayOneDev.Service.Interface;
using OneDayOneDev.Utils;
using OneDayOneDev.Utils.Interface;
using OneDayOneDev_DayThirteen;
using System.Globalization;

namespace OneDayOneDev.Service
{

    public class TaskService(ITaskRules _tasksrules,
                    ILog _LogHandler, 
                    ITaskRepository repo, 
                    IDateTimeProvider _DateTimeProvider) : ITaskService
    {

        private readonly ITaskRepository _taskRepository = repo;
        ILog _LogHandler { get; set; } = _LogHandler;
        public ITaskRules taskRules { get; set; } = _tasksrules;

        private readonly IDateTimeProvider _DateTime = _DateTimeProvider;





        #region UTILS



        private bool HasTask()
        {
            return _taskRepository.HasTasks() > 0 ? true : false;
        }

        #endregion

        #region GETTER

        public IEnumerable<TaskItem> GetTaskList()
        {
            return _taskRepository.GetAllTask();
        }

        public TaskItem? GetTaskById(int id)
        {
            return _taskRepository.GetTaskById(id);
        }

        public IEnumerable<TaskItem> GetEndedTasks()
        {
            return _taskRepository.GetDoneTasks();
        }

        public IEnumerable<TaskItem> GetNonEndedTasks()
        {
            return _taskRepository.GetUnDoneTasks();
        }

        public TaskItem? GetTaskByTitle(string Recherche)
        {
            if (string.IsNullOrWhiteSpace(Recherche)) return null;
            return _taskRepository.GetTaskByTitle(Recherche);
        }

        public List<TaskItem> GetTaskThatEndTodayAndNotOver()
        {
            return GetNonEndedTasks().Where(t => t.DueDate.HasValue && t.DueDate.Value.Date == _DateTime.Today).ToList();
        }
        public List<TaskItem> GetTaskThatEndTodayAndAreOver()
        {
            return GetEndedTasks().Where(t => t.DueDate.HasValue && t.DueDate.Value.Date == _DateTime.Today).ToList();
        }
        public List<TaskItem> GetLateTask()
        {
            return GetNonEndedTasks().Where(t => t.DueDate.HasValue && t.DueDate.Value.Date < _DateTime.Today).ToList();
        }
        public List<TaskItem> GetIncomingTask()
        {
            return GetNonEndedTasks().Where(t => t.DueDate.HasValue && t.DueDate.Value.Date >= _DateTime.Today || !t.DueDate.HasValue)
                                        .ToList();
        }
        public int GetNumberOfEndedTask()
        {
            var ListOfEndedTask = GetEndedTasks();
            return ListOfEndedTask.Count();
        }
        public int GetNumberOfNonEndedTask()
        {
            var ListOfNonEndedTask = GetNonEndedTasks();
            return ListOfNonEndedTask.Count();
        }
        public IEnumerable<TaskItem> GetSortedList()
        {
            return _taskRepository.GetOrderTasks();
        }

        #endregion

        #region MAIN_FUNCTION

        public Result<TaskItem> CreateNewTask(TaskItem TaskToadd)
        {
            var result = _taskRepository.AddTask(TaskToadd);

            if (result.Success)
            {
                _LogHandler.AddLog($"Création d'une nouvelle tâche le {_DateTime.Today.ToString("dd/MM/yyyy")} \n" +
                                $"Title : {result.Data.Title} \nEchéance : {result.Data.DueDate}\n" +
                                $"Priorité : {result.Data.Priority.GetString()}");
            }
            return result;
            

        }
        public Result<TaskItem> CreateNewTask(string? TaskTitle, string? DueDate, TaskPriority priority = TaskPriority.MEDIUM)
        {

            if (string.IsNullOrWhiteSpace(TaskTitle))
                return Result<TaskItem>.Failed("Le titre ne peut pas être vide.");


            var normalized = TaskTitle.Trim();
            var AlreadyExists = _taskRepository.GetTaskByTitle(normalized);

            if (AlreadyExists != null)
            {
                return  Result<TaskItem>.Failed("Une autre tâche possédant ce nom existe déja");
            }
            else
            {

                if (!string.IsNullOrEmpty(DueDate) && IDateTimeProvider.ParseDate(DueDate) == null)
                {
                    return Result<TaskItem>.Failed($"Erreur dans le format de la date tapée ou date invalide : {DueDate}");
                }
                else
                {

                    var result = _taskRepository.AddTask(new TaskItem(Title: normalized,
                                                            _DateTime.Today,
                                                            IDateTimeProvider.ParseDate(DueDate),
                                                            IsCompleted: false,
                                                            priority: priority));

                    if (result.Success)
                    {
                        _LogHandler.AddLog($"Création d'une nouvelle tâche le {_DateTime.Today.ToString("dd/MM/yyyy")} \n" +
                                        $"Title : {result.Data.Title} \nEchéance : {result.Data.DueDate}\n" +
                                        $"Priorité : {result.Data.Priority.GetString()}");
                    }
                    return result;


                }

            }

        }




        public Result<TaskItem> UpdateTask(int identifiant, string NewTitle, string NewDueDate, bool NewIscompleted, TaskPriority priority)
        {
            if (!HasTask())
            {
                return Result<TaskItem>.Failed("Aucune tâches n'as été trouvée");
            }
            var task = _taskRepository.GetTaskById(identifiant);

            if (task == null)
            {
                return Result<TaskItem>.Failed($"Aucune tâche ne correspond à l'identifiant {identifiant}");
            }

            if (!string.IsNullOrWhiteSpace(NewTitle))
            {
                var normalized = NewTitle.Trim();
                var exists = _taskRepository.GetTaskByTitle(normalized);

                if (exists != null)
                    return Result<TaskItem>.Failed("Une autre tâche possédant ce nom existe déjà");

                task.Title = normalized;
            }

            if (!string.IsNullOrWhiteSpace(NewDueDate))
            {
                var parsed = IDateTimeProvider.ParseDate(NewDueDate);
                if (parsed == null)
                    return Result<TaskItem>.Failed($"Format de date invalide : {NewDueDate}");

                task.DueDate = parsed;
            }

            if (!task.Iscompleted && NewIscompleted)
            {
                task.OverDate = _DateTime.Today;
            }

            task.Iscompleted = NewIscompleted;

            if (!NewIscompleted)
                task.OverDate = null;

            task.Priority = priority;
            task.UpdateAt = _DateTime.Today;

            return _taskRepository.UpdateTask(identifiant, task); ;

        }

        public Result<TaskItem> SetTaskCompleted(int identifiant)
        {
            return _taskRepository.SetTaskCompleted(identifiant);
        }
        public Result<TaskItem> SetTaskImcompleted(int identifiant)
        {
            return _taskRepository.SetTaskImcompleted(identifiant);
        }
        public Result<TaskItem> DeleteTask(int identifiant)
        {
            return _taskRepository.DeleteTask(identifiant);
        }



        #endregion






    }
}
