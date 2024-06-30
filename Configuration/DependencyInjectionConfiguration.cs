using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Contexts;
using Repository.Repositories;

namespace Configurations;

public static class DependencyInjectionConfiguration
{
    public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.InjectServices();
        services.InjectDbContext(configuration);
        services.InjectRepositories();
        services.InjectAutoMapper();
    }

    private static void InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IToDoTaskService, ToDoTaskService>();
    }

    private static void InjectDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString,
                options => options.EnableRetryOnFailure()
            );
        });
    }

    private static void InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<IToDoTaskRepository, TodoTaskRepository>();
    }

    private static void InjectAutoMapper(this IServiceCollection services)
    {
        IMapper mapper = AutoMapperConfiguration.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}