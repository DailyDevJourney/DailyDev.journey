using Microsoft.EntityFrameworkCore;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Identification;

public class TaskDbContext : DbContext
{
    
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TaskItem> TasksList => Set<TaskItem>();
    public DbSet<User> Users => Set<User>();

    
    

    
}