using Microsoft.EntityFrameworkCore;

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
            var taskservice = new Service.TaskService(new Repository.TaskRepository(), _dateTimeProvider);
            var taskrules = new TaskRules();

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(taskservice, taskrules,_dateTimeProvider));
        }
    }
}