using Domain.Entities;
using Domain.Interfaces;
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
}
