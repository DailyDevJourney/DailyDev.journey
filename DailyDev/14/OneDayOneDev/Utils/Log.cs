using OneDayOneDev.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev.Utils
{
    public class Log(FileHandler fileHandler) : ILog
    {
        FileHandler fileHandler = fileHandler;

        public void AddLog(string message)
        {
            fileHandler.AddTextToFile(fileHandler.GetLogPath(), $"{message}\n");
        }
    }
}
