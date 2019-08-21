using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.EntityFrameworkCore;
using ToDoAndroid.Models;

namespace ToDoAndroid.Contexts
{
    class ToDosContext : DbContext
    {
        public DbSet<ToDoItem> ToDos { get; set; }

        public ToDosContext()
        {
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbName = "toDos.db";
            var dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(dbFolder, dbName);

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}