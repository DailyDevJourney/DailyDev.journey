using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev
{
    public class Log(FileHandler fileHandler)
    {
        FileHandler fileHandler = fileHandler;

        public void AddLog(string message) 
        {
            this.fileHandler.AddTextToFile(this.fileHandler.GetLogPath(),$"{ message}\n");
        }
    }
}
