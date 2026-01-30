
using OneDayOneDev_DayEight;
using OneDayOneDev_DayFive;
using OneDayOneDev_DaySeven;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        TaskService taskService = new TaskService();
        ConsoleUi consoleUi = new ConsoleUi();
        var result = 0;
        var TaskId = -1;



        while (result != (int)MenuInfo.Quit)
        {
            Console.Clear();
            consoleUi.GetMenu();
            result = consoleUi.ReadMenuChoice();
            TaskId = -1;
            OperationResult? operationResult = null ;

            
            switch (result)
            {
                case (int)MenuInfo.AddTask:
                    //Ajouter une tâche
                    
                    consoleUi.ShowMessage("Donner un nom à votre tâche \n");
                    string? TaskTitle = Console.ReadLine();
                    consoleUi.ShowMessage("Donner une date d'échéance (format 26/01/2026) (Peux être vide)\n");
                    string? DueDate = Console.ReadLine();
                    operationResult = taskService.CreateNewTask(TaskTitle, DueDate);
                    break;
                case (int)MenuInfo.showAllTask:
                    //Afficher les tâches

                    consoleUi.ShowTasksListWithSummary(taskService.GetTaskList());
                        
                    break;
                case (int)MenuInfo.SetTaskToCompleted:
                    //Marquer une tâche comme terminée
                    consoleUi.ShowTasksListWithSummary(taskService.GetTaskList());
                    consoleUi.ShowMessage("ID de la tâche : ");
                    TaskId = consoleUi.ReadId();
                    if (TaskId < 0)
                    {
                        operationResult = new OperationResult(false, "ID invalide.");
                        break;
                    }
                    operationResult = taskService.EndTask(TaskId);

                    break;
                case (int)MenuInfo.DeleteTask:
                    //Supprimer une tâche

                    consoleUi.ShowTasksListWithSummary(taskService.GetTaskList());
                    consoleUi.ShowMessage("ID de la tâche : ");
                    TaskId = consoleUi.ReadId();
                    if (TaskId < 0)
                    {
                        operationResult = new OperationResult(false, "ID invalide.");
                        break;
                    }
                    operationResult = taskService.DeleteTask(TaskId);

                    break;
                case (int)MenuInfo.showCompletedTask:
                    //Taches terminées

                    consoleUi.ShowTasksListWithSummary(taskService.GetEndedTasks());

                    break;
                case (int)MenuInfo.ShowNotCompletedTask:
                    //Taches non terminée

                    consoleUi.ShowTasksListWithSummary(taskService.GetNonEndedTasks());


                    break;
                case (int)MenuInfo.SearchTaskByWord:
                    //Contient un mot

                    consoleUi.ShowMessage("Quel tâches recherchez vous?");
                    string? Mot = Console.ReadLine()?.ToString();
                    if (!string.IsNullOrEmpty(Mot))
                    {
                        consoleUi.ShowTasksListWithSummary(taskService.GetTaskByTitle(Mot));
                    }

                    break;
                case (int)MenuInfo.nbOfImcompletedTask:
                    //nb taches fini
                    consoleUi.ShowMessage($"Il y à {taskService.GetNumberOfEndedTask()} tache(s) terminée(s)");
                    break;
                case (int)MenuInfo.nbOfcompletedTask:
                    //nb taches non fini
                    consoleUi.ShowMessage($"Il y à {taskService.GetNumberOfNonEndedTask()} tache(s) non terminée(s)");
                    break;
                case (int)MenuInfo.ShowSortedTaskList:
                    //tâches triées
                    consoleUi.ShowTasksListWithSummary(taskService.GetSortedList());
                    break;
                case (int)MenuInfo.ShowDueDateAndNotOver:
                    //tâches triées
                    consoleUi.ShowTasksListWithSummary(taskService.GetTaskThatEndTodayAndNotOver());
                    break;
                case (int)MenuInfo.ShowDueDateAndOver:
                    //tâches triées
                    consoleUi.ShowTasksListWithSummary(taskService.GetTaskThatEndTodayAndAreOver());
                    break;
                case (int)MenuInfo.ShowIncomingTask:
                    //tâches triées
                    consoleUi.ShowTasksListWithSummary(taskService.GetIncomingTask());
                    break;
                case (int)MenuInfo.ExportAllTaskToCSV:
                    //Exportation CSV
                    operationResult = taskService.ExportToCSV(taskService.GetTaskList(), MenuInfo.ExportAllTaskToCSV);
                    break;
                case (int)MenuInfo.ExportLateTaskToCSV:
                    //Exportation CSV
                    operationResult = taskService.ExportToCSV(taskService.GetLateTask(), MenuInfo.ExportLateTaskToCSV);
                    break;
                case (int)MenuInfo.ExportCompletedTaskToCSV:
                    //Exportation CSV
                    operationResult = taskService.ExportToCSV(taskService.GetEndedTasks(), MenuInfo.ExportCompletedTaskToCSV);
                    break;
                case (int)MenuInfo.ExportNonCompletedTaskToCSV:
                    //Exportation CSV
                    operationResult = taskService.ExportToCSV(taskService.GetNonEndedTasks(), MenuInfo.ExportNonCompletedTaskToCSV);
                    break;
                case (int)MenuInfo.UpdateATask:
                    //Mise à jour
                    consoleUi.ShowTasksList(taskService.GetTaskList());
                    consoleUi.ShowMessage("Quel tâche souhaitez-vous mettre à jour (saisir l'identifiant)");
                    var id = consoleUi.ReadId();
                    var task = taskService.GetTaskById(id);
                    if(task == null)
                    {
                        operationResult = new OperationResult(false, $"Aucune tâche ne correspond à l'identifiant {id}");
                    }
                    bool update = true;

                    if(task.Iscompleted)
                    {
                        consoleUi.ShowMessage("La tâche est déja fini, êtes-vous sur de vouloir la modifier? (O/N)");
                        var input = Console.ReadLine();
                        if(input != "O" || input != "N")
                        {
                            operationResult = new OperationResult(false, $"Valeur incorrect");
                            update = false;
                        }
                        else
                        {
                            if(input == "0")
                            {
                                update = true;
                            }
                            else
                            {
                                operationResult = new OperationResult(false, $"Vous avez annulé la modification");
                                update = false;
                            }
                        }

                            
                    }
                    if(update)
                    {
                        consoleUi.ShowMessage("Quel nom souhaitez-vous donner à votre tâches ?");
                        consoleUi.ShowMessage($"Nom actuelle : {task.Title}");
                        var NewTitle = Console.ReadLine();
                        consoleUi.ShowMessage("Quel date d'échéance souhaitez-vous donner à votre tâches ?");
                        consoleUi.ShowMessage($"Date actuelle : {task.DueDate}");
                        var NewDueDate = Console.ReadLine();
                        
                        operationResult = taskService.UpdateTask(id,NewTitle,NewDueDate,task.Iscompleted);
                    }
                        
                    break;
                case (int)MenuInfo.Quit:
                    //Quitter
                    taskService.SaveData();
                    break;
                default:
                    operationResult = new OperationResult(false, "Choix invalide.");
                    continue;
            }

            if (operationResult != null)
            {
                if (operationResult.succes)
                {
                    consoleUi.ShowMessage("SUCCESS : \n");
                }
                else 
                {
                    consoleUi.ShowMessage("ERROR : \n");
                }

                consoleUi.ShowMessage(operationResult.message);
            }

            if(result != (int)MenuInfo.Quit)
            {
                consoleUi.ShowMessage("Appuyer sur une touche pour revenir au menu principal");
                Console.ReadLine();
            }
            
        }

            

        
        
    }
}