using Microsoft.VisualBasic;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Repository;
using OneDayOneDev.Service.Interface;
using OneDayOneDev.Utils;
using OneDayOneDev_DayThirteen;
using System.Globalization;

namespace OneDayOneDev.Service
{

    public class TaskService : Interface.ITaskService
    {

        private readonly TaskRepository _taskRepository;
        Log LogHandler { get; set; }
        FileHandler fileHandler { get; set; }
        public TaskRules taskRules { get; set; }

        private readonly IDateTimeProvider _DateTime;

        public TaskService(TaskRepository repo, IDateTimeProvider DateTimeProvider)
        {

            _taskRepository = repo;
            taskRules = new TaskRules();
            _DateTime = DateTimeProvider;
            fileHandler = new FileHandler(_DateTime);
            LogHandler = new Log(fileHandler);

        }




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

        public OperationResult CreateNewTask(TaskItem TaskToadd)
        {

            return _taskRepository.AddTask(TaskToadd);
        }
        public OperationResult CreateNewTask(string? TaskTitle, string? DueDate, TaskPriority priority = TaskPriority.MEDIUM)
        {

            if (string.IsNullOrWhiteSpace(TaskTitle))
                return new OperationResult(false, "Le titre ne peut pas être vide.");


            var normalized = TaskTitle.Trim();
            var AlreadyExists = _taskRepository.GetTaskByTitle(normalized);

            if (AlreadyExists != null)
            {
                return new OperationResult(false, "Une autre tâche possédant ce nom existe déja");
            }
            else
            {

                if (!string.IsNullOrEmpty(DueDate) && IDateTimeProvider.ParseDate(DueDate) == null)
                {
                    return new OperationResult(false, $"Erreur dans le format de la date tapée ou date invalide : {DueDate}");
                }
                else
                {

                    _taskRepository.AddTask(new TaskItem(Title: normalized,
                                                            _DateTime.Today,
                                                            IDateTimeProvider.ParseDate(DueDate),
                                                            IsCompleted: false,
                                                            priority: priority));
                    LogHandler.AddLog($"Création d'une nouvelle tâche le {_DateTime.Today.ToString("dd/MM/yyyy")} \n" +
                                        $"Title : {normalized} \nEchéance : {IDateTimeProvider.ParseDate(DueDate)}\n" +
                                        $"Priorité : {priority.GetString()}");
                    return new OperationResult(true, "La création de la nouvelle tâche à réussi");
                }

            }

        }




        public OperationResult UpdateTask(int identifiant, string NewTitle, string NewDueDate, bool NewIscompleted, TaskPriority priority)
        {
            if (!HasTask())
            {
                return new OperationResult(false, "Aucune tâches n'as été trouvée");
            }
            var task = _taskRepository.GetTaskById(identifiant);

            if (task == null)
            {
                return new OperationResult(false, $"Aucune tâche ne correspond à l'identifiant {identifiant}");
            }

            if (!string.IsNullOrWhiteSpace(NewTitle))
            {
                var normalized = NewTitle.Trim();
                var exists = _taskRepository.GetTaskByTitle(normalized);

                if (exists != null)
                    return new OperationResult(false, "Une autre tâche possédant ce nom existe déjà");

                task.Title = normalized;
            }

            if (!string.IsNullOrWhiteSpace(NewDueDate))
            {
                var parsed = IDateTimeProvider.ParseDate(NewDueDate);
                if (parsed == null)
                    return new OperationResult(false, $"Format de date invalide : {NewDueDate}");

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

        public OperationResult SetTaskCompleted(int identifiant)
        {
            return _taskRepository.SetTaskCompleted(identifiant);
        }
        public OperationResult SetTaskImcompleted(int identifiant)
        {
            return _taskRepository.SetTaskImcompleted(identifiant);
        }
        public OperationResult DeleteTask(int identifiant)
        {
            return _taskRepository.DeleteTask(identifiant);
        }



        #endregion






    }
}
