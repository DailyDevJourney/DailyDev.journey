using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev_DayTwo
{
    public enum MenuInfo
    {
        Add = 1,showAll = 2,Ended=3,Delete=4,showEnded=5,ShowNonEnded=6,SearchByWord= 7,nbEnded=8,nbNonEnded=9,Quit=10
    }
    public class TaskService
    {
        List<TaskItem> Tasks { get; set; }
        string ListFilePath = $"{AppContext.BaseDirectory}ListeTache.txt";

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

        public void LoadTaskFile()
        {
            using (StreamReader sr = new StreamReader(ListFilePath))
            {
                string? line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] valeur = line.Split('|');
                    Tasks.Add(new TaskItem(id: int.Parse(valeur[0]), Title: valeur[1], IsCompleted: (valeur[2] == "0" ? false : true)));
                }

            }
        }

        public void SaveTaskFile()
        {
            File.WriteAllLines(ListFilePath,
                Tasks.Select(t => $"{t.id}|{t.Title}|{(t.Iscompleted == false ? '0' : '1')}"));
            
        }

        public void CreateNewTask()
        {
            string? TaskTitle = null;
            while (TaskTitle == null || string.IsNullOrEmpty(TaskTitle))
            {
                Console.Clear();
                string add = "Ajouter une tâche : \n" +
                            "Donner un nom à votre tâches \n";

                Console.WriteLine(add);

                TaskTitle = Console.ReadLine();
            }

            TaskItem? task = Tasks.FirstOrDefault(t => t.Title == TaskTitle);
            if (task != null)
            {
                Console.WriteLine("Une tache possédent le même nom existe déja");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                Tasks.Add(new TaskItem(id: GetNewId(), Title: TaskTitle, IsCompleted: false));
            }
                
        }

        public void ShowTasksList(List<TaskItem> List)
        {
            Console.Clear();
            string? ListOfTasks = null;
            foreach (var item in List)
            {
                ListOfTasks += $"{item.id} - {item.Title} {(item.Iscompleted != true ? "[ ]" : "[X]")}\n";
            }

            Console.WriteLine($"{(ListOfTasks == null ? "Aucune taches" : ListOfTasks)} \n");

        }

        public int GetNewId()
        {
            return Tasks.Count == 0 ? 1 : Tasks.Max(t => t.id);
        }
        private int RetrieveCustomerInputForId()
        {
            Console.WriteLine("ID de la tâche : ");
            var input = Console.ReadLine();

            if(!int.TryParse(input, out var id))
            {
                Console.WriteLine("Entrée invalide");
                Thread.Sleep(1500);
                return -1;
            }
            else
            {
                return id;
            }
        }

        public TaskItem? SearchTaskById(int id)
        {
            return Tasks.FirstOrDefault(t => t.id == id);
        }

        public void EndTask()
        {
            
                int identifiant = RetrieveCustomerInputForId();

                TaskItem? task = SearchTaskById(identifiant);
                if (task != null)
                {
                    task.Iscompleted = true;
                    Console.WriteLine($"Tache numéro {identifiant} terminée");
                        
                }
                else
                {
                    Console.WriteLine($"Aucune tache n'as pas identifiant {identifiant}");
                        
                }
                
                System.Threading.Thread.Sleep(3000);

            
        }
        public void DeleteTask()
        {
            int identifiant = RetrieveCustomerInputForId();

            TaskItem? task = SearchTaskById(identifiant);
            if (task != null)
            {
                Tasks.Remove(task);
                Console.WriteLine($"Tache numéro {identifiant} supprimée");

            }
            else
            {
                Console.WriteLine($"Aucune tache n'as pas identifiant {identifiant}");

            }
        }
        

        public List<TaskItem> ShowEndedTasks()
        {
            return Tasks.Where(t => t.Iscompleted).ToList();
        }

        public List<TaskItem> ShowNonEndedTasks()
        {
            return Tasks.Where(t => !t.Iscompleted).ToList();
        }
        public List<TaskItem> SearchTaskByTitle(string Recherche)
        {
            return Tasks.Where(t => t.Title.Contains(Recherche,StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public int GetNumberOfEndedTask()
        {
            var ListOfEndedTask = ShowEndedTasks();
            return ListOfEndedTask.Count();
        }
        public int GetNumberOfNonEndedTask()
        {
            var ListOfNonEndedTask = ShowNonEndedTasks();
            return ListOfNonEndedTask.Count();
        }

        public void ShowMenu()
        {
            bool parsing = false;
            var result = 0;
            while (result != (int)MenuInfo.Quit)
            {
                Console.Clear();
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
                            "10 - Quitter\n";
                Console.WriteLine(Menu);

                var input = Console.ReadLine();

                if (!int.TryParse(input, out result))
                {
                    Console.WriteLine("Entrée invalide");
                    Thread.Sleep(1500);
                    
                }
                else
                {
                    Console.Clear();
                    switch (result)
                    {
                        case (int)MenuInfo.Add:
                            //Ajouter une tâche
                            CreateNewTask();
                            break;
                        case (int)MenuInfo.showAll:
                            //Afficher les tâches

                            ShowTasksList(Tasks);
                            Console.WriteLine("Appuyer sur un touche pour revenir au menu principal");
                            Console.ReadLine();
                            break;
                        case (int)MenuInfo.Ended:
                            //Marquer une tâche comme terminée
                            ShowTasksList(Tasks);
                            EndTask();

                            break;
                        case (int)MenuInfo.Delete:
                            //Supprimer une tâche

                            ShowTasksList(Tasks);
                            DeleteTask();

                            break;
                        case (int)MenuInfo.showEnded:
                            //Taches terminées

                            ShowTasksList(ShowEndedTasks());
                            Console.WriteLine("Appuyer sur un touche pour revenir au menu principal");
                            Console.ReadLine();

                            break;
                        case (int)MenuInfo.ShowNonEnded:
                            //Taches non terminée

                            ShowTasksList(ShowNonEndedTasks());
                            Console.WriteLine("Appuyer sur un touche pour revenir au menu principal");
                            Console.ReadLine();


                            break;
                        case (int)MenuInfo.SearchByWord:
                            //Contient un mot

                            Console.WriteLine("Quel tâches recherchez vous?");
                            string? Mot = Console.ReadLine()?.ToString();
                            if (!string.IsNullOrEmpty(Mot))
                            {
                                ShowTasksList(SearchTaskByTitle(Mot));
                            }
                            Console.WriteLine("Appuyer sur un touche pour revenir au menu principal");
                            Console.ReadLine();

                            break;
                        case (int)MenuInfo.nbEnded:
                            //nb taches fini
                            Console.WriteLine($"Il y à {GetNumberOfEndedTask()} tache(s) terminée(s)");
                            Console.WriteLine("Appuyer sur un touche pour revenir au menu principal");
                            Console.ReadLine();
                            break;
                        case (int)MenuInfo.nbNonEnded:
                            //nb taches non fini
                            Console.WriteLine($"Il y à {GetNumberOfNonEndedTask()} tache(s) non terminée(s)");
                            Console.WriteLine("Appuyer sur un touche pour revenir au menu principal");
                            Console.ReadLine();
                            break;
                        case (int)MenuInfo.Quit:
                            //Quitter
                            SaveTaskFile();
                            break;
                        default:
                            Console.WriteLine("Valeur non valide");
                            System.Threading.Thread.Sleep(3000);
                            break;
                    }
                }

                
               




            }
        }


    }
}
