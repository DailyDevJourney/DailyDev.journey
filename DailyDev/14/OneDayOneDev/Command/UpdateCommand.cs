using Microsoft.VisualBasic;
using OneDayOneDev.Command.Interface;
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

        private TaskItem OldMemo;

        public UpdateCommand(OnedayOneDev_Shared.Service.Interface.ITaskService service, int id, string Title, string dueDate, bool Done, OnedayOneDev_Shared.TaskPriority priority)
        {
            _service = service;
            TaskId = id;

            taskTitle = Title;
            taskDueDate = dueDate;
            TaskDone = Done;
            _taskpriority = priority;
        }
        public Result<TaskItem> Execute()
        {
            var exist = _service.GetTaskById(TaskId);
            OldMemo ??= exist.Clone();
            return _service.UpdateTask(TaskId, taskTitle, taskDueDate, TaskDone,_taskpriority);
        }

        public void Undo()
        {
            if(OldMemo == null)
            {
                return ;
            }

            _service.UpdateTask(TaskId, OldMemo.Title, OldMemo.DueDate?.ToString("dd/MM/yyyy"),OldMemo.Iscompleted,OldMemo.Priority);
        }
    }
}
