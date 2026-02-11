using Microsoft.EntityFrameworkCore;
using OneDayOneDev_DayThirteen;

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
            var taskRepository = new TaskRepository();
            var taskrules = new TaskRules();

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(taskRepository, taskrules,_dateTimeProvider));
        }
    }
}