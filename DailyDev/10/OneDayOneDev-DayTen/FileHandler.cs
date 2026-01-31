using OneDayOneDev_DayFive;
using OneDayOneDev_DaySeven;
using OneDayOneDev_DayTen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev_DayEight
{
    public class FileHandler
    {
        readonly string TaskDataPath = $"{AppContext.BaseDirectory}ListeTache.txt";
        readonly string TasksExportPath = $"{AppContext.BaseDirectory}TaskExport";

        public FileHandler()
        {
        }
        public List<TaskItem>? LoadTaskData()
        {
            if (!File.Exists(TaskDataPath))
            {
                var myFile = File.Create(TaskDataPath);
                myFile.Close();

                return new List<TaskItem>(); ;
            }

            List<TaskItem> Tasks = new List<TaskItem>();

            using (StreamReader sr = new StreamReader(TaskDataPath))
            {
                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] valeur = line.Split('|');

                    switch (valeur.Length)
                    {
                        case 5:

                            Tasks.Add(new TaskItem(id: int.Parse(valeur[0]),
                                                    Title: valeur[1],
                                                    CreatedAt: TaskService.ParseDate(valeur[2]),
                                                    dueDate: TaskService.ParseDate(valeur[3]),
                                                    IsCompleted: valeur[4] == "0" ? false : true));
                            break;
                        case 6:

                            Tasks.Add(new TaskItem(id: int.Parse(valeur[0]),
                                                    Title: valeur[1],
                                                    CreatedAt: TaskService.ParseDate(valeur[2]),
                                                    dueDate: TaskService.ParseDate(valeur[3]),
                                                    IsCompleted: valeur[4] == "0" ? false : true,
                                                    TaskService.ParseDate(valeur[5])));

                            break;
                        case 7:

                            Tasks.Add(new TaskItem(id: int.Parse(valeur[0]),
                                                    Title: valeur[1],
                                                    CreatedAt: TaskService.ParseDate(valeur[2]),
                                                    dueDate: TaskService.ParseDate(valeur[3]),
                                                    IsCompleted: valeur[4] == "0" ? false : true,
                                                    TaskService.ParseDate(valeur[5]),
                                                    priority : (Enum.TryParse(valeur[6], out TaskPriority taskPriority)) ? taskPriority : TaskPriority.MEDIUM));

                            break;
                        default:
                            if (valeur.Length >= 3)
                            {
                                Tasks.Add(new TaskItem(id: int.Parse(valeur[0]), Title: valeur[1], CreatedAt: DateTime.Today, dueDate: null, IsCompleted: valeur[2] == "0" ? false : true));
                            }

                            break;

                    }


                }

            }

            return Tasks;
        }

        public void SaveTaskData(List<TaskItem>? Tasks)
        {
            
            if (Tasks?.Count > 0)
            {
                File.WriteAllLines(TaskDataPath,
                    contents: Tasks.Select(t => $"{t.id}|{t.Title}|{(t.CreatedAt == null ? "" : t.CreatedAt?.ToString("dd/MM/yyyy"))}|{(t.DueDate == null ? "" : t.DueDate?.ToString("dd/MM/yyyy"))}|{(t.Iscompleted == false ? '0' : '1')}|{(t.OverDate == null ? "" : t.OverDate?.ToString("dd/MM/yyyy"))}|{Enum.GetName(typeof(TaskPriority), t.Priority)}"));
            }
                

        }

        public OperationResult SaveTaskExport(List<TaskItem> Tasks,MenuInfo TypeOfExport)
        {
            string exportDir = Path.Combine(AppContext.BaseDirectory, "TaskExport");
            Directory.CreateDirectory(exportDir);

            string? ExportName = TypeOfExport switch
            {
                MenuInfo.ExportAllTaskToCSV => "AllTasks",
                MenuInfo.ExportLateTaskToCSV => "LateTasks",
                MenuInfo.ExportCompletedTaskToCSV => "CompletedTasks",
                MenuInfo.ExportNonCompletedTaskToCSV => "NonCompletedTasks",
                _ => null

            };
            if (ExportName == null)
            {
                return new OperationResult(false, "Export type incorrect");
            }


            string exportPath = Path.Combine(exportDir, $"{ExportName}.csv");

            if (!File.Exists(exportPath))
            {
                var myFile = File.Create(exportPath);
                myFile.Close();
            }
            else
            {
                 return new OperationResult(false, $"Un fichier {exportPath} éxiste déja"); 

            }


            File.AppendAllText(exportPath, "Id;Title;CreatedAt;DueDate;OverDate;IsCompleted;Priority\n");
            File.AppendAllLines(exportPath,
                Tasks.Select(t => $"{t.id};{t.Title};{(t.CreatedAt == null ? "" : t.CreatedAt?.ToString("dd/MM/yyyy"))};{(t.DueDate == null ? "" : t.DueDate?.ToString("dd/MM/yyyy"))};{(t.OverDate == null ? "" : t.OverDate?.ToString("dd/MM/yyyy"))};{t.Iscompleted};{Enum.GetName(typeof(TaskPriority), t.Priority)}"));

            var Total = Tasks == null ? 0 : Tasks.Count();
            var NonEnded = Tasks == null ? 0 : Tasks.Where(t => !t.Iscompleted).Count();
            var Ended = Tasks == null ? 0 : Tasks.Where(t => t.Iscompleted).Count();
            var Late = Tasks == null ? 0 : Tasks.Where(t => !t.Iscompleted && (t.DueDate.HasValue && t.DueDate.Value.Date < DateTime.Today)).Count();

            
            return new OperationResult(true, $"Export terminé ! \n" +
                $"Total tâches : {Total} \n" +
                $"Terminées : {Ended} \n" +
                $"Restantes : {NonEnded}\n" +
                $"En retard : {Late}");
        }
    }
}
