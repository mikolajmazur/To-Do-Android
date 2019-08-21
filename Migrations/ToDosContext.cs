using Microsoft.EntityFrameworkCore;
using ToDoAndroid.Models;

namespace ToDoAndroid.Contexts
{
    class ToDosContext : DbContext
    {
        public DbSet<ToDoItem> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=blah.db");
        }
    }
}