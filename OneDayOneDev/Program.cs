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
            var taskService = new TaskService(_dateTimeProvider);

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(taskService,_dateTimeProvider));
        }
    }
}