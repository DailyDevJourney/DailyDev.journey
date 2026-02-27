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
    public class AddTaskCommand : ICommand
    {
        private readonly OnedayOneDev_Shared.Service.Interface.ITaskService _service;
        private readonly TaskItem _ItemToAdd;
        private readonly ApiClient _api;

        public AddTaskCommand(ApiClient api,OnedayOneDev_Shared.Service.Interface.ITaskService service,TaskItem taskToAdd)
        {
            _service = service;
            _ItemToAdd = taskToAdd;
            _api = api;
        }
        public async Task<Result<TaskItem>> Execute()
        {
            try
            {
                

                return await _api.CreateATaskAsync(_ItemToAdd);
            }
            catch (Exception ex) 
            {
                    return Result<TaskItem>.Failed(ex.Message);
            }


         }
        

        public async Task Undo()
        {
            await _api.DeleteTaskAsync(_ItemToAdd.id);
        }
    }
}
