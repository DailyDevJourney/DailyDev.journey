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
    public class CompletTaskCommand : ICommand
    {
        private readonly OnedayOneDev_Shared.Service.Interface.ITaskService _service;
        private readonly int _TaskId;

        public CompletTaskCommand(OnedayOneDev_Shared.Service.Interface.ITaskService service, int Id)
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
