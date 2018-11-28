using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp
{
   public class TodoRepository : ITodoRepository
   {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context) => _context = context;

        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await _context.Todos.ToListAsync();
                
        }

        public async Task<Todo> Add(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> GetBy(int id)
        {
             return await _context.Todos.FirstOrDefaultAsync(r => r.Id == id);   
        }

        public async Task Update(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
        }

    }
    
}