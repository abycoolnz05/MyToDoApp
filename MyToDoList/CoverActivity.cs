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
using AlertDialog = Android.App.AlertDialog;

namespace MyToDoList
{
    [Activity(Label = "CoverActivity", WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden)]
    public class CoverActivity : ListActivity
    {
        public List<string> Items { get; set; } // list to hold items

        ArrayAdapter<string> adapter; // Array Adapter is used to connect data to the listview

        ISharedPreferences prefs = Application.Context.GetSharedPreferences("TODO_DATA", FileCreationMode.Private); //file where todo items will be stored.

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_cover);

            Button btnMainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);
            btnMainMenu.Click += BtnMainMenu_Click;

            //Initialise the list
            Items = new List<string>();

            //Load list of items from Shared Preferences
            LoadList();

            // Add the list of items to the listview
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice);
            ListAdapter = adapter;

            Button addButton = FindViewById<Button>(Resource.Id.AddButton);
            addButton.Click += delegate
            {
                EditText itemText = FindViewById<EditText>(Resource.Id.itemText); //get EditTextbox and take item out of it
                string item = itemText.Text;

                if (item == "" || item == null)   //
                {
                    return;
                }

                Items.Add(item);                           // Add item to Main list of items

                adapter.Add(item);                       // Add item to the Adapter list
                adapter.NotifyDataSetChanged();         // Let listview know thw adapter list has changed

                itemText.Text = "";                     // clear textbox for new entry

                UpdatedStoredData();                    //Update the key/value pair in shared prefs
            };
        }

        private void BtnMainMenu_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }


        //**********************Functions****************************************************************//
        public void LoadList()    // Loads items in the shared prefs amd populate list 
        {
            int count = prefs.GetInt("itemCount", 0);

            if (count > 0)
            {
                Toast.MakeText(this, "Getting saved items...", ToastLength.Short).Show();

                for (int i = 0; i <= count; i++) // loop through the number of items
                {
                    string item = prefs.GetString(i.ToString(), null);
                    if (item != null)
                    {
                        Items.Add(item); // add item in the list
                    }
                }
            }
        }

        public void UpdatedStoredData() // Updates the stored key/value pairs in Shared Prefs
        {
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Clear();
            editor.Commit();            //Removes the current items in shared preferences


            editor = prefs.Edit();     //Add all items in the list inorder to re open list again
            editor.PutInt("ItemCount", Items.Count); //keeps track of stored items

            int counter = 0;
            foreach (string item in Items)     //loop each item, add to the shared prefs
            {
                editor.PutString(counter.ToString(), item);
                counter++;
            }

            editor.Apply();
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id) // checked item click
        {
            base.OnListItemClick(l, v, position, id);

            RunOnUiThread(() =>
            {
                AlertDialog.Builder builder;
                builder = new AlertDialog.Builder(this);
                builder.SetTitle(" Confirm ");
                builder.SetMessage(" Are you done with this item ?");
                builder.SetCancelable(true);

                builder.SetPositiveButton(" OK ", delegate
                {
                    //remove item from listview
                    var item = Items[position];
                    Items.Remove(item);
                    adapter.Remove(item);

                    //reset listview l
                    l.ClearChoices();
                    l.RequestLayout();

                    UpdatedStoredData();
                });

                builder.SetNegativeButton(" Cancel ", delegate
                { return; });

                builder.Show(); //Launches the popup!

            }
                );
        }
    }
}