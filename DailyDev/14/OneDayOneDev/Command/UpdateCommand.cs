using Microsoft.VisualBasic;
using OneDayOneDev.Command.Interface;
using OneDayOneDev.http;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneDayOneDev.Command
{
    public class UpdateCommand : Interface.ICommand
    {
        private readonly OnedayOneDev_Shared.Service.Interface.ITaskService _service;
        private readonly int TaskId;
        private readonly string taskTitle;
        private readonly string taskDueDate;
        private readonly bool TaskDone;
        private readonly OnedayOneDev_Shared.TaskPriority _taskpriority;
        private readonly ApiClient _api;

        private TaskItem OldMemo;

        public UpdateCommand(ApiClient api,OnedayOneDev_Shared.Service.Interface.ITaskService service, int id, string Title, string dueDate, bool Done, OnedayOneDev_Shared.TaskPriority priority)
        {
            _service = service;
            TaskId = id;

            taskTitle = Title;
            taskDueDate = dueDate;
            TaskDone = Done;
            _taskpriority = priority;
            _api = api;
        }
        public async Task<Result<TaskItem>> Execute()
        {
            var exist = await _api.GetTaskByIdAsync(TaskId);
            OldMemo ??= exist.Clone();
            var result = await _api.UpdateTaskAsync(TaskId, taskTitle, taskDueDate, TaskDone, _taskpriority);
            return result;
        }

        public async Task Undo()
        {
            if(OldMemo == null)
            {
                return ;
            }

            _ = await _api.UpdateTaskAsync(TaskId, OldMemo.Title, OldMemo.DueDate?.ToString("dd/MM/yyyy"),OldMemo.Iscompleted,OldMemo.Priority);
        }
    }
}
