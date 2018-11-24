using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace TodoApp
{
    [Route("api/[controller]")]
    [ApiController]

    

    public class TodoController : ControllerBase
    {
        void AddResponseHeader(string key, string value) 
        { 
            if (Request != null) Request.HttpContext.Response.Headers.Add(key, value);    
        }

        private readonly ITodoRepository _todoRepository;
        public TodoController(ITodoRepository todoRepository) => _todoRepository = todoRepository;

        // GET api/todos
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var todos = await _todoRepository.GetAll();
            if (todos.Count() == 0) return NoContent();
            AddResponseHeader("Todos-Total-Count", todos.Count().ToString());
            return Ok(todos);
        }

        // GET api/todos/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var todoById = await _todoRepository.GetBy(id);
            if ( todoById == null ) return NotFound("Id not found");
            return Ok(todoById);
        }

        // POST api/todos
        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Todo todo)
        {
            var todoSanitized = HtmlSanitize.Sanitize(todo);
            if (todoSanitized.Name.Length < 1) return BadRequest("Improper Name!");
            AddResponseHeader("Todo-Name-Is-Sanitized", (!todo.Name.Equals(todoSanitized.Name)).ToString());
            todo.Name = todoSanitized.Name;
            // prevent setting id
            todo.Id = 0;
            var addedTodo =  await _todoRepository.Add(todo);
            
            // TodoSanitized vil have korrect db id
            return Created("api/todo/" + addedTodo.Id, addedTodo);
        }

        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Todo todo)
        {
            var todoById = await _todoRepository.GetBy(id);
            if ( todoById == null ) return NotFound("Id not found");
            todoById.Checkmark = todo.Checkmark;
            await _todoRepository.Update(todoById);
            return Ok("PUT Ok");
        }

        // DELETE api/todos/1
        // [HttpDelete("{id}")]
        // public IActionResult Delete([FromRoute] int id)
        // {
        //     var todoById = GetTodoById(id);
        //     if ( todoById == null ) return NotFound("Id not found");
        //     _todoContext.Remove(todoById);
        //     _todoContext.SaveChanges();
        //     return Ok("Deleted id " + id);
        // }
    }
}
