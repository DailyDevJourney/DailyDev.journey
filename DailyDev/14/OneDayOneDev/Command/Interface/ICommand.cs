using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.ResultData;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Command.Interface
{
    public interface ICommand
    {
        Result<TaskItem> Execute();
        void Undo();
    }
}
