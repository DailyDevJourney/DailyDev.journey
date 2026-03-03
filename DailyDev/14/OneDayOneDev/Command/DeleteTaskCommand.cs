using OneDayOneDev.Command.Interface;
using OneDayOneDev.http;
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
        
        private readonly ApiClient _api;
        private TaskItem? ItemTodelete;
        private readonly int _taskId;

        public DeleteTaskCommand(ApiClient api, int ItemId)
        {
        
            _taskId = ItemId;
            this._api = api;
        }
        public async Task<Result<TaskItem?>?> Execute()
        {
            var Exist = await _api.GetTaskByIdAsync(_taskId);
            if (Exist is null)
                return Result < TaskItem? >.Failed( "Tâche introuvable");
            ItemTodelete = Exist.Clone();
            var result= await _api.DeleteTaskAsync(_taskId);

            var response = (result) ? 
                new Result<TaskItem>(true, $"Suppression réussie de la tache {_taskId}", ItemTodelete) : 
                new Result<TaskItem>(false, $"erreur lors de la suppression de la tache {_taskId}", ItemTodelete);
            
            
            return response;
        }

        public async Task Undo()
        {
           if(ItemTodelete is null)
            {
                return;
            }
            _ = await _api.CreateATaskAsync(ItemTodelete);
        }
    }
}
