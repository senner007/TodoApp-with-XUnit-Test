using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using TodoApp;


namespace TodoAppTest
{
    public class TestClientProvider : IDisposable
    {
        private TestServer server;
        public HttpClient Client {get; set;}
        private readonly TodoContext context;
        public TestClientProvider()
        {
            server = new TestServer( new WebHostBuilder()
            .UseEnvironment("Testing")
            .UseStartup<Startup>());

            context = server.Host.Services.GetService(typeof(TodoContext)) as TodoContext;
            
            // https://github.com/aspnet/EntityFrameworkCore/issues/11666
            // Trigger hasdata seeding to immemory
            // context.Database.EnsureCreated();

            // inject Recipe in in-memory db
            context.Todos.Add(new Todo()
            {
                Name = "New Todo",
                Checkmark = false
            });

            context.SaveChanges();
            
            Client = server.CreateClient();

           
        }

        public void Dispose()
        {
            //if server and client not null
            server?.Dispose();
            Client?.Dispose();
        }
    }

}