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

        public void Add(Todo todo)
        {
            _context.Add(todo);
            _context.SaveChanges();
        }

        public Todo Sanitize(Todo todo)
        {
            return HtmlSanitize.SanitizeTodo(todo);
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

        public void AddCountToHeaders(HttpRequest request, IEnumerable<Todo> todos)
        {
            // conditional for testing
            if (request != null) 
            {
                request.HttpContext.Response.Headers.Add("Todos-Total-Count", todos.Count().ToString());
            }
            
        }

        public void AddIsSanitizedToHeaders(HttpRequest request, Todo todo, Todo todoSanitized)
        {
            // conditional for testing
            if (request != null)
            {
                request.HttpContext.Response.Headers.Add("Todo-Name-Is-Sanitized", (!todo.Name.Equals(todoSanitized.Name)).ToString());
            }
        }
    }
    
}