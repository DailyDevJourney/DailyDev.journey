using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Service;

[ApiController]
[Route("api/tasks")]
public class TasksController(TaskService taskService, TaskDbContext db) : ControllerBase
{
    private readonly TaskDbContext _db = db;

    private readonly TaskService _taskService = taskService;
    

    [HttpGet("GetAllTask")]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _db.TasksList.AsNoTracking().ToListAsync();
        return Ok(tasks);
    }

    [HttpGet("GetTaskByTitle")]
    public  IActionResult GetTaskByTitle([FromQuery] string Title)
    {
        var tasks = _taskService.GetTaskByTitle(Title);
        return tasks is null ? NotFound() : Ok(tasks);
    }
}
