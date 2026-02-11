using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev
{
    public class TaskRepository
    {

        private TaskDbContext _TaskDbContext { get; set; }
        public TaskRepository()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseSqlite("Data Source=tasks.db")
            .Options;

            _TaskDbContext = new TaskDbContext(options);
            _TaskDbContext.Database.EnsureCreated();
        }

        public IEnumerable<TaskItem> GetAllTask()
        {
            try
            {
                var tasks = _TaskDbContext.TasksList.ToList();

                return tasks;
            }
            catch (Exception ex) 
            { 
                return new List<TaskItem>(); 
            }
        }

        public TaskItem GetTaskById(int id)
        {
            try
            {

                return _TaskDbContext.TasksList.Find(id);
            }
            catch (Exception ex)
            {

                return new TaskItem(Title: "", DateTime.Today, TaskService.ParseDate(""));
            }
        }
        public OperationResult SetTaskCompleted(int id)
        {
            try
            {
                var task = _TaskDbContext.TasksList.Find(id);
                if(task != null)
                {
                    task.Iscompleted = true;
                    _TaskDbContext.Entry(task).CurrentValues.SetValues(task);
                    _TaskDbContext.SaveChanges();

                    return new OperationResult(true, "Mise à jour réussi");
                }
                else
                {
                    return new OperationResult(false, "tache inexistante");
                }

            }
            catch (Exception ex)
            {

                return new OperationResult(false, $"erreur : {ex.Message}"); ;
            }
        }
        public TaskItem GetTaskByTitle(string Title)
        {
            try
            {

                return _TaskDbContext.TasksList.Find(Title);
            }
            catch (Exception ex)
            {

                return new TaskItem( Title: "", DateTime.Today, TaskService.ParseDate(""));
            }
        }

        public OperationResult AddTask(TaskItem task)
        {
            try
            {

                var entity = _TaskDbContext.TasksList.Find(task.id);

                if(entity == null)
                {
                    _TaskDbContext.TasksList.Add(task);
                    _TaskDbContext.SaveChanges();

                    return new OperationResult(true,"Ajout réussi");
                }
                else
                {
                    return new OperationResult(false, "tache déja existante");
                }
                 
            }
            catch (Exception ex)
            {

                return  new OperationResult(false, $"erreur : {ex.Message}"); ;
            }
            
        }
        public OperationResult AddTask(string? TaskTitle, string? DueDate, TaskPriority priority = TaskPriority.MEDIUM)
        {
            try
            {
                var newTask = new TaskItem( TaskTitle, DateTime.Today, TaskService.ParseDate(DueDate), false, null,priority);
                return AddTask(newTask);
                 
            }
            catch (Exception ex)
            {

                return  new OperationResult(false, $"erreur : {ex.Message}"); ;
            }
            
        }
        public OperationResult UpdateTask(TaskItem task)
        {
            try
            {

                var entity = _TaskDbContext.TasksList.Find(task.id);

                if(entity != null)
                {
                    _TaskDbContext.Entry(entity).CurrentValues.SetValues(task);
                    _TaskDbContext.SaveChanges();
                    return new OperationResult(true,"Mise à jour réussi");
                }
                else
                {
                    return new OperationResult(false, "tache inexistante");
                }
                 
            }
            catch (Exception ex)
            {

                return  new OperationResult(false, $"erreur : {ex.Message}"); ;
            }
            
        }
        public OperationResult UpdateTask(int identifiant, string NewTitle, string NewDueDate, bool NewIscompleted, TaskPriority priority)
        {
            try
            {
                var newTask = new TaskItem( NewTitle, DateTime.Today, TaskService.ParseDate(NewDueDate), NewIscompleted, null, priority);
                newTask.id = identifiant;
                return UpdateTask(newTask);
                
                 
            }
            catch (Exception ex)
            {

                return  new OperationResult(false, $"erreur : {ex.Message}"); ;
            }
            
        }

        public OperationResult DeleteTask(TaskItem task)
        {
            try
            {

                var entity = _TaskDbContext.TasksList.Find(task.id);

                if (entity != null)
                {
                    _TaskDbContext.TasksList.Remove(task);
                    _TaskDbContext.SaveChanges();
                    return new OperationResult(true, "suppression réussi");
                }
                else
                {
                    return new OperationResult(false, "tache inexistante");
                }

            }
            catch (Exception ex)
            {

                return new OperationResult(false, $"erreur : {ex.Message}"); ;
            }

        }
        public OperationResult DeleteTask(int id)
        {
            try
            {

                var entity = _TaskDbContext.TasksList.Find(id);

                if (entity != null)
                {
                    _TaskDbContext.TasksList.Remove(entity);
                    _TaskDbContext.SaveChanges();
                    return new OperationResult(true, "suppression réussi");
                }
                else
                {
                    return new OperationResult(false, "tache inexistante");
                }

            }
            catch (Exception ex)
            {

                return new OperationResult(false, $"erreur : {ex.Message}"); ;
            }

        }

    }
}
