using Api.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests.Controllers;

public class ToDoTaskControllerTests
{
    private readonly Mock<IToDoTaskService> _mockTaskService;
    private readonly ToDoTaskController _controller;

    public ToDoTaskControllerTests()
    {
        _mockTaskService = new Mock<IToDoTaskService>();
        _controller = new ToDoTaskController(_mockTaskService.Object);
    }

    [Fact]
    public async Task GetTasks_ReturnsOkResult_WithListOfTasks()
    {
        // Arrange
        var tasks = new List<ToDoTaskDTO> { new() { Id = 1, Title = "Test Task" } };
        _mockTaskService.Setup(service => service.GetAllTasksAsync()).ReturnsAsync(tasks);

        // Act
        var result = await _controller.GetTasks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTasks = Assert.IsType<List<ToDoTaskDTO>>(okResult.Value);
        Assert.Single(returnTasks);
    }

    [Fact]
    public async Task GetTask_ReturnsOkResult_WithTask()
    {
        // Arrange
        var task = new ToDoTaskDTO { Id = 1, Title = "Test Task" };
        _mockTaskService.Setup(service => service.GetTaskByIdAsync(1)).ReturnsAsync(task);

        // Act
        var result = await _controller.GetTask(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTask = Assert.IsType<ToDoTaskDTO>(okResult.Value);
        Assert.Equal(1, returnTask.Id);
    }

    [Fact]
    public async Task GetTask_ReturnsNotFound_WhenTaskNotFound()
    {
        // Arrange
        _mockTaskService.Setup(service => service.GetTaskByIdAsync(1)).ReturnsAsync((ToDoTaskDTO)null);

        // Act
        var result = await _controller.GetTask(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetTasks_ReturnsOkResult_WithEmptyList()
    {
        // Arrange
        var tasks = new List<ToDoTaskDTO>();
        _mockTaskService.Setup(service => service.GetAllTasksAsync()).ReturnsAsync(tasks);

        // Act
        var result = await _controller.GetTasks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTasks = Assert.IsType<List<ToDoTaskDTO>>(okResult.Value);
        Assert.Empty(returnTasks);
    }

    [Fact]
    public async Task GetTasks_ReturnsStatusCode500_WhenExceptionIsThrown()
    {
        // Arrange
        _mockTaskService.Setup(service => service.GetAllTasksAsync()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.GetTasks();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task GetTask_ReturnsStatusCode500_WhenExceptionIsThrown()
    {
        // Arrange
        _mockTaskService.Setup(service => service.GetTaskByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.GetTask(1);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task CreateTask_ReturnsCreatedAtActionResult_WithTask()
    {
        // Arrange
        var taskDto = new ToDoTaskDTO { Id = 1, Title = "New Task" };
        _mockTaskService.Setup(service => service.AddTaskAsync(taskDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateTask(taskDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnTask = Assert.IsType<ToDoTaskDTO>(createdAtActionResult.Value);
        Assert.Equal(1, returnTask.Id);
        Assert.Equal("New Task", returnTask.Title);
    }

    [Fact]
    public async Task CreateTask_ReturnsBadRequest_WhenTaskDtoIsNull()
    {
        // Act
        var result = await _controller.CreateTask(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateTask_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = await _controller.CreateTask(new ToDoTaskDTO());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateTask_ReturnsBadRequest_WhenTitleIsEmpty()
    {
        // Arrange
        var taskDto = new ToDoTaskDTO { Id = 1, Title = "" };
        _controller.ModelState.AddModelError("Title", "Title is required");

        // Act
        var result = await _controller.CreateTask(taskDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateTask_ReturnsStatusCode500_WhenExceptionIsThrown()
    {
        // Arrange
        var taskDto = new ToDoTaskDTO { Id = 1, Title = "New Task" };
        _mockTaskService.Setup(service => service.AddTaskAsync(It.IsAny<ToDoTaskDTO>())).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.CreateTask(taskDto);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task UpdateTask_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var taskDto = new ToDoTaskDTO { Id = 1, Title = "Updated Task" };
        _mockTaskService.Setup(service => service.UpdateTaskAsync(taskDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateTask(1, taskDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsBadRequest_WhenTaskDtoIsNull()
    {
        // Act
        var result = await _controller.UpdateTask(1, null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = await _controller.UpdateTask(1, new ToDoTaskDTO());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsBadRequest_WhenIdDoesNotMatchTaskDtoId()
    {
        // Act
        var result = await _controller.UpdateTask(2, new ToDoTaskDTO { Id = 1 });

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsBadRequest_WhenTitleIsEmpty()
    {
        // Arrange
        var taskDto = new ToDoTaskDTO { Id = 1, Title = "" };
        _controller.ModelState.AddModelError("Title", "Title is required");

        // Act
        var result = await _controller.UpdateTask(1, taskDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsStatusCode500_WhenExceptionIsThrown()
    {
        // Arrange
        var taskDto = new ToDoTaskDTO { Id = 1, Title = "Updated Task" };
        _mockTaskService.Setup(service => service.UpdateTaskAsync(It.IsAny<ToDoTaskDTO>())).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.UpdateTask(1, taskDto);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task DeleteTask_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        _mockTaskService.Setup(service => service.DeleteTaskAsync(It.IsAny<ToDoTaskDTO>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteTask(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ReturnsNotFound_WhenTaskDoesNotExist()
    {
        // Arrange
        _mockTaskService.Setup(service => service.DeleteTaskAsync(It.IsAny<ToDoTaskDTO>())).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteTask(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ReturnsStatusCode500_WhenExceptionIsThrown()
    {
        // Arrange
        _mockTaskService.Setup(service => service.DeleteTaskAsync(It.IsAny<ToDoTaskDTO>())).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.DeleteTask(1);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
}