using OneDayOneDev.Command.Interface;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Command
{
    public class AddTaskCommand : ICommand
    {
        private readonly OnedayOneDev_Shared.Service.Interface.ITaskService _service;
        private readonly TaskItem _ItemToAdd;

        public AddTaskCommand(OnedayOneDev_Shared.Service.Interface.ITaskService service,TaskItem taskToAdd)
        {
            _service = service;
            _ItemToAdd = taskToAdd;
        }
        public Result<TaskItem> Execute()
        {
            
            return _service.CreateNewTask(_ItemToAdd);
        }
        

        public void Undo()
        {
            _service.DeleteTask(_ItemToAdd.id);
        }
    }
}
