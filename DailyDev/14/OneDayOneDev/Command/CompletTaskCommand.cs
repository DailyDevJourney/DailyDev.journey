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
    public class CompletTaskCommand : ICommand
    {
        private readonly Service.Interface.ITaskService _service;
        private readonly int _TaskId;

        public CompletTaskCommand(Service.Interface.ITaskService service, int Id)
        {
            _service = service;
            _TaskId = Id;
        }
        public Result<TaskItem> Execute()
        {

            return this._service.SetTaskCompleted(_TaskId);
        }

        public void Undo()
        {
            this._service.SetTaskImcompleted(_TaskId);
        }
    }
}
