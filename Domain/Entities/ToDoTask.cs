using Domain.Entities.Base;

namespace Domain.Entities;

public class ToDoTask : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }

    public ToDoTask(string title, string description, bool isCompleted)
    {
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
    }

    public void UpdateAddTime()
    {
        CreatedAt = DateTime.Now;
    }

    public void UpdateEditTime()
    {
        EditedAt = DateTime.Now;
    }
}
