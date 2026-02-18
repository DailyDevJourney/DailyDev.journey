using OneDayOneDev.Command.Interface;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Resultdata;
using OneDayOneDev.Service;
using OneDayOneDev.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Command
{
    public class AddTaskCommand : ICommand
    {
        private readonly Service.Interface.ITaskService _service;
        private readonly TaskItem _ItemToAdd;

        public AddTaskCommand(Service.Interface.ITaskService service,TaskItem taskToAdd)
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
