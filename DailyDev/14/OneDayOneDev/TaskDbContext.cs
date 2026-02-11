using Microsoft.EntityFrameworkCore;
using OneDayOneDev;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TaskItem> TasksList => Set<TaskItem>();
}