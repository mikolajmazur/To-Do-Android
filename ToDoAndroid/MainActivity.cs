using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ToDoAndroid.Models;
using ToDoAndroid.UI;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace ToDoAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ToDoListAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _adapter = new ToDoListAdapter(this);
            var listView = (ListView)FindViewById<ListView>(Resource.Id.toDoListView);
            listView.Adapter = _adapter;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var input = new EditText(this);
            input.InputType = Android.Text.InputTypes.ClassText;
            var todoText = String.Empty;
            var alertDialogBuilder = new AlertDialog.Builder(this)
            // TODO: replace with resource string
                .SetTitle("Add To Do")
                .SetView(input)
                .SetPositiveButton(Android.Resource.String.Ok, (alertSender, args) => 
                {
                    var userInput = input.Text;
                    _adapter.Add(new ToDoItem
                    {
                        Text = userInput,
                        IsDone = false
                    });
                    //((AlertDialog)alertSender).Dismiss();
                } )
                .SetNegativeButton(Android.Resource.String.Cancel, 
                (alertSender, args) => ((AlertDialog)alertSender).Cancel() )
                .Show();

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

