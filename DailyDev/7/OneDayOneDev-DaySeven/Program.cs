
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
                case (int)MenuInfo.Add:
                    //Ajouter une tâche
                    
                    consoleUi.ShowMessage("Donner un nom à votre tâche \n");
                    string? TaskTitle = Console.ReadLine();
                    consoleUi.ShowMessage("Donner une date d'échéance (format 26/01/2026) (Peux être vide)\n");
                    string? DueDate = Console.ReadLine();
                    operationResult = taskService.CreateNewTask(TaskTitle, DueDate);
                    break;
                case (int)MenuInfo.showAll:
                    //Afficher les tâches

                    consoleUi.ShowTasksList(taskService.GetTaskList());
                        
                    break;
                case (int)MenuInfo.Ended:
                    //Marquer une tâche comme terminée
                    consoleUi.ShowTasksList(taskService.GetTaskList());
                    consoleUi.ShowMessage("ID de la tâche : ");
                    TaskId = consoleUi.ReadId();
                    if (TaskId < 0)
                    {
                        operationResult = new OperationResult(false, "ID invalide.");
                        break;
                    }
                    operationResult = taskService.EndTask(TaskId);

                    break;
                case (int)MenuInfo.Delete:
                    //Supprimer une tâche

                    consoleUi.ShowTasksList(taskService.GetTaskList());
                    consoleUi.ShowMessage("ID de la tâche : ");
                    TaskId = consoleUi.ReadId();
                    if (TaskId < 0)
                    {
                        operationResult = new OperationResult(false, "ID invalide.");
                        break;
                    }
                    operationResult = taskService.DeleteTask(TaskId);

                    break;
                case (int)MenuInfo.showEnded:
                    //Taches terminées

                    consoleUi.ShowTasksList(taskService.GetEndedTasks());

                    break;
                case (int)MenuInfo.ShowNonEnded:
                    //Taches non terminée

                    consoleUi.ShowTasksList(taskService.GetNonEndedTasks());


                    break;
                case (int)MenuInfo.SearchByWord:
                    //Contient un mot

                    consoleUi.ShowMessage("Quel tâches recherchez vous?");
                    string? Mot = Console.ReadLine()?.ToString();
                    if (!string.IsNullOrEmpty(Mot))
                    {
                        consoleUi.ShowTasksList(taskService.GetTaskByTitle(Mot));
                    }

                    break;
                case (int)MenuInfo.nbEnded:
                    //nb taches fini
                    consoleUi.ShowMessage($"Il y à {taskService.GetNumberOfEndedTask()} tache(s) terminée(s)");
                    break;
                case (int)MenuInfo.nbNonEnded:
                    //nb taches non fini
                    consoleUi.ShowMessage($"Il y à {taskService.GetNumberOfNonEndedTask()} tache(s) non terminée(s)");
                    break;
                case (int)MenuInfo.ShowSortedList:
                    //tâches triées
                    consoleUi.ShowTasksList(taskService.GetSortedList());
                    break;
                case (int)MenuInfo.ShowDueDateAndNotOver:
                    //tâches triées
                    consoleUi.ShowTasksList(taskService.GetTaskThatEndTodayAndNotOver());
                    break;
                case (int)MenuInfo.ShowDueDateAndOver:
                    //tâches triées
                    consoleUi.ShowTasksList(taskService.GetTaskThatEndTodayAndAreOver());
                    break;
                case (int)MenuInfo.ShowIncomingTask:
                    //tâches triées
                    consoleUi.ShowTasksList(taskService.GetIncomingTask());
                    break;
                case (int)MenuInfo.Quit:
                    //Quitter
                    taskService.SaveTaskFile();
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