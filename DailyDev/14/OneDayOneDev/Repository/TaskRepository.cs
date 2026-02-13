using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.Repository
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

        public IEnumerable<TaskItem>? GetAllTask()
        {
            try
            {
                var tasks = _TaskDbContext.TasksList.ToList();

                return tasks;
            }
            catch (Exception ex) 
            {
                return null;
            }
        }
        public IEnumerable<TaskItem>? GetDoneTasks()
        {
            try
            {
                
                var tasks = _TaskDbContext.TasksList.Where(t => t.Iscompleted).ToList();

                return tasks;
            }
            catch (Exception ex) 
            {
                return null; 
            }
        }
        public IEnumerable<TaskItem>? GetUnDoneTasks()
        {
            try
            {
                
                var tasks = _TaskDbContext.TasksList.Where(t => !t.Iscompleted).ToList();

                return tasks;
            }
            catch (Exception ex) 
            { 
                return null; 
            }
        }
        public int? HasTasks()
        {
            try
            {
                var tasks = _TaskDbContext.TasksList.ToList();

                return tasks.Count;
            }
            catch (Exception ex) 
            { 
                return null; 
            }
        }

        public TaskItem? GetTaskById(int id)
        {
            try
            {

                return _TaskDbContext.TasksList.Find(id);
            }
            catch (Exception ex)
            {

                return null;
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
        public OperationResult SetTaskImcompleted(int id)
        {
            try
            {
                var task = _TaskDbContext.TasksList.Find(id);
                if(task != null)
                {
                    task.Iscompleted = false;
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
        public TaskItem? GetTaskByTitle(string Title)
        {
            try
            {

                return _TaskDbContext.TasksList.Where(t => t.Title.Contains(Title.Trim(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault() ;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public IEnumerable<TaskItem>? GetOrderTasks()
        {
            try
            {

                return _TaskDbContext.TasksList.OrderBy(t => t.Iscompleted).ThenBy(t => t.Title).ThenBy(t => t.id).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public OperationResult AddTask(TaskItem task)
        {
            try
            {
                if(task == null)
                {
                    return new OperationResult(false,"Erreur la tache est null");
                }
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
        
        public OperationResult UpdateTask(int id,TaskItem Newtask)
        {
            try
            {

                var entity = _TaskDbContext.TasksList.Find(id);

                if(entity != null)
                {
                    _TaskDbContext.Entry(entity).CurrentValues.SetValues(Newtask);
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
