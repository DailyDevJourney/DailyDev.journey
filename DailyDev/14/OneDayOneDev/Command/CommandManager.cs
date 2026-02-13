using OneDayOneDev.Command.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Command
{
    public class CommandManager
    {
        private readonly Stack<ICommand> UndoStack = new();
        private readonly Stack<ICommand> RedoStack = new();

       
        public bool CanUndo()
        {
            return UndoStack.Count > 0;
        }
        public bool CanRedo()
        {
            return RedoStack.Count > 0;
        }

        public OperationResult  Execute(ICommand command)
        {
            var result = command.Execute();
            UndoStack.Push(command);
            RedoStack.Clear();

            return result;
        }

        public void Undo()
        {
            if (!CanUndo()) return;

            var cmd = UndoStack.Pop();
            cmd.Undo();
            RedoStack.Push(cmd);

        }

        public void Redo()
        {
            if (!CanRedo()) return;

            var cmd = RedoStack.Pop();
            cmd.Execute();
            UndoStack.Push(cmd);
        }

    }
}
