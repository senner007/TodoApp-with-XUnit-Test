using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp;


namespace TodoAppTest
{
    public class TestClientProvider_TestDb : IDisposable
    {
        private TestServer server;
        public HttpClient Client {get; set;}
        private readonly TodoContext context;
        public TestClientProvider_TestDb()
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

     public class TestClientProvider_Production : IDisposable
    {
        private TestServer server;
        public HttpClient Client {get; set;}
        //private readonly TodoContext context;
        public TestClientProvider_Production()
        {
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            // TODO :  better way to get TodoApp path?
            var parent = Directory.GetParent(wanted_path).Parent;
       
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(parent.ToString())
            .AddJsonFile("TodoApp\\appsettings.json")
            .Build();

           
            WebHostBuilder webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureServices(s => s.AddDbContext<TodoContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))));
            webHostBuilder.UseStartup<Startup>();

            server = new TestServer(webHostBuilder);

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