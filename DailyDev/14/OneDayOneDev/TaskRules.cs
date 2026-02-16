using OneDayOneDev.DataWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev
{
    public class TaskRules : ITaskRules
    {

        public bool IsTaskLate(TaskItem task, DateTime ReferenceDate)
        {
            if (task.DueDate == null) return false; // pas de date d'échéance

            if (task.Iscompleted) return false; //tache fini


            if (task.DueDate >= ReferenceDate) return false;

            if (task.DueDate < ReferenceDate) return true;

            return false;
        }

        public bool CanBeDeleted(TaskItem task, DateTime ReferenceDate)
        {
            if (task.Iscompleted || IsTaskLate(task, ReferenceDate)) return false;
            return true;
        }
        public bool CanBeCompleted(TaskItem task)
        {
            if (task.Iscompleted) return false;
            return true;
        }
    }
}
