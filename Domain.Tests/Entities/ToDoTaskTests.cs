using Domain.Entities;

namespace Domain.Tests.Entities;

public class ToDoTaskTests
{
    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        // Arrange
        string title = "Test Task";
        string description = "Test Description";
        bool isCompleted = false;

        // Act
        var task = new ToDoTask(title, description, isCompleted);

        // Assert
        Assert.Equal(title, task.Title);
        Assert.Equal(description, task.Description);
        Assert.Equal(isCompleted, task.IsCompleted);
        Assert.Equal(default, task.CreatedAt);
        Assert.Null(task.EditedAt);
        Assert.Null(task.DeletedAt);
    }

    [Fact]
    public void UpdateAddTime_SetsCreatedAtToCurrentTime()
    {
        // Arrange
        var task = new ToDoTask("Test Task", "Test Description", false);
        var beforeUpdate = DateTime.Now;

        // Act
        task.UpdateAddTime();
        var afterUpdate = DateTime.Now;

        // Assert
        Assert.True(task.CreatedAt >= beforeUpdate && task.CreatedAt <= afterUpdate);
    }

    [Fact]
    public void UpdateEditTime_SetsEditedAtToCurrentTime()
    {
        // Arrange
        var task = new ToDoTask("Test Task", "Test Description", false);
        var beforeUpdate = DateTime.Now;

        // Act
        task.UpdateEditTime();
        var afterUpdate = DateTime.Now;

        // Assert
        Assert.True(task.EditedAt.HasValue);
        Assert.True(task.EditedAt.Value >= beforeUpdate && task.EditedAt.Value <= afterUpdate);
    }
}