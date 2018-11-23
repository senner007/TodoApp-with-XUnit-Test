
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp
{
   public interface ITodoRepository 
   {
       Todo Add(Todo todo);
    
       IEnumerable<Todo> GetAll();
       Todo GetBy(int id);
       void Update(Todo todo);
    }
    
}