using Microsoft.EntityFrameworkCore;
using OneDayOneDev.DataWindow;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TaskItem> TasksList => Set<TaskItem>();
}