using Microsoft.VisualBasic;
using OneDayOneDev.Command.Interface;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Service;
using OneDayOneDev.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneDayOneDev.Command
{
    public class UpdateCommand : Interface.ICommand
    {
        private readonly Service.Interface.ITaskService _service;
        private readonly int TaskId;
        private readonly string taskTitle;
        private readonly string taskDueDate;
        private readonly bool TaskDone;
        private readonly TaskPriority _taskpriority;

        private TaskItem OldMemo;

        public UpdateCommand(Service.Interface.ITaskService service, int id, string Title, string dueDate, bool Done, TaskPriority priority)
        {
            _service = service;
            TaskId = id;

            taskTitle = Title;
            taskDueDate = dueDate;
            TaskDone = Done;
            _taskpriority = priority;
        }
        public OperationResult Execute()
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
