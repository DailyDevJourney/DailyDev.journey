using Microsoft.EntityFrameworkCore;
using OneDayOneDev.Utils;
using OnedayOneDev_Shared;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Utils;
using OnedayOneDev_Shared.Utils.Interface;

namespace OneDayOneDev
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var _dateTimeProvider = new SystemDateTimeProvider();
            var _taskrules = new TaskRules();
            var _FileHandler = new FileHandler(_dateTimeProvider);
            var _LogHandler = new Log(_FileHandler) ;
            var _repository = new OnedayOneDev_Shared.Repository.TaskRepository();
           

            var taskservice = new TaskService(_taskrules, _LogHandler,_repository, _dateTimeProvider);
            var taskrules = new TaskRules();

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(taskservice, taskrules,_dateTimeProvider));
        }
    }
}