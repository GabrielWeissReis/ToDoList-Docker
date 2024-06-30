using Application.DTOs;

namespace Application.Interfaces;

public interface IToDoTaskService
{
    Task<IEnumerable<ToDoTaskDTO>> GetAllTasksAsync();
    Task<ToDoTaskDTO> GetTaskByIdAsync(int id);
    Task AddTaskAsync(ToDoTaskDTO taskDTO);
    Task UpdateTaskAsync(ToDoTaskDTO taskDTO);
    Task DeleteTaskAsync(ToDoTaskDTO taskDTO);
}
