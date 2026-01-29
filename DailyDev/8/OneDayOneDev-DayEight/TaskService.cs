using Microsoft.VisualBasic;
using OneDayOneDev_DayEight;
using OneDayOneDev_DayFive;
using OneDayOneDev_DaySeven;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace OneDayOneDev_DayFive
{
    
    public class TaskService
    {
        List<TaskItem>? Tasks { get; set; }
        FileHandler fileHandler { get; set; }
        
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

        public TaskService(List<TaskItem> InitialList)
        {
            Tasks = InitialList ?? new List<TaskItem>();
            fileHandler = new FileHandler();

            Tasks = InitialList;
        }

        public TaskService()
        { 
            Tasks = new List<TaskItem>();
            fileHandler = new FileHandler();

            Tasks = fileHandler.LoadTaskData();

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

        #endregion

        #region GETTER

        
        public List<TaskItem> GetTaskList()
        {
            return Tasks.ToList();
        }
        public int GetNewId()
        {
            return Tasks.Count == 0 ? 1 : Tasks.Max(t => t.id) + 1;
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
            return Tasks.Where(t => t.Title.Contains(Recherche.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<TaskItem> GetTaskThatEndTodayAndNotOver()
        {
            return GetNonEndedTasks().Where(t => (t.DueDate.HasValue && t.DueDate.Value.Date == DateTime.Today)).ToList();
        }
        public List<TaskItem> GetTaskThatEndTodayAndAreOver()
        {
            return GetEndedTasks().Where(t => (t.DueDate.HasValue && t.DueDate.Value.Date == DateTime.Today)).ToList();
        }
        public List<TaskItem> GetLateTask()
        {
            return GetNonEndedTasks().Where(t => (t.DueDate.HasValue && t.DueDate.Value.Date < DateTime.Today)).ToList();
        }
        public List<TaskItem> GetIncomingTask()
        {
            return GetNonEndedTasks().Where(t => (t.DueDate.HasValue && t.DueDate.Value.Date >= DateTime.Today) || !t.DueDate.HasValue)
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
        public OperationResult CreateNewTask(string? TaskTitle,string? DueDate)
        {

            if (string.IsNullOrWhiteSpace(TaskTitle))
                return new OperationResult(false, "Le titre ne peut pas être vide.");


            TaskItem? task = Tasks.FirstOrDefault(t => t.Title == TaskTitle);
            if (task != null)
            {
                return new OperationResult(false, "Une autre tâche possédant ce nom existe déja");
            }
            else
            {
                if (!string.IsNullOrEmpty(DueDate) && ParseDate(DueDate) == null)
                {
                    return new OperationResult(false, $"Erreur dans le format de la date tapée : {DueDate}");
                }
                else
                {
                    Tasks.Add(new TaskItem(id: GetNewId(), Title: TaskTitle, DateTime.Today, ParseDate(DueDate), IsCompleted: false));
                    return new OperationResult(true, "La création de la nouvelle tâche à réussi");
                }



            }

        }

        public OperationResult EndTask(int identifiant)
        {
            
                TaskItem? task = GetTaskById(identifiant);
                if (task != null)
                {
                    if(task.Iscompleted)
                    {
                        return new OperationResult(false, $"La tâche n° {identifiant} est déja terminée depuis le {task.OverDate}");
                    }
                    else
                    {
                        task.Iscompleted = true;
                        task.OverDate = DateTime.Today;
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
            

            TaskItem? task = GetTaskById(identifiant);
            if (task != null)
            {
                if(task.Iscompleted)
                {
                    return new OperationResult(false, $"La tâche n° {identifiant} ne peux être supprimée car elle est déja terminée");
                }
                Tasks.Remove(task);
                return new OperationResult(true, $"La tâche n° {identifiant} à été supprimée");

            }
            else
            {
                return new OperationResult(false, $"La tâche n° {identifiant} n'existe pas");

            }
        }

        #endregion
       


        


    }
}
