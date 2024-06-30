using Domain.Entities;

namespace Domain.Interfaces;

public interface IToDoTaskRepository : IBaseRepository<ToDoTask>
{
    Task<bool> TaskExistsByTitleAsync(string taskTitle);
}
