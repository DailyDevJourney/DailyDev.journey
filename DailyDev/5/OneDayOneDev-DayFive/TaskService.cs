using Microsoft.VisualBasic;
using OneDayOneDev_DayFive;
using System.Globalization;
using System.Runtime.Serialization;

namespace OneDayOneDev_DayFive
{
    
    public class TaskService
    {
        List<TaskItem> Tasks { get; set; }
        string ListFilePath = $"{AppContext.BaseDirectory}ListeTache.txt";
        ConsoleUi UI = new ConsoleUi();
        public TaskService()
        { 
            Tasks = new List<TaskItem>();
            if (File.Exists(ListFilePath))
            {
                LoadTaskFile();
            }
            else
            {
                var myFile = File.Create(ListFilePath);
                myFile.Close();
            }
        }

        public List<TaskItem> GetTask()
        {
            return Tasks;
        }

        private static DateTime? ParseDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                if(date.Length == 10)
                {
                    date += " 00:00:00";
                }
                if (DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                    return result;
            }

            return null;
        }
        public void LoadTaskFile()
        {
            using (StreamReader sr = new StreamReader(ListFilePath))
            {
                string? line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] valeur = line.Split('|');
                    if (valeur.Length < 5) //Ancien fichier
                    {
                        Tasks.Add(new TaskItem(id: int.Parse(valeur[0]), Title: valeur[1], CreatedAt: DateTime.Now, dueDate: null, IsCompleted: valeur[2] == "0" ? false : true));

                    }
                    else
                    {
                        var CreatedDate = ParseDate(valeur[2]);
                        var DueDate = ParseDate(valeur[3]);
                        Tasks.Add(new TaskItem(id: int.Parse(valeur[0]), Title: valeur[1], CreatedAt: CreatedDate, dueDate: DueDate, IsCompleted: valeur[4] == "0" ? false : true));
                    }
                    
                }

            }
        }

        public void SaveTaskFile()
        {
            File.WriteAllLines(ListFilePath,
                Tasks.Select(t => $"{t.id}|{t.Title}|{(t.CreatedAt == null ? "" : t.CreatedAt?.ToString("o"))}|{(t.DueDate == null ? "" : t.DueDate?.ToString("o"))}|{(t.Iscompleted == false ? '0' : '1')}"));
            
        }

        public void CreateNewTask()
        {
            string? TaskTitle = null;
            while (TaskTitle == null || string.IsNullOrEmpty(TaskTitle))
            {
                Console.Clear();
                string add = "Donner un nom à votre tâche \n";

                UI.ShowMessage(add);

                TaskTitle = Console.ReadLine();
            }

            TaskItem? task = Tasks.FirstOrDefault(t => t.Title == TaskTitle);
            if (task != null)
            {
                UI.ShowMessage("Une autre tâche possédant ce nom existe déja");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                string AddDate = "Donner une date d'échéance (format 26/01/2026) (Peux être vide)";

                UI.ShowMessage(AddDate);

                string? DueDate = Console.ReadLine();

                if(!string.IsNullOrEmpty(DueDate) && ParseDate(DueDate) == null)
                {
                    UI.ShowMessage($"Erreur dans le format de la date tapée : {DueDate}");
                    System.Threading.Thread.Sleep(3000);
                }
                else
                {
                    Tasks.Add(new TaskItem(id: GetNewId(), Title: TaskTitle, DateTime.Now, ParseDate(DueDate), IsCompleted: false));
                }


                    
            }
                
        }

        
        private int RetrieveCustomerInputForId()
        {
            UI.ShowMessage("ID de la tâche : ");
            return UI.ReadId();
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
            return Tasks.Where(t => t.Title.Contains(Recherche, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<TaskItem> GetTaskThatEndTodayAndNotOver()
        {
            return GetNonEndedTasks().Where(t => t.DueDate == DateTime.Today).ToList();
        }
        public List<TaskItem> GetTaskThatEndTodayAndAreOver()
        {
            return GetEndedTasks().Where(t => t.DueDate == DateTime.Today).ToList();
        }
        public List<TaskItem> GetLateTask()
        {
            return GetNonEndedTasks().Where(t => t.DueDate <= DateTime.Today).ToList();
        }
        public List<TaskItem> GetIncomingTask()
        {
            return GetNonEndedTasks().Where(t => t.DueDate >= DateTime.Today || t.DueDate == null)
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

        public void EndTask()
        {
            
                int identifiant = RetrieveCustomerInputForId();

                TaskItem? task = GetTaskById(identifiant);
                if (task != null)
                {
                    task.Iscompleted = true;
                    UI.ShowMessage($"Tache numéro {identifiant} terminée");
                        
                }
                else
                {
                    UI.ShowMessage($"Aucune tache n'as pas identifiant {identifiant}");
                        
                }
                
                System.Threading.Thread.Sleep(3000);

            
        }
        public void DeleteTask()
        {
            int identifiant = RetrieveCustomerInputForId();

            TaskItem? task = GetTaskById(identifiant);
            if (task != null)
            {
                Tasks.Remove(task);
                UI.ShowMessage($"Tache numéro {identifiant} supprimée");

            }
            else
            {
                UI.ShowMessage($"Aucune tache n'as pas identifiant {identifiant}");

            }
        }
        public List<TaskItem> GetSortedList()
        {
            return Tasks.OrderBy(t => t.Iscompleted).ThenBy(t => t.Title).ThenBy(t => t.id).ToList();
        }


        


    }
}
