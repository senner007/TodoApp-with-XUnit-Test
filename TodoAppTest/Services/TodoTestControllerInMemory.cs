using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp;
using Xunit;

namespace TodoAppTest
{
    public class TodoTestControllerInMemory
    {
        Todo testItem = new Todo()
        {
            Name = "New Todo",
            Checkmark = false
        };

        Todo AddTodo(TodoContext context, Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }


        [Fact]
        public void GetInvalidId()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {
                //Arrange
                AddTodo(context, testItem);

                var controller = new TodoController(new TodoRepository(context));

                // Act
                var result = controller.Get(2);


                // Assert
                Assert.IsType<NotFoundObjectResult>(result);

            }
        }

        [Fact]
        public void AddInvalidId()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {
                //Arrange
                AddTodo(context, testItem);

                var controller = new TodoController(new TodoRepository(context));

                // Act
                testItem.Id = 1;
                var result = controller.PostTodo(testItem) as CreatedResult;

                // Assert
                Assert.True("New Todo" == (result.Value as Todo).Name, "Should be like, New Todo");
                Assert.True(2 == (result.Value as Todo).Id, "Should be like, 2");

            }
        }

        [Fact]
        public void UpdateInvalidId()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {
                //Arrange
                AddTodo(context, testItem);

                var controller = new TodoController(new TodoRepository(context));

                var item1 = context.Todos.First();


                item1.Checkmark = true;
                // Act
                var result = controller.Put(2, item1);

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        [Fact]
        public void GetAll()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {
                //Arrange
                AddTodo(context, testItem);

                var controller = new TodoController(new TodoRepository(context));

                // Act
                var result = controller.GetList() as OkObjectResult;


                // Assert
                Assert.True("New Todo" == (result.Value as IEnumerable<Todo>).First().Name, "Should be like, New Todo");
                Assert.True(false == (result.Value as IEnumerable<Todo>).First().Checkmark, "Should be like, false");
                Assert.True(1 == (result.Value as IEnumerable<Todo>).First().Id, "Should be like, 1");

            }
        }

        [Fact]
        public void GetId()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {
                //Arrange
                AddTodo(context, testItem);

                var controller = new TodoController(new TodoRepository(context));

                // Act
                var result = controller.Get(1) as OkObjectResult;


                // Assert
                Assert.True("New Todo" == (result.Value as Todo).Name, "Should be like, New Todo");
                Assert.True(false == (result.Value as Todo).Checkmark, "Should be like, false");
                Assert.True(1 == (result.Value as Todo).Id, "Should be like, 1");

            }
        }

        [Fact]
        public void Add()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {

                //Arrange
                var controller = new TodoController(new TodoRepository(context));

                // Act
                var result = controller.PostTodo(testItem) as CreatedResult;

                // Assert
                Assert.IsType<CreatedResult>(result);
                Assert.True("New Todo" == (result.Value as Todo).Name, "Should be like, New Todo");
                Assert.True(false == (result.Value as Todo).Checkmark, "Should be like, false");
                Assert.True(1 == (result.Value as Todo).Id, "Should be like, 1");
            }
        }

        [Fact]
        public void Update()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {
                AddTodo(context, testItem);

                var controller = new TodoController(new TodoRepository(context));

                var item1 = context.Todos.First();


                item1.Checkmark = true;
                // Act
                var result = controller.Put(1, item1);

                // Assert
                Assert.IsType<OkObjectResult>(result);
            }
        }
    }
}