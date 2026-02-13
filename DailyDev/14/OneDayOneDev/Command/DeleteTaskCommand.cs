using OneDayOneDev.Command.Interface;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Service;
using OneDayOneDev.Service.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Command
{
    public class DeleteTaskCommand : ICommand
    {
        private readonly Service.Interface.ITaskService _service;
        private TaskItem? ItemTodelete;
        private readonly int _taskId;

        public DeleteTaskCommand(Service.Interface.ITaskService service, int ItemId)
        {
            _service = service;
            _taskId = ItemId;
        }
        public OperationResult Execute()
        {
            var Exist = _service.GetTaskById(_taskId);
            if (Exist is null)
                return new OperationResult(false, "Tâche introuvable");
            ItemTodelete = Exist.Clone();
            return _service.DeleteTask(_taskId);
        }

        public void Undo()
        {
           if(ItemTodelete is null)
            {
                return;
            }
            _service.CreateNewTask(ItemTodelete);
        }
    }
}
