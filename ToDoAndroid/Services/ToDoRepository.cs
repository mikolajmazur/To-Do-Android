using SQLite;
using System.Collections.Generic;
using System.IO;
using ToDoAndroid.Models;

namespace ToDoAndroid.Services
{
    class ToDoRepository : IToDoRepository
    {
        private readonly string _dbPath;
        public ToDoRepository()
        {
            var dbName = "todos.db";
            var dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            _dbPath = Path.Combine(dbFolder, dbName);
        }

        public void EnsureCreated()
        {
            using (var db = new SQLiteConnection(_dbPath))
            {
                db.CreateTable<ToDoItem>();
            }
        }

        public void AddAndSave(ToDoItem item)
        {
            using (var db = new SQLiteConnection(_dbPath))
            {
                db.Insert(item);
            }
        }

        public void DeleteAndSave(ToDoItem item)
        {
            using (var db = new SQLiteConnection(_dbPath))
            {
                db.Delete(item);
            }
        }

        public List<ToDoItem> GetAll()
        {
            using (var db = new SQLiteConnection(_dbPath))
            {
                return db.Table<ToDoItem>().ToList();
            }
        }
    }
}