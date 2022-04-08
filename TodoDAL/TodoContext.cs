
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoDAL.Models;

namespace TodoDAL
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
        {
            TodoItems.Add(todoItem);
            await base.SaveChangesAsync();
            return todoItem;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodoItems()
        {
            return await TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(long id)
        {
            return await TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> DeleteTodoItem(long id)
        {
            var todoItem = await TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                TodoItems.Remove(todoItem);
                await base.SaveChangesAsync();               
            }
            return todoItem;
        }

        public async Task<TodoItem> UpdateTodoItemAsync(TodoItem item)
        {
            var prev = await TodoItems.FindAsync(item.Id);
            if (prev != null)
            {
                prev.IsComplete = item.IsComplete;
                prev.Name = item.Name;
                TodoItems.Update(prev);
                await base.SaveChangesAsync();                
            }
            return prev;
        }
    }
}
