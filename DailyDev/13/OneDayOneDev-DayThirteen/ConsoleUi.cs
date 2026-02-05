namespace OneDayOneDev_DayThirten
{
    public class ConsoleUi(IDateTimeProvider DateTimeProvider)
    {
        private readonly IDateTimeProvider _DateTime = DateTimeProvider;
        public void ShowTasksListWithSummary(List<TaskItem> List)
        {
            Console.Clear();
            string? ListOfTasks = null;
            foreach (var item in List)
            {
                ListOfTasks += $"{item.id} - {item.Title} {(item.Iscompleted != true ? "[ ]" : "[X]")} - Créer le : {item.CreatedAt?.ToString("dd/MM/yyyy")} - échéance au : {(item.DueDate == null ? "pas d'échéance" : item.DueDate?.ToString("dd/MM/yyyy"))} - Priorité : {Enum.GetName(typeof(TaskPriority), item.Priority)}\n";
            }

            ShowMessage($"{(ListOfTasks == null ? "Aucune taches" : ListOfTasks)} \n{GetSummary(List)}");


        }
        public void ShowTasksList(List<TaskItem> List)
        {
            Console.Clear();
            string? ListOfTasks = null;
            foreach (var item in List)
            {
                ListOfTasks += $"{item.id} - {item.Title} {(item.Iscompleted != true ? "[ ]" : "[X]")} - Créer le : {item.CreatedAt?.ToString("dd/MM/yyyy")} - échéance au : {(item.DueDate == null ? "pas d'échéance" : item.DueDate?.ToString("dd/MM/yyyy"))} - Priorité : {Enum.GetName(typeof(TaskPriority), item.Priority)}\n";
            }

            ShowMessage($"{(ListOfTasks == null ? "Aucune taches" : ListOfTasks)} \n");

           
        }

        public char ReadAnswerYesOrNo(string Message)
        {
            ShowMessage(Message);
            var result = Console.ReadKey();
            while (result.Key != ConsoleKey.O && result.Key != ConsoleKey.N)
            {
                result = Console.ReadKey();

                if(result.Key != ConsoleKey.O && result.Key != ConsoleKey.N)
                {
                    ShowMessage("Valeur incorrect");
                    Thread.Sleep(1500);

                }
            }
        
            return char.ToUpperInvariant(result.KeyChar); ;
        }

        public string? AskTitle()
        {
            ShowMessage("Quel nom souhaitez-vous donner à votre tache ?\n");

            var newTitle = Console.ReadLine();

            return newTitle;
        }

        public string? AskDueDate()
        {
            ShowMessage("Donner une date d'échéance (format 26/01/2026) (Peux être vide)\n");

            var newDueDate = Console.ReadLine();

            return newDueDate;
        }
        public TaskPriority AskPriority()
        {
            ShowMessage("Donner une priorité à votre tâche\n Si le champ est vide la priorité sera MEDIUM par defaut");

            foreach(TaskPriority priority in Enum.GetValues<TaskPriority>())
            {
                ShowMessage($"{priority.GetString()}");
            }

            var input = Console.ReadLine();
            if (int.TryParse(input, out var num) && Enum.IsDefined(typeof(TaskPriority), num))
                return (TaskPriority)num;

            return TaskPriority.MEDIUM;
        }

        public char? AskIsCompleted()
        {

            var iscompleted = ReadAnswerYesOrNo("Voulez-vous que votre tâche apparaisse terminée ? (O/N)\n");

            return iscompleted;
        }
        

        public string GetSummary(List<TaskItem>? Tasks)
        {

            var Total = Tasks == null ? 0 : Tasks.Count();
            var NonEnded = Tasks == null ? 0 : Tasks.Where(t => !t.Iscompleted).Count();
            var Ended = Tasks == null ? 0 : Tasks.Where(t => t.Iscompleted).Count();
            var Late = Tasks == null ? 0 : Tasks.Where(t => !t.Iscompleted && (t.DueDate.HasValue && t.DueDate.Value.Date < _DateTime.Today)).Count();

            return new string($"Total tâches : {Total} \n" +
                $"Terminées : {Ended} \n" +
                $"Restantes : {NonEnded}\n" +
                $"En retard : {Late}");
        }

        public void GetMenu()
        {
            ShowMessage("Bonjour, merci de faire un choix :");

            foreach(var menuInfoValues in Enum.GetValues<MenuInfo>() )
            {
                ShowMessage( menuInfoValues.GetString());
            }

            
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
                ShowMessage("Entrée invalide");
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
