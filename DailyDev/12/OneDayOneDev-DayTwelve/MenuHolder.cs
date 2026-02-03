namespace OneDayOneDev_DayTwelve
{
    public enum MenuInfo
    {
        AddTask ,
        UpdateATask,
        DeleteTask,
        SetTaskToCompleted,

        showAllTask ,
        SearchTaskByWord,

        ShowSortedTaskList,
        ShowDueDateAndNotOver,
        ShowDueDateAndOver,
        ShowIncomingTask,
        showCompletedTask ,
        ShowNotCompletedTask ,
        
        nbOfImcompletedTask ,
        nbOfcompletedTask ,
        
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
                case MenuInfo.AddTask: return $"{menuInfo.GetNumber()} - Ajouter une tâche "; 
                case MenuInfo.showAllTask: return $"{menuInfo.GetNumber()} - Afficher les tâches ";
                case MenuInfo.SetTaskToCompleted: return $"{menuInfo.GetNumber()} - Marquer une tâches comme terminée ";
                case MenuInfo.DeleteTask: return $"{menuInfo.GetNumber()} - Supprimer une tâche ";
                case MenuInfo.showCompletedTask: return $"{menuInfo.GetNumber()} - Montrer les tâches terminées ";
                case MenuInfo.ShowNotCompletedTask: return $"{menuInfo.GetNumber()} - Montrer les tâches non terminées ";
                case MenuInfo.SearchTaskByWord: return $"{menuInfo.GetNumber()} - Montrer les tâches qui contiennent un mot";
                case MenuInfo.nbOfImcompletedTask: return $"{menuInfo.GetNumber()} - Combien de taches non terminées";
                case MenuInfo.nbOfcompletedTask: return $"{menuInfo.GetNumber()} - Combien de taches terminées";
                case MenuInfo.ShowSortedTaskList: return $"{menuInfo.GetNumber()} - Montrer les tâches triées par fini puis titre";
                case MenuInfo.ShowDueDateAndNotOver: return $"{menuInfo.GetNumber()} - Montrer les tâches à échéances aujourdh'ui non finis";
                case MenuInfo.ShowDueDateAndOver: return $"{menuInfo.GetNumber()} - Montrer les tâches à échéances aujourdh'ui finis";
                case MenuInfo.ShowIncomingTask: return $"{menuInfo.GetNumber()} - Montrer les tâches à venir";
                case MenuInfo.ExportAllTaskToCSV: return $"{menuInfo.GetNumber()} - Exporter en .CSV la liste des tâches";
                case MenuInfo.ExportLateTaskToCSV: return $"{menuInfo.GetNumber()} - Exporter en .CSV la liste des tâches en retard";
                case MenuInfo.ExportCompletedTaskToCSV: return $"{menuInfo.GetNumber()} - Exporter en .CSV la liste des tâches terminée";
                case MenuInfo.ExportNonCompletedTaskToCSV: return $"{menuInfo.GetNumber()} - Exporter en .CSV la liste des tâches non terminée";
                case MenuInfo.UpdateATask: return $"{menuInfo.GetNumber()} - Mettre à jour une tâche";
                case MenuInfo.Quit :
                    return $"{menuInfo.GetNumber()} - Quitter";
                    
            }

            return "";
        }
    }
}
