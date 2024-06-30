using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoTaskController : ControllerBase
{
    private readonly IToDoTaskService _taskService;

    public ToDoTaskController(IToDoTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("GetTasks")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();

        return Ok(tasks);
    }

    [HttpGet("GetTask/{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask(ToDoTaskDTO taskDto)
    {
        if (taskDto == null)
            return BadRequest("Task data is missing");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _taskService.AddTaskAsync(taskDto);

        return CreatedAtAction(nameof(GetTask), new { id = taskDto.Id }, taskDto);
    }

    [HttpPut("UpdateTask/{id}")]
    public async Task<IActionResult> UpdateTask(int id, ToDoTaskDTO taskDto)
    {
        if (taskDto == null)
            return BadRequest("Task data is missing");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != taskDto.Id)
            return BadRequest();

        await _taskService.UpdateTaskAsync(taskDto);

        return NoContent();
    }

    [HttpDelete("DeleteTask/{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _taskService.DeleteTaskAsync(new ToDoTaskDTO { Id = id });

        return NoContent();
    }
}
