using Microsoft.EntityFrameworkCore;
using OneDayOneDev.Utils;

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
            var _dateTimeProvider = new Utils.SystemDateTimeProvider();
            var _taskrules = new TaskRules();
            var _FileHandler = new Utils.FileHandler(_dateTimeProvider);
           var _LogHandler = new Utils.Log(_FileHandler) ;
            var _repository = new Repository.TaskRepository();
           

            var taskservice = new Service.TaskService(_taskrules, _LogHandler,_repository, _dateTimeProvider);
            var taskrules = new TaskRules();

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(taskservice, taskrules,_dateTimeProvider));
        }
    }
}