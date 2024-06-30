using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using Repository.Repositories.Base;

namespace Repository.Repositories;

public class TodoTaskRepository : BaseRepository<ToDoTask>, IToDoTaskRepository
{
    private readonly AppDbContext _appDbContext;

    public TodoTaskRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> TaskExistsByTitleAsync(string taskTitle)
    {
        return await _appDbContext.ToDoTasks.AnyAsync(t => t.Title == taskTitle);
    }
}