using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Configurations;

public class AutoMapperConfiguration
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<ToDoTask, ToDoTaskDTO>().ReverseMap();
        });
    }
}
