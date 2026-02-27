using OneDayOneDev.Command.Interface;
using OneDayOneDev.http;
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
        private readonly ApiClient _api;

        public CompletTaskCommand(ApiClient api,OnedayOneDev_Shared.Service.Interface.ITaskService service, int Id)
        {
            _service = service;
            _TaskId = Id;
            _api = api;
        }
        public async Task<Result<TaskItem>> Execute()
        {

            return await this._api.SetTaskDoneAsync(_TaskId);
        }

        public async Task Undo()
        {
            _ = await this._api.SetTaskUndoneAsync(_TaskId);
        }
    }
}
