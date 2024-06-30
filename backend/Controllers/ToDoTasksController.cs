using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoTasksController : ControllerBase
{
    private readonly IToDoTaskService _taskService;

    public ToDoTasksController(IToDoTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(ToDoTaskDTO taskDto)
    {
        if (taskDto == null)
        {
            return BadRequest();
        }

        await _taskService.AddTaskAsync(taskDto);
        return CreatedAtAction(nameof(GetTask), new { id = taskDto.Id }, taskDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, ToDoTaskDTO taskDto)
    {
        if (id != taskDto.Id)
        {
            return BadRequest();
        }

        await _taskService.UpdateTaskAsync(taskDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _taskService.DeleteTaskAsync(new ToDoTaskDTO { Id = id });
        return NoContent();
    }
}
