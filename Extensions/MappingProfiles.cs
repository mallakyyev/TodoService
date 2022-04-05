using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoBAL.Dtos;
using TodoDAL.Models;

namespace TodoApiDTO.Extensions
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TodoItemDTO, TodoItem>().ReverseMap();
        }
    }
}
