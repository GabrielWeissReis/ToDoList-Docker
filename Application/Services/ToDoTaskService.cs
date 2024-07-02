using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ToDoTaskService : IToDoTaskService
{
    private readonly IToDoTaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public ToDoTaskService(IToDoTaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ToDoTaskDTO>> GetAllTasksAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ToDoTaskDTO>>(tasks);
    }

    public async Task<ToDoTaskDTO> GetTaskByIdAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            return null;
        }
        return _mapper.Map<ToDoTaskDTO>(task);
    }

    public async Task AddTaskAsync(ToDoTaskDTO taskDTO)
    {
        if (await _taskRepository.TaskExistsByTitleAsync(taskDTO.Title))
            throw new ArgumentException($"A task with the title '{taskDTO.Title}' already exists.");

        var task = _mapper.Map<ToDoTask>(taskDTO);

        task.UpdateAddTime();

        await _taskRepository.AddAsync(task);
    }

    public async Task UpdateTaskAsync(ToDoTaskDTO taskDTO)
    {
        var task = await _taskRepository.GetByIdAsync(taskDTO.Id);
        if (task != null)
        {
            _mapper.Map(taskDTO, task);

            task.UpdateEditTime();

            await _taskRepository.UpdateAsync(task);
        }
    }

    public async Task DeleteTaskAsync(ToDoTaskDTO taskDTO)
    {
        var task = await _taskRepository.GetByIdAsync(taskDTO.Id);
        if (task != null)
        {
            await _taskRepository.DeleteAsync(task);
            return;
        }

        throw new KeyNotFoundException("Task not found");
    }
}
