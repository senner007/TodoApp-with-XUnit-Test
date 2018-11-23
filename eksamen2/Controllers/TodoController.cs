using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace TodoApp
{
    [Route("api/[controller]")]
    [ApiController]

    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        public TodoController(ITodoRepository todoRepository) => _todoRepository = todoRepository;

        // GET api/todos
        [HttpGet]
        public IActionResult GetList()
        {
            var todos = _todoRepository.GetAll();
            if (todos.Count() == 0) return NoContent();
            _todoRepository.AddCountToHeaders(Request, todos);
           
            return Ok(todos);
        }

        // GET api/todos/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var todoById = _todoRepository.GetBy(id);
            if ( todoById == null ) return NotFound("Id not found");
            return Ok(todoById);
        }

        // POST api/todos
        [HttpPost]
        public IActionResult PostTodo([FromBody] Todo todo)
        {
            var todoSanitized = _todoRepository.Sanitize(todo);
            if (todoSanitized.Name.Length < 1) return BadRequest("Improper Name!");
            _todoRepository.Add(todoSanitized);
            _todoRepository.AddIsSanitizedToHeaders(Request, todo, todoSanitized);

            // TodoSanitized vil indeholde korrect db id
            return Created("api/todo/" + todoSanitized.Id, todoSanitized);
        }

        // PUT api/todos/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Todo todo)
        {
            var todoById = _todoRepository.GetBy(id);
            if ( todoById == null ) return NotFound("Id not found");
            todoById.Checkmark = todo.Checkmark;
            _todoRepository.Update(todoById);
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
