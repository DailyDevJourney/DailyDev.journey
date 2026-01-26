using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev_DayFive
{
    public class ConsoleUi
    {
        public void ShowTasksList(List<TaskItem> List)
        {
            Console.Clear();
            string? ListOfTasks = null;
            foreach (var item in List)
            {
                ListOfTasks += $"{item.id} - {item.Title} {(item.Iscompleted != true ? "[ ]" : "[X]")} - Créer le : {item.CreatedAt} - échéance au : {(item.DueDate == null ? "pas d'échéance" : item.DueDate)}\n";
            }

            ShowMessage($"{(ListOfTasks == null ? "Aucune taches" : ListOfTasks)} \n");

            var NonEnded = List.Where(t => !t.Iscompleted).Count();
            var Ended = List.Where(t => t.Iscompleted).Count();

            ShowMessage($"Total tâches : {List.Count()} \nTerminées : {Ended} \nRestantes : {NonEnded}\n");
        }

        public void GetMenu()
        {
            string Menu = "Bonjour, merci de faire un choix : \n" +
                            "1 - Ajouter une tâche \n" +
                            "2 - Afficher les tâches \n" +
                            "3 - Marquer une tâches comme terminée\n" +
                            "4 - Supprimer une tâche\n" +
                            "5 - Montrer les tâches terminées\n" +
                            "6 - Montrer les tâches non terminées\n" +
                            "7 - Montrer les tâches qui contiennent un mot\n" +
                            "8 - Combien de taches non terminées\n" +
                            "9 - Combien de taches terminées\n" +
                            "10 - Montrer les tâches triées par fini puis titre\n" +
                            "11 - Montrer les tâches à échéances aujourdh'ui non finis\n" +
                            "12 - Montrer les tâches à échéances aujourdh'ui finis\n" +
                            "13 - Montrer les tâches à venir\n" +
                            "14 - Quitter\n";

            ShowMessage(Menu);
        }
        public int ReadMenuChoice()
        {
            var input = Console.ReadLine();

            if (!int.TryParse(input, out var result))
            {
                Console.WriteLine("Entrée invalide");
                Thread.Sleep(1500);
                return -1;
            }
            else
            {
                return result;
            }

                
        }
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
        public string? ReadText(string label)
        {
            string? input = Console.ReadLine();

            return input;
        }
        public int ReadId()
        {
            var input = Console.ReadLine();

            if (!int.TryParse(input, out var result))
            {
                Console.WriteLine("Entrée invalide");
                Thread.Sleep(1500);
                return -1;
            }
            else
            {
                return result;
            }
        }
    }
}
