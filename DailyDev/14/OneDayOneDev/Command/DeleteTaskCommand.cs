using OneDayOneDev.Command.Interface;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Service.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Command
{
    public class DeleteTaskCommand : ICommand
    {
        private readonly OnedayOneDev_Shared.Service.Interface.ITaskService _service;
        private TaskItem? ItemTodelete;
        private readonly int _taskId;

        public DeleteTaskCommand(OnedayOneDev_Shared.Service.Interface.ITaskService service, int ItemId)
        {
            _service = service;
            _taskId = ItemId;
        }
        public Result<TaskItem> Execute()
        {
            var Exist = _service.GetTaskById(_taskId);
            if (Exist is null)
                return Result < TaskItem >.Failed( "Tâche introuvable");
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
