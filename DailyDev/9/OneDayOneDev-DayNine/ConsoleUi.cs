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
                ListOfTasks += $"{item.id} - {item.Title} {(item.Iscompleted != true ? "[ ]" : "[X]")} - Créer le : {item.CreatedAt?.ToString("dd/MM/yyyy")} - échéance au : {(item.DueDate == null ? "pas d'échéance" : item.DueDate?.ToString("dd/MM/yyyy"))}\n";
            }

            ShowMessage($"{(ListOfTasks == null ? "Aucune taches" : ListOfTasks)} \n");

           
        }
        public void ShowTasksListWithSummary(List<TaskItem> List)
        {
            Console.Clear();
            string? ListOfTasks = null;
            foreach (var item in List)
            {
                ListOfTasks += $"{item.id} - {item.Title} {(item.Iscompleted != true ? "[ ]" : "[X]")} - Créer le : {item.CreatedAt?.ToString("dd/MM/yyyy")} - échéance au : {(item.DueDate == null ? "pas d'échéance" : item.DueDate?.ToString("dd/MM/yyyy"))}\n";
            }

            ShowMessage($"{(ListOfTasks == null ? "Aucune taches" : ListOfTasks)} \n{GetSummary(List)}");

           
        }

        public string GetSummary(List<TaskItem>? Tasks)
        {

            var Total = Tasks == null ? 0 : Tasks.Count();
            var NonEnded = Tasks == null ? 0 : Tasks.Where(t => !t.Iscompleted).Count();
            var Ended = Tasks == null ? 0 : Tasks.Where(t => t.Iscompleted).Count();
            var Late = Tasks == null ? 0 : Tasks.Where(t => !t.Iscompleted && (t.DueDate.HasValue && t.DueDate.Value.Date < DateTime.Today)).Count();

            return new string($"Total tâches : {Total} \n" +
                $"Terminées : {Ended} \n" +
                $"Restantes : {NonEnded}\n" +
                $"En retard : {Late}");
        }

        public void GetMenu()
        {
            string Menu = "Bonjour, merci de faire un choix : \n";

            foreach(var menuInfoValues in Enum.GetValues<MenuInfo>() )
            {
                Menu += menuInfoValues.GetString() + "\n";
            }

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
                //To match the enum, i have to take -1
                result--;
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
