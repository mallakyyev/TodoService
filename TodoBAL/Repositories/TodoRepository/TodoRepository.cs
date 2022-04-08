using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoDAL;
using TodoBAL.Dtos;
using TodoDAL.Models;

namespace TodoBAL.Repositories.TodoRepository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;
   
        public TodoRepository(TodoContext todoContext, IMapper mapper)
        {
            _todoContext = todoContext;
            _mapper = mapper;           
        }
        public async Task<ResponceDTO> CreateTodoItemAsync(TodoItemDTO item)
        {
            var itemDto = _mapper.Map<TodoItem>(item);
            try
            {
                var responce = await _todoContext.CreateTodoItemAsync(itemDto);
                return new ResponceDTO(true, _mapper.Map<TodoItemDTO>(responce), $"New Todo Item with id={responce.Id} created");                    
            }catch(Exception ex)
            {
                return new ResponceDTO(false, item, "Cannot create TodoItem. Either entity with the same id is already exist or database error accured", ex);
            }                               
        }

        public async Task<ResponceDTO> DeleteTodoItemAsync(long id)
        {
            try
            {
                var responce = await _todoContext.DeleteTodoItem(id);
                return new ResponceDTO(true, responce);
            }catch(Exception ex)
            {
                return new ResponceDTO(false, null, $"Error deleting TodoItem with id={id}.", ex);
            }
        }

        public async Task<ResponceDTO> GetTodoItemAsync(long id)
        {
            try
            {
                var todoItem = await _todoContext.GetTodoItemByIdAsync(id);
                if(todoItem!=null)
                    return new ResponceDTO(true, _mapper.Map<TodoItemDTO>(todoItem));

                return new ResponceDTO(true, null);
            }
            catch (Exception ex)
            {
                return new ResponceDTO(false, null, $"Error getting TodoItem with id={id}.", ex);
            }          
        }

        public async Task<ResponceDTO> GetTodoItemsAsync()
        {
            try
            {
                var responce = await _todoContext.GetAllTodoItems();
                if(responce!=null)
                    return new ResponceDTO(true, _mapper.Map<IEnumerable<TodoItemDTO>>(responce), "Retrieving all TodoItems");

                return new ResponceDTO(true, new List<TodoItemDTO>());
            }
            catch(Exception ex)
            {
                return new ResponceDTO(false, null, "Error retrieving data(TodoItems table) from the databse", ex);
            }           
        }

        public async Task<ResponceDTO> UpdateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            try
            {
                var responce = await _todoContext.UpdateTodoItemAsync(_mapper.Map<TodoItem>(todoItemDTO));
                if(responce != null)
                {
                    return new ResponceDTO(true, responce);
                }
                return new ResponceDTO(true, null, "Entity not fount in the database");
            }catch(Exception ex)
            {
                return new ResponceDTO(false, null, $"Error accured while updating TodoItem with id={todoItemDTO.Id}", ex);
            }        
        }
    }
}
