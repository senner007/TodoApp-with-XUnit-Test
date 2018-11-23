using System;
using Microsoft.EntityFrameworkCore;
using Xunit;

using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TodoApp;

namespace TodoAppTest
{
    public static class InMemoryDb
    {
        // https://stackoverflow.com/questions/38890269/how-to-isolate-ef-inmemory-database-per-xunit-test
        public static DbContextOptions<TodoContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .UseInternalServiceProvider(serviceProvider);
        
            return builder.Options;
        }
    }

}