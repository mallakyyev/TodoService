using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoBAL.Dtos;
using TodoBAL.Repositories.TodoRepository;
using TodoDAL;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ITodoRepository _todoRepo;
        private readonly ILogger<TodoItemsController> _logger;
        public TodoItemsController(TodoContext context, ITodoRepository todoRepository, ILogger<TodoItemsController> logger)
        {
            _context = context;
            _todoRepo = todoRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var responce = await _todoRepo.GetTodoItemsAsync();
            if (responce.IsSuccess)           
                return (List<TodoItemDTO>)responce.Results;
            
            _logger.LogError(responce.DisplayMessage, responce.Exception);
            return Problem(responce.DisplayMessage);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {          
            var responce = await _todoRepo.GetTodoItemAsync(id);
            if (!responce.IsSuccess)
            {
                _logger.LogError(responce.DisplayMessage, responce.Exception);
                return Problem(responce.DisplayMessage);
            }

            if (responce.Results == null)
                return NotFound();

            return (TodoItemDTO)responce.Results;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)            
                return BadRequest();            

            var responce = await _todoRepo.UpdateTodoItemAsync(todoItemDTO);

            if (!responce.IsSuccess)
            {
                _logger.LogError(responce.DisplayMessage, responce.Exception);
                return Problem(responce.DisplayMessage);
            }

            if (responce.Results == null)
                return NotFound();

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {       
            var responce = await _todoRepo.CreateTodoItemAsync(todoItemDTO);

            if (responce.IsSuccess)
                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDTO.Id}, responce.Results);

            _logger.LogError(responce.DisplayMessage, responce.Exception);
            return Problem(responce.DisplayMessage);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var responce = await _todoRepo.DeleteTodoItemAsync(id);

            if (!responce.IsSuccess)
            {
                _logger.LogError(responce.DisplayMessage, responce.Exception);
                return Problem(responce.DisplayMessage);
            }

            if (responce.Results == null)
                return NotFound();

            return NoContent();
        }
        #region Old Code Region
        /*
        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };       
        */
        #endregion
    }
}
