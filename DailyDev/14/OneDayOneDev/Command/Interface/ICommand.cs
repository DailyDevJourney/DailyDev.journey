using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Command.Interface
{
    public interface ICommand
    {
        OperationResult Execute();
        void Undo();
    }
}
