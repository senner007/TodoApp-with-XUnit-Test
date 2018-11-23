using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp;
using Xunit;

namespace TodoAppTest
{
    public class TodoTestsMock
    {

        [Fact]
        public async Task Get_Returns_ActionResults()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new TodoData().GetTodos());
            var controller = new TodoController(mockRepo.Object);

            // Act
            var result = await controller.GetList();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Todo>>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task PostXss_Returns_ActionResults()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {
                // Arrange
                var mockRepo = new Mock<ITodoRepository>();
                var todo = new Todo { Name = "Stuff <script>alert('xss')</script>", Checkmark = false };
                
                mockRepo.Setup(repo => repo.Add(todo)).ReturnsAsync(todo);
                var controller = new TodoController(mockRepo.Object);

                // Act
                var result = await controller.PostTodo(todo) as CreatedResult;

                // Save to in-memory db - will auto increment todo.Id
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();

                // Assert

                var viewResult = Assert.IsType<CreatedResult>(result);
                Assert.NotNull(result);

                Assert.True(false == (result.Value as Todo).Checkmark, "Checkmark should be like, false");
                Assert.True("Stuff " == (result.Value as Todo).Name, "Name should be like, Stuff ");
                Assert.True(1 == (result.Value as Todo).Id, "Id should be like, 1 ");
            }
        }


    }
}