
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp
{
   public interface ITodoRepository 
   {
       void Add(Todo todo);
       Todo Sanitize(Todo todo); 
       IEnumerable<Todo> GetAll();
       Todo GetBy(int id);
       void Update(Todo todo);
       void AddCountToHeaders(HttpRequest request, IEnumerable<Todo> todos);
       void AddIsSanitizedToHeaders(HttpRequest request, Todo todo, Todo todoSanitized);
    }
    
}