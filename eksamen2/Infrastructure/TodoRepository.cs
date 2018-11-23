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
            await _context.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> GetBy(int id)
        {
             return await _context.Todos.Where(r => r.Id == id).FirstOrDefaultAsync();   
        }

        public void Update(Todo todo)
        {
            _context.Update(todo);
            _context.SaveChanges();
        }

    }
    
}