using OneDayOneDev_DayThirteen;
using System.Globalization;

namespace OneDayOneDev
{
    
    public class TaskService
    {
        List<TaskItem> Tasks { get; set; }
        FileHandler fileHandler { get; set; }

        TaskRules taskRules { get; set; }

        private readonly IDateTimeProvider _DateTime;

       public OperationResult ExportToCSV(List<TaskItem> TaskToExport,MenuInfo TypeOfExport)
       {
            var sorted = TaskToExport.OrderBy(t => !t.Iscompleted).ThenBy(t => t.DueDate).ThenBy(t => t.Title).ToList();

            var result = fileHandler.SaveTaskExport(sorted, TypeOfExport);

            return result;
            
       }
        public OperationResult SaveData()
        {
            fileHandler.SaveTaskData(Tasks);
            return new OperationResult(true, $"Fichier sauvegardé ! ");
        }

        public TaskService(List<TaskItem> InitialList, IDateTimeProvider DateTimeProvider)
        {
            _DateTime = DateTimeProvider;
            Tasks = InitialList ?? new List<TaskItem>();
            fileHandler = new FileHandler(DateTimeProvider);
            taskRules = new TaskRules();
        }

        public TaskService(IDateTimeProvider DateTimeProvider)
        { 
            _DateTime = DateTimeProvider;
            Tasks = new List<TaskItem>();
            fileHandler = new FileHandler(DateTimeProvider);
            Tasks = fileHandler.LoadTaskData() ?? new List<TaskItem>();
            taskRules = new TaskRules();
        }

        #region UTILS

        public  static DateTime? ParseDate(string? date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return null;

            if (DateTime.TryParseExact(date, "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result))
            {
                return result.Date;
            }
            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var iso))
                return iso.Date;

            return null;
        }

        private bool HasTask()
        {
            return Tasks.Count > 0 ? true : false;
        }

        #endregion

        #region GETTER

        private TaskItem? TryGetTask(int id)
        {
            return GetTaskById(id);
        }
        public List<TaskItem> GetTaskList()
        {
            return Tasks.ToList();
        }
        public int GetNewId()
        {
            return !HasTask() ? 1 : Tasks.Max(t => t.id) + 1;
        }
        public TaskItem? GetTaskById(int id)
        {
            return Tasks.FirstOrDefault(t => t.id == id);
        }

        public List<TaskItem> GetEndedTasks()
        {
            return Tasks.Where(t => t.Iscompleted).ToList();
        }

        public List<TaskItem> GetNonEndedTasks()
        {
            return Tasks.Where(t => !t.Iscompleted).ToList();
        }
        public List<TaskItem> GetTaskByTitle(string Recherche)
        {
            if (string.IsNullOrWhiteSpace(Recherche)) return new List<TaskItem>();
            return Tasks.Where(t => t.Title.Contains(Recherche.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<TaskItem> GetTaskThatEndTodayAndNotOver()
        {
            return GetNonEndedTasks().Where(t => (t.DueDate.HasValue && t.DueDate.Value.Date == _DateTime.Today)).ToList();
        }
        public List<TaskItem> GetTaskThatEndTodayAndAreOver()
        {
            return GetEndedTasks().Where(t => (t.DueDate.HasValue && t.DueDate.Value.Date == _DateTime.Today)).ToList();
        }
        public List<TaskItem> GetLateTask()
        {
            return GetNonEndedTasks().Where(t => (t.DueDate.HasValue && t.DueDate.Value.Date < _DateTime.Today)).ToList();
        }
        public List<TaskItem> GetIncomingTask()
        {
            return GetNonEndedTasks().Where(t => (t.DueDate.HasValue && t.DueDate.Value.Date >= _DateTime.Today) || !t.DueDate.HasValue)
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
        public List<TaskItem> GetSortedList()
        {
            return Tasks.OrderBy(t => t.Iscompleted).ThenBy(t => t.Title).ThenBy(t => t.id).ToList();
        }

        #endregion

        #region MAIN_FUNCTION
        public OperationResult CreateNewTask(string? TaskTitle,string? DueDate,TaskPriority priority = TaskPriority.MEDIUM)
        {
            
            if (string.IsNullOrWhiteSpace(TaskTitle))
                return new OperationResult(false, "Le titre ne peut pas être vide.");

            var normalized = TaskTitle.Trim();
            var AlreadyExists = Tasks.Any(t => string.Equals(t.Title.Trim(), normalized, StringComparison.OrdinalIgnoreCase));

            if (AlreadyExists)
            {
                return new OperationResult(false, "Une autre tâche possédant ce nom existe déja");
            }
            else
            {

                if (!string.IsNullOrEmpty(DueDate) && ParseDate(DueDate) == null)
                {
                    return new OperationResult(false, $"Erreur dans le format de la date tapée ou date invalide : {DueDate}");
                }
                else
                {
                    
                    Tasks.Add(new TaskItem(id: GetNewId(), Title: normalized, _DateTime.Today, ParseDate(DueDate), IsCompleted: false,priority : priority));
                    return new OperationResult(true, "La création de la nouvelle tâche à réussi");
                }

            }

        }

        public OperationResult UpdateTask(int identifiant, string NewTitle, string NewDueDate, bool NewIscompleted,TaskPriority priority)
        {
            if (!HasTask())
            {
                return new OperationResult(false, "Aucune tâches n'as été trouvée");
            }
            var task = TryGetTask(identifiant);

            if (task == null)
            {
                return new OperationResult(false, $"Aucune tâche ne correspond à l'identifiant {identifiant}");
            }

            if (!string.IsNullOrWhiteSpace(NewTitle))
            {
                var normalized = NewTitle.Trim();
                var exists = Tasks.Any(t => t.id != identifiant &&
                    string.Equals(t.Title.Trim(), normalized, StringComparison.OrdinalIgnoreCase));

                if (exists)
                    return new OperationResult(false, "Une autre tâche possédant ce nom existe déjà");

                task.Title = normalized;
            }

            if (!string.IsNullOrWhiteSpace(NewDueDate))
            {
                var parsed = ParseDate(NewDueDate);
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

            return new OperationResult(true, $"La tâche correspond à l'identifiant {identifiant} à été mise à jour");

        }

        public OperationResult SetTaskCompleted(int identifiant)
        {
            if (!HasTask())
            {
                return new OperationResult(false, "Aucune tâches n'as été trouvée");
            }
            TaskItem? task = TryGetTask(identifiant);
                if (task != null)
                {
                    if(taskRules.IsTaskLate(task,_DateTime.Today))
                    {
                        return new OperationResult(false, "La tâche est en retard");
                    }

                    if(!taskRules.CanBeCompleted(task))
                    {
                        return new OperationResult(false, $"La tâche n° {identifiant} est déja terminée depuis le {task.OverDate}");
                    }
                    else
                    {
                        task.Iscompleted = true;
                        task.OverDate = _DateTime.Today;
                        return new OperationResult(true, $"La tâche n° {identifiant} est terminée");
                    }
                    
                   

                }
                else
                {
                    return new OperationResult(false, $"Erreur lors de la récupération de l'identifiant : {identifiant}");

                }
                

            
        }
        public OperationResult DeleteTask(int identifiant)
        {
            if(!HasTask())
            {
                return new OperationResult(false, "Aucune tâches n'as été trouvée");
            }

            TaskItem? task = TryGetTask(identifiant);
            if (task != null)
            {
                if(!taskRules.CanBeDeleted(task,_DateTime.Today))
                {
                    return new OperationResult(false, $"La tâche n° {identifiant} ne peut être supprimée");
                }
                else
                {
                    Tasks.Remove(task);
                    return new OperationResult(true, $"La tâche n° {identifiant} à été supprimée");
                }

                    

            }
            else
            {
                return new OperationResult(false, $"La tâche n° {identifiant} n'existe pas");

            }
        }



        #endregion
       


        


    }
}
