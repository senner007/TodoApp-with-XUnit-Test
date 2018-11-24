using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace TodoApp
{
    public class TodoContext : DbContext
    {

        public TodoContext()
        {

        }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=TodoApp");
        // }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity<Todo>(entity =>
            {
                // Id is auto incremented in db and not required in post
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Checkmark)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Todo>().HasData(new Todo()
            {
                Id = 1,
                Name = "Do stuff",
                Checkmark = false
            });

        }
    }
    
    
}