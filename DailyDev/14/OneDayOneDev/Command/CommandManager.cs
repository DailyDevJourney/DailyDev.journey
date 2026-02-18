using OneDayOneDev.Command.Interface;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Resultdata;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Command
{
    public class CommandManager
    {
        private  Stack<ICommand> UndoStack = new();
        private  Stack<ICommand> RedoStack = new();

        //max 20 sauvegardes
        private readonly int CommandMax = 20;

        public int GetUndoNbr()
        {
            return UndoStack.Count;
        }

        public int GetRedoNbr()
        {
            return RedoStack.Count;
        }
        public Stack<ICommand> DeleteOlderOne(Stack<ICommand> command)
        {
            Stack<ICommand> newCommand = new();

            for (int i = 1; i < command.Count; i++)
            {
                newCommand.Push(command.ElementAt(i));
            }

            return newCommand;
        }
       
        public bool CanUndo()
        {
            return UndoStack.Count > 0;
        }
        public bool CanRedo()
        {
            return RedoStack.Count > 0;
        }

        public Result<TaskItem> Execute(ICommand command)
        {
            var result = command.Execute();

            if (result.Success) 
            {
                if (UndoStack.Count >= CommandMax) 
                {
                    UndoStack = DeleteOlderOne(UndoStack);
                }
                UndoStack.Push(command);
                RedoStack.Clear();
            }
            

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
