using System;
using SQLite;

namespace ToDoAndroid.Models
{
    [Table("ToDos")]
    class ToDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; } = false;

        public static explicit operator Java.Lang.Object(ToDoItem v)
        {
            throw new NotImplementedException();
        }
    }
}