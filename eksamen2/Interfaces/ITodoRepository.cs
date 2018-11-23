
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp
{
   public interface ITodoRepository 
   {
       Task<Todo> Add(Todo todo);
    
       Task<IEnumerable<Todo>> GetAll();
       Task<Todo> GetBy(int id);
       void Update(Todo todo);
    }
    
}