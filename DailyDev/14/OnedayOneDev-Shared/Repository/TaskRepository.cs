using Microsoft.EntityFrameworkCore;

using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Utils.Interface;

namespace OnedayOneDev_Shared.Repository
{
    public class TaskRepository : ITaskRepository
    {

        private TaskDbContext _TaskDbContext { get; set; }
        public TaskRepository()
        {
            var dataBasePath = Path.Combine(AppContext.BaseDirectory, "tasks.db");

            var options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseSqlite($"Data Source={dataBasePath}")
            .Options;

            _TaskDbContext = new TaskDbContext(options);
            _TaskDbContext.Database.EnsureCreated();
        }

        public IEnumerable<TaskItem>? GetAllTask(Filter _filter = null)
        {
            try
            {
                var tasks = _TaskDbContext.TasksList;
                if(_filter is not null)
                {
                    if (_filter.IsCompleted is not null)
                    {
                        tasks.Where(t => (bool)_filter.IsCompleted ? t.Iscompleted : !t.Iscompleted);
                    }
                    if (_filter.DateFrom is not null)
                    {
                        tasks.Where(t => t.CreatedAt >= IDateTimeProvider.ParseDate(_filter.DateFrom));
                    }
                    if (_filter.DateTo is not null)
                    {
                        tasks.Where(t => t.DueDate <= IDateTimeProvider.ParseDate(_filter.DateTo));
                    }

                    if (_filter.SearchDirection is not null)
                    {
                        if (_filter.SearchDirection == "DESC")
                            tasks.OrderByDescending(t => t.CreatedAt);
                    }
                }
                

                return tasks.ToList();
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
        public Result<TaskItem> SetTaskCompleted(int id)
        {
            try
            {
                var task = _TaskDbContext.TasksList.Find(id);
                if (task != null)
                {
                    task.Iscompleted = true;
                    _TaskDbContext.Entry(task).CurrentValues.SetValues(task);
                    _TaskDbContext.SaveChanges();

                    return Result<TaskItem>.Ok(task,"Mise à jour réussi");
                }
                else
                {
                    return Result<TaskItem>.Failed("tache inexistante");
                }

            }
            catch (Exception ex)
            {

                return Result<TaskItem>.Failed($"erreur : {ex.Message}"); ;
            }
        }
        public Result<TaskItem> SetTaskImcompleted(int id)
        {
            try
            {
                var task = _TaskDbContext.TasksList.Find(id);
                if (task != null)
                {
                    task.Iscompleted = false;
                    _TaskDbContext.Entry(task).CurrentValues.SetValues(task);
                    _TaskDbContext.SaveChanges();

                    return Result<TaskItem>.Ok(task,"Mise à jour réussi");
                }
                else
                {
                    return Result<TaskItem>.Failed("tache inexistante");
                }

            }
            catch (Exception ex)
            {

                return Result<TaskItem>.Failed($"erreur : {ex.Message}"); ;
            }
        }
        public TaskItem? GetTaskByTitle(string Title)
        {
            try
            {
                
                return  _TaskDbContext.TasksList.Where(t => t.Title.ToLower().Contains(Title.ToLower())).FirstOrDefault();
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

        public Result<TaskItem> AddTask(OnedayOneDev_Shared.DataWindow.TaskItem task)
        {
            try
            {
                if (task == null)
                {
                    return Result<TaskItem>.Failed("Erreur la tache est null");
                }
                var entity = _TaskDbContext.TasksList.Find(task.id);

                if (entity == null)
                {
                    _TaskDbContext.TasksList.Add(task);
                    _TaskDbContext.SaveChanges();

                    return Result<TaskItem>.Ok(task, "Ajout réussi");
                }
                else
                {
                    return Result<TaskItem>.Failed("tache déja existante");
                }

            }
            catch (Exception ex)
            {

                return Result<TaskItem>.Failed($"erreur : {ex.Message}"); ;
            }

        }

        public Result<TaskItem> UpdateTask(int id, TaskItem Newtask)
        {
            try
            {

                var entity = _TaskDbContext.TasksList.Find(id);

                if (entity != null)
                {
                    _TaskDbContext.Entry(entity).CurrentValues.SetValues(Newtask);
                    _TaskDbContext.SaveChanges();
                    return Result<TaskItem>.Ok(entity,"Mise à jour réussi");
                }
                else
                {
                    return Result<TaskItem>.Failed("tache inexistante");
                }

            }
            catch (Exception ex)
            {

                return Result<TaskItem>.Failed($"erreur : {ex.Message}"); ;
            }

        }



        public Result<TaskItem> DeleteTask(int id)
        {
            try
            {

                var entity = _TaskDbContext.TasksList.Find(id);

                if (entity != null)
                {
                    _TaskDbContext.TasksList.Remove(entity);
                    _TaskDbContext.SaveChanges();
                    return Result<TaskItem>.Ok(null,"suppression réussi");
                }
                else
                {
                    return Result<TaskItem>.Failed( "tache inexistante");
                }

            }
            catch (Exception ex)
            {

                return Result<TaskItem>.Failed($"erreur : {ex.Message}"); ;
            }

        }

    }
}
