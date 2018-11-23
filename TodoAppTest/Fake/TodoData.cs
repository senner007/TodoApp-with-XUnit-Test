using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp;

namespace TodoAppTest
{
    public class TodoData
    {
        public IEnumerable<Todo> GetTodos()
        {
     
            var todos = new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Name = "Stuff",
                    Checkmark = false
                },
                new Todo
                {
                    Id = 2,
                    Name = "More Stuff",
                    Checkmark = false
                },
            };

            return todos;       
        }
    }
}