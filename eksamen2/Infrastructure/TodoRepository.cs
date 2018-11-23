using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp
{
   public class TodoRepository : ITodoRepository
   {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context) => _context = context;

        public IEnumerable<Todo> GetAll()
        {
            return _context.Todos;
        }

        public Todo Add(Todo todo)
        {
            _context.Add(todo);
            _context.SaveChanges();
            return todo;
        }

        public Todo GetBy(int id)
        {
             return _context.Todos.Where(r => r.Id == id).FirstOrDefault();   
        }

        public void Update(Todo todo)
        {
            _context.Update(todo);
            _context.SaveChanges();
        }

    }
    
}