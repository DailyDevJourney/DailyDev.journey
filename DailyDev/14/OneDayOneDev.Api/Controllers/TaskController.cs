using Microsoft.AspNetCore.Mvc;
using OneDayOneDev;
using OnedayOneDev_Shared.Request;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared;
using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.ResultData;
using Microsoft.AspNetCore.Authorization;
using OnedayOneDev_Shared.Service.Interface;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskService taskService) : ControllerBase
{
    

    private readonly ITaskService _taskService = taskService;
    

    [HttpGet("GetAllTask")]
    public async Task<IActionResult> GetAllTasks([FromQuery] GetRequest request)
    {
        var tasks =  _taskService.GetTaskList(request._filter);

        return tasks is null ? NotFound() : Ok(tasks?.ConvertToPageResult(page: request.PageIndex, pageSize: request.PageSize, Filter: request._filter));
    }

    [HttpGet("GetTaskById")]
    public IActionResult GetTaskById([FromQuery] int identifiant)
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
    public IActionResult CreateATask([FromQuery] CreationRequest request)
    {
        var result = _taskService.CreateNewTask(
        request.Title,
        request.DueDate?.ToString(),
        request.Priority);

        return result.Success
            ? Ok(result)
            : BadRequest(result.Message);
    }

    [HttpDelete("DeleteATask")]
    public IActionResult DeleteATask(int identifiant)
    {
        var result = _taskService.DeleteTask(identifiant);

        return result.Success
            ? Ok(result)
            : BadRequest(result.Message);
    }

    [HttpPut("SetTaskCompleted")]
    public IActionResult SetTaskCompleted([FromBody] int identifiant)
    {
        var result = _taskService.SetTaskCompleted(identifiant);

        return result.Success
            ? Ok(result)
            : BadRequest(result.Message);
    }

    [HttpPut("SetTaskIncompleted")]
    public IActionResult SetTaskIncompleted([FromBody] int identifiant)
    {
        var result = _taskService.SetTaskImcompleted(identifiant);

        return result.Success
            ? Ok(result)
            : BadRequest(result.Message);
    }

    [HttpPut("UpdateTask")]
    public IActionResult UpdateTask([FromBody] UpdateRequest request)
    {
        var result = _taskService.UpdateTask(request.identifiant,request.NewTitle,request.NewDueDate,request.NewIscompleted,request.priority);

        return result.Success
            ? Ok(result)
            : BadRequest(result.Message);
    }
}
