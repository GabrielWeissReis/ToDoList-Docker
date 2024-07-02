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
        try
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("GetTask/{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        try
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask(ToDoTaskDTO taskDto)
    {
        try
        {
            if (taskDto == null)
                return BadRequest("Task data is missing");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskService.AddTaskAsync(taskDto);

            return CreatedAtAction(nameof(GetTask), new { id = taskDto.Id }, taskDto);
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("UpdateTask/{id}")]
    public async Task<IActionResult> UpdateTask(int id, ToDoTaskDTO taskDto)
    {
        try
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
        catch
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("DeleteTask/{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            await _taskService.DeleteTaskAsync(new ToDoTaskDTO { Id = id });
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}
