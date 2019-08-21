using System.Collections.Generic;
using ToDoAndroid.Models;

namespace ToDoAndroid.Services
{
    interface IToDoRepository
    {
        List<ToDoItem> GetAll();
        void AddAndSave(ToDoItem item);
        void DeleteAndSave(ToDoItem item);
    }
}