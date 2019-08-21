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
using ToDoAndroid.Contexts;
using ToDoAndroid.Models;

namespace ToDoAndroid.UI
{
    class ToDoListAdapter : BaseAdapter
    {
        private List<ToDoItem> _toDos;
        private MainActivity _mainActivity;

        public ToDoListAdapter(MainActivity mainActivity)
        {
            using (var context = new ToDosContext())
            {
                _toDos = context.ToDos.Select(o => o).ToList();
            }
            _mainActivity = mainActivity;
        }

        public override int Count => _toDos.Count;

        public override Java.Lang.Object GetItem(int position) =>
            (Java.Lang.Object) _toDos[position];

        public override long GetItemId(int position) => _toDos[position].Id;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder;
            if (convertView == null)
            {
                // if we can't reuse old row layout create new one
                var layoutInflater = (LayoutInflater)_mainActivity.GetSystemService(Context.LayoutInflaterService);
                convertView = layoutInflater.Inflate(Resource.Layout.todo_row, parent, false);
                viewHolder = new ViewHolder(convertView);
                viewHolder.DeleteButton.Click += DeleteButton_Click;
                convertView.Tag = viewHolder;
            }
            else
            {
                // we can reuse old layout - get ViewHolder containing references
                // to checkbox and delete button
                viewHolder = (ViewHolder)convertView.Tag;
            }

            var currentItem = _toDos[position];

            var checkbox = viewHolder.CheckBox;
            checkbox.Checked = currentItem.IsDone;
            checkbox.Text = currentItem.Text;

            return convertView;
        }

        public void Add(ToDoItem toDo)
        {
            _toDos.Add(toDo);
            using (var context = new ToDosContext())
            {
                context.Add(toDo);
                context.SaveChanges();
            }
            NotifyDataSetChanged();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var toDoToRemove = GetToDoToRemove((ImageButton)sender);
            using (var context = new ToDosContext())
            {
                context.ToDos.Remove(toDoToRemove);
                context.SaveChanges();
            }

            _toDos.Remove(toDoToRemove);
            NotifyDataSetChanged();
        }

        private ToDoItem GetToDoToRemove(ImageButton deleteButton)
        {
            var listViewRowLayout = (RelativeLayout) deleteButton.Parent;
            var listView = (ListView)listViewRowLayout.Parent;
            var position = listView.GetPositionForView(listViewRowLayout);

            return _toDos[position];
        }

        private class ViewHolder : Java.Lang.Object
        {
            public CheckBox CheckBox { get; private set; }
            public ImageButton DeleteButton { get; private set; }
            public ViewHolder(View view)
            {
                CheckBox = (CheckBox)view.FindViewById<CheckBox>(Resource.Id.ToDoCheckbox);
                DeleteButton = (ImageButton)view.FindViewById<ImageButton>(Resource.Id.deleteButton);
            }
        }
    }
}