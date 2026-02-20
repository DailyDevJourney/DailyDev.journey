using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneDayOneDev.Api;
using OneDayOneDev.Api.Request;
using OneDayOneDev.DataWindow;
using OneDayOneDev.Service;

[ApiController]
[Route("api/tasks")]
public class TasksController(TaskService taskService) : ControllerBase
{
    

    private readonly TaskService _taskService = taskService;
    

    [HttpGet("GetAllTask")]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks =  _taskService.GetTaskList();
        return tasks is null ? NotFound() : Ok(tasks);
    }

    [HttpGet("GetTaskById")]
    public IActionResult GetTaskByTitle([FromQuery] int identifiant)
    {
        var tasks = _taskService.GetTaskById(identifiant);
        return tasks is null ? NotFound() : Ok(tasks);
    }

    [HttpGet("GetTaskByTitle")]
    public  IActionResult GetTaskByTitle([FromQuery] string Title)
    {
        var tasks = _taskService.GetTaskByTitle(Title);
        return tasks is null ? NotFound() : Ok(tasks);
    }

    [HttpPost("CreateATask")]
    public IActionResult CreateATask([FromBody] TaskCreationRequest request)
    {
        var result = _taskService.CreateNewTask(
        request.Title,
        request.DueDate?.ToString(),
        request.Priority);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }
}
