using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp;
using Xunit;

namespace TodoAppTest
{
    public class TodoTestsMock
    {

        [Fact]
        public void Get_Returns_ActionResults()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(new TodoData().GetTodos());
            var controller = new TodoController(mockRepo.Object);

            // Act
            var result = controller.GetList();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Todo>>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void PostXss_Returns_ActionResults()
        {
            using (var context = new TodoContext(InMemoryDb.CreateNewContextOptions()))
            {
                // Arrange
                var mockRepo = new Mock<ITodoRepository>();
                var t = new Todo { Name = "Stuff <script>alert('xss')</script>", Checkmark = false };
                context.Todos.Add(t);
                context.SaveChanges();
                var first = context.Todos.First();

                mockRepo.Setup(repo => repo.Add(t)).Returns(first);

                var controller = new TodoController(mockRepo.Object);

                // Act
                var result = controller.PostTodo(t);

                // Assert
                var viewResult = Assert.IsType<CreatedResult>(result);
                var model = Assert.IsAssignableFrom<Todo>(viewResult.Value);
                Assert.NotNull(model);
                Assert.True(false == model.Checkmark, "Checkmark should be like, false");
                Assert.True("Stuff " == model.Name, "Name should be like, Stuff ");
            }
        }


    }
}