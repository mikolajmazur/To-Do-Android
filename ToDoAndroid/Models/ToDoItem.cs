using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace ToDoAndroid.Models
{
    class ToDoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; } = false;

        public static explicit operator Java.Lang.Object(ToDoItem v)
        {
            throw new NotImplementedException();
        }
    }
}