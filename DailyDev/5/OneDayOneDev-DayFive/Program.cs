
using OneDayOneDev_DayFive;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        TaskService taskService = new TaskService();
        ConsoleUi consoleUi = new ConsoleUi();
        var result = 0;

        while (result != (int)MenuInfo.Quit)
        {
            
            while (result != (int)MenuInfo.Quit)
            {
                Console.Clear();
                consoleUi.GetMenu();
                result = consoleUi.ReadMenuChoice();

                switch (result)
                {
                    case (int)MenuInfo.Add:
                        //Ajouter une tâche
                        taskService.CreateNewTask();
                        break;
                    case (int)MenuInfo.showAll:
                        //Afficher les tâches

                        consoleUi.ShowTasksList(taskService.GetTask());

                        Console.ReadLine();
                        break;
                    case (int)MenuInfo.Ended:
                        //Marquer une tâche comme terminée
                        consoleUi.ShowTasksList(taskService.GetTask());
                        taskService.EndTask();

                        break;
                    case (int)MenuInfo.Delete:
                        //Supprimer une tâche

                        consoleUi.ShowTasksList(taskService.GetTask());
                        taskService.DeleteTask();

                        break;
                    case (int)MenuInfo.showEnded:
                        //Taches terminées

                        consoleUi.ShowTasksList(taskService.GetEndedTasks());
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();

                        break;
                    case (int)MenuInfo.ShowNonEnded:
                        //Taches non terminée

                        consoleUi.ShowTasksList(taskService.GetNonEndedTasks());
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();


                        break;
                    case (int)MenuInfo.SearchByWord:
                        //Contient un mot

                        consoleUi.ShowMessage("Quel tâches recherchez vous?");
                        string? Mot = Console.ReadLine()?.ToString();
                        if (!string.IsNullOrEmpty(Mot))
                        {
                            consoleUi.ShowTasksList(taskService.GetTaskByTitle(Mot));
                        }
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();

                        break;
                    case (int)MenuInfo.nbEnded:
                        //nb taches fini
                        consoleUi.ShowMessage($"Il y à {taskService.GetNumberOfEndedTask()} tache(s) terminée(s)");
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();
                        break;
                    case (int)MenuInfo.nbNonEnded:
                        //nb taches non fini
                        consoleUi.ShowMessage($"Il y à {taskService.GetNumberOfNonEndedTask()} tache(s) non terminée(s)");
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();
                        break;
                    case (int)MenuInfo.ShowSortedList:
                        //tâches triées
                        consoleUi.ShowTasksList(taskService.GetSortedList());
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();
                        break;
                    case (int)MenuInfo.ShowDueDateAndNotOver:
                        //tâches triées
                        consoleUi.ShowTasksList(taskService.GetTaskThatEndTodayAndNotOver());
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();
                        break;
                    case (int)MenuInfo.ShowDueDateAndOver:
                        //tâches triées
                        consoleUi.ShowTasksList(taskService.GetTaskThatEndTodayAndAreOver());
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();
                        break;
                    case (int)MenuInfo.ShowIncomingTask:
                        //tâches triées
                        consoleUi.ShowTasksList(taskService.GetIncomingTask());
                        consoleUi.ShowMessage("Appuyer sur un touche pour revenir au menu principal");
                        Console.ReadLine();
                        break;
                    case (int)MenuInfo.Quit:
                        //Quitter
                        taskService.SaveTaskFile();
                        break;
                    default:
                        consoleUi.ShowMessage("Valeur non valide");
                        System.Threading.Thread.Sleep(3000);
                        break;
                }

            }
        }
        
        
    }
}