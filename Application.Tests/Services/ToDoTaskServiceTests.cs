using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Application.Tests.Services;

public class ToDoTaskServiceTests
{
    private readonly Mock<IToDoTaskRepository> _mockTaskRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ToDoTaskService _service;

    public ToDoTaskServiceTests()
    {
        _mockTaskRepository = new Mock<IToDoTaskRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new ToDoTaskService(_mockTaskRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllTasksAsync_ReturnsListOfTaskDTOs()
    {
        // Arrange
        var tasks = new List<ToDoTask> { new("Test Task", "Description", false) };
        var taskDTOs = new List<ToDoTaskDTO> { new() { Id = 1, Title = "Test Task" } };

        _mockTaskRepository.Setup(repo => repo.GetAllAsync(null)).ReturnsAsync(tasks);
        _mockMapper.Setup(m => m.Map<IEnumerable<ToDoTaskDTO>>(tasks)).Returns(taskDTOs);

        // Act
        var result = await _service.GetAllTasksAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ReturnsTaskDTO_WhenTaskExists()
    {
        // Arrange
        var task = new ToDoTask("Test Task", "Description", false) { Id = 1 };
        var taskDTO = new ToDoTaskDTO { Id = 1, Title = "Test Task" };

        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(1, null)).ReturnsAsync(task);
        _mockMapper.Setup(m => m.Map<ToDoTaskDTO>(task)).Returns(taskDTO);

        // Act
        var result = await _service.GetTaskByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ReturnsNull_WhenTaskDoesNotExist()
    {
        // Arrange
        int taskId = 1;
        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskId, null)).ReturnsAsync((ToDoTask)null);

        // Act
        var result = await _service.GetTaskByIdAsync(taskId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddTaskAsync_ThrowsArgumentException_WhenTaskWithSameTitleExists()
    {
        // Arrange
        var taskDTO = new ToDoTaskDTO { Title = "Test Task" };

        _mockTaskRepository.Setup(repo => repo.TaskExistsByTitleAsync(taskDTO.Title)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.AddTaskAsync(taskDTO));
    }

    [Fact]
    public async Task AddTaskAsync_CallsAddAsync_WhenTaskIsValid()
    {
        // Arrange
        var taskDTO = new ToDoTaskDTO { Title = "New Task", Description = "Description", IsCompleted = false };
        var task = new ToDoTask("New Task", "Description", false);

        _mockTaskRepository.Setup(repo => repo.TaskExistsByTitleAsync(taskDTO.Title)).ReturnsAsync(false);
        _mockMapper.Setup(m => m.Map<ToDoTask>(taskDTO)).Returns(task);

        // Act
        await _service.AddTaskAsync(taskDTO);

        // Assert
        _mockTaskRepository.Verify(repo => repo.AddAsync(task), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_CallsUpdateAsync_WhenTaskExists()
    {
        // Arrange
        var taskDTO = new ToDoTaskDTO { Id = 1, Title = "Updated Task", Description = "Description", IsCompleted = true };
        var task = new ToDoTask("Old Task", "Old Description", false) { Id = 1 };

        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskDTO.Id, null)).ReturnsAsync(task);
        _mockMapper.Setup(m => m.Map(taskDTO, task)).Returns(task);

        // Act
        await _service.UpdateTaskAsync(taskDTO);

        // Assert
        _mockTaskRepository.Verify(repo => repo.UpdateAsync(task), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_CallsDeleteAsync_WhenTaskExists()
    {
        // Arrange
        var taskDTO = new ToDoTaskDTO { Id = 1 };
        var task = new ToDoTask("Task to be deleted", "Description", false) { Id = 1 };

        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskDTO.Id, null)).ReturnsAsync(task);

        // Act
        await _service.DeleteTaskAsync(taskDTO);

        // Assert
        _mockTaskRepository.Verify(repo => repo.DeleteAsync(task), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_ThrowsKeyNotFoundException_WhenTaskNotFound()
    {
        // Arrange
        var taskDTO = new ToDoTaskDTO { Id = 1 };

        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskDTO.Id, null)).ReturnsAsync((ToDoTask)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteTaskAsync(taskDTO));
    }
}