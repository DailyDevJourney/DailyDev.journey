using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev_DayTwo
{
    public class TaskService
    {
        List<TaskItem> Tasks { get; set; }
        string ListFilePath = $"{AppContext.BaseDirectory}ListeTache.txt";

        public TaskService()
        { 
            Tasks = new List<TaskItem>();
            if (File.Exists(ListFilePath))
            {
                ChargerListeDepuisFichier();
            }
            else
            {
                var myFile = File.Create(ListFilePath);
                myFile.Close();
            }
        }

        public void ChargerListeDepuisFichier()
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

        public void SauvegarderListeVersFichier()
        {

            File.Delete(ListFilePath);
            System.Threading.Thread.Sleep(1000);
            var myFile = File.Create(ListFilePath);
            myFile.Close();
            using (StreamWriter outputFile = new StreamWriter(ListFilePath))
            {
                foreach (var task in Tasks)
                {
                    outputFile.WriteLine($"{task.id}|{task.Title}|{(task.Iscompleted == false ? '0' : '1')}");
                }
                    
            }
        }

        public void AjouterUneTache()
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
                Tasks.Add(new TaskItem(id: Tasks.Count() + 1, Title: TaskTitle, IsCompleted: false));
            }
                
        }

        public void AfficherLesTaches()
        {
            Console.Clear();
            string? ListOfTasks = null;
            foreach (var item in Tasks)
            {
                ListOfTasks += $"{item.id} - {item.Title} {(item.Iscompleted != true ? "[ ]" : "[X]")}\n";
            }

            Console.WriteLine($"{(ListOfTasks == null ? "Aucune taches" : ListOfTasks)} \n");

        }

        public void TerminerUneTache()
        {
            Console.WriteLine($"Quel tâche souhaitez-vous terminer ? ( saisir son identifiant ) \n touche échape pour sortir");
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            
            if (consoleKeyInfo.Key == ConsoleKey.Escape || consoleKeyInfo.Key == ConsoleKey.Enter)
            {
                return;
            }
            else
            {
                int identifiant = 0;
                bool test = int.TryParse(consoleKeyInfo.KeyChar.ToString(), out identifiant);
                if(test)
                {
                    TaskItem? task = Tasks.FirstOrDefault(t => t.id == identifiant);
                    if (task != null)
                    {
                        task.Iscompleted = true;
                        Console.WriteLine($"Tache numéro {identifiant} terminée");
                        
                    }
                    else
                    {
                        Console.WriteLine($"Aucune tache n'as pas identifiant {identifiant}");
                        
                    }
                }
                else
                {
                    Console.WriteLine("Entrée invalide");
                }
                System.Threading.Thread.Sleep(3000);

            }
        }
        public void SupprimerUneTache()
        {
            Console.WriteLine($"Quel tâche souhaitez-vous suprimer ? ( saisir son identifiant ) \n touche échape pour sortir");
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key == ConsoleKey.Escape || consoleKeyInfo.Key == ConsoleKey.Enter)
            {
                return;
            }
            else
            {
                int identifiant = 0;
                bool test = int.TryParse(consoleKeyInfo.KeyChar.ToString(), out identifiant);
                if (test)
                {
                    TaskItem? task = Tasks.FirstOrDefault(t => t.id == identifiant);
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
                else
                {
                    Console.WriteLine("Entrée invalide");
                }

                System.Threading.Thread.Sleep(3000);

            }
        }

        public void MontreMenu()
        {
            bool parsing = false;
            int result = 0;
            while (result != 5)
            {
                Console.Clear();
                string Menu = "Bonjour, merci de faire un choix : \n" +
                            "1 - Ajouter une tâche \n" +
                            "2 - Afficher les tâches \n" +
                            "3 - Marquer une tâches comme terminée\n" +
                            "4 - Supprimer une tâche\n" +
                            "5 - Quitter\n";
                Console.WriteLine(Menu);

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.Key != ConsoleKey.Escape || consoleKeyInfo.Key != ConsoleKey.Enter)
                {
                     parsing = int.TryParse(consoleKeyInfo.KeyChar.ToString(), out result);
                    
                }
               
                if(parsing)
                {
                    switch (result)
                    {
                        case 1:
                            //Ajouter une tâche
                            Console.Clear();
                            AjouterUneTache();
                            break;
                        case 2:
                            //Afficher les tâches
                            Console.Clear();
                            AfficherLesTaches();
                            Console.WriteLine("Appuyer sur un touche pour revenir au menu principal");
                            Console.ReadLine();
                            break;
                        case 3:
                            //Marquer une tâche comme terminée

                            Console.Clear();
                            AfficherLesTaches();
                            TerminerUneTache();

                            break;
                        case 4:
                            //Supprimer une tâche
                            Console.Clear();
                            AfficherLesTaches();
                            SupprimerUneTache();

                            break;
                        case 5:
                            //Quitter
                            SauvegarderListeVersFichier();
                            break;
                        default:
                            Console.WriteLine("Valeur non valide");
                            System.Threading.Thread.Sleep(3000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrée invalide");
                    System.Threading.Thread.Sleep(3000);
                }




            }
        }


    }
}
