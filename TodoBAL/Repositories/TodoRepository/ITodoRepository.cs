using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoBAL.Dtos;

namespace TodoBAL.Repositories.TodoRepository
{
    public interface ITodoRepository
    {
        Task<ResponceDTO> CreateTodoItemAsync(TodoItemDTO item);
        Task<ResponceDTO> GetTodoItemAsync(long id);
        Task<ResponceDTO> UpdateTodoItemAsync(TodoItemDTO todoItemDTO);
        Task<ResponceDTO> DeleteTodoItemAsync(long id);
        Task<ResponceDTO> GetTodoItemsAsync();
    }
}
