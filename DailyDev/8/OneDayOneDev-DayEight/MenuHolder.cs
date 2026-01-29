using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace OneDayOneDev_DayFive
{
    public enum MenuInfo
    {
        Add ,
        showAll ,
        Ended ,
        Delete ,
        showEnded ,
        ShowNonEnded ,
        SearchByWord ,
        nbEnded ,
        nbNonEnded ,
        ShowSortedList ,
        ShowDueDateAndNotOver ,
        ShowDueDateAndOver ,
        ShowIncomingTask ,
        ExportAllTaskToCSV ,
        ExportLateTaskToCSV ,
        ExportCompletedTaskToCSV,
        ExportNonCompletedTaskToCSV,
        Quit 
    }

    public static class MenuInfoExtension 
    {
        public static int GetNumber(this MenuInfo menuInfo)
        {
            return (int)menuInfo + 1;
        }

       
        public static string GetString(this MenuInfo menuInfo)
        {
            switch (menuInfo)
            {
                case MenuInfo.Add: return $"{menuInfo.GetNumber()} - Ajouter une tâche "; 
                case MenuInfo.showAll: return $"{menuInfo.GetNumber()} - Afficher les tâches ";
                case MenuInfo.Ended: return $"{menuInfo.GetNumber()} - Marquer une tâches comme terminée ";
                case MenuInfo.Delete: return $"{menuInfo.GetNumber()} - Supprimer une tâche ";
                case MenuInfo.showEnded: return $"{menuInfo.GetNumber()} - Montrer les tâches terminées ";
                case MenuInfo.ShowNonEnded: return $"{menuInfo.GetNumber()} - Montrer les tâches non terminées ";
                case MenuInfo.SearchByWord: return $"{menuInfo.GetNumber()} - Montrer les tâches qui contiennent un mot";
                case MenuInfo.nbEnded: return $"{menuInfo.GetNumber()} - Combien de taches non terminées";
                case MenuInfo.nbNonEnded: return $"{menuInfo.GetNumber()} - Combien de taches terminées";
                case MenuInfo.ShowSortedList: return $"{menuInfo.GetNumber()} - Montrer les tâches triées par fini puis titre";
                case MenuInfo.ShowDueDateAndNotOver: return $"{menuInfo.GetNumber()} - Montrer les tâches à échéances aujourdh'ui non finis";
                case MenuInfo.ShowDueDateAndOver: return $"{menuInfo.GetNumber()} - Montrer les tâches à échéances aujourdh'ui finis";
                case MenuInfo.ShowIncomingTask: return $"{menuInfo.GetNumber()} - Montrer les tâches à venir";
                case MenuInfo.ExportAllTaskToCSV: return $"{menuInfo.GetNumber()} - Exporter en .CSV la liste des tâches";
                case MenuInfo.ExportLateTaskToCSV: return $"{menuInfo.GetNumber()} - Exporter en .CSV la liste des tâches en retard";
                case MenuInfo.ExportCompletedTaskToCSV: return $"{menuInfo.GetNumber()} - Exporter en .CSV la liste des tâches terminée";
                case MenuInfo.ExportNonCompletedTaskToCSV: return $"{menuInfo.GetNumber()} - Exporter en .CSV la liste des tâches non terminée";
                case MenuInfo.Quit :
                    return $"{menuInfo.GetNumber()} - Quitter";
                    
            }

            return "";
        }
    }
}
