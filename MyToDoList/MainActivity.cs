using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using AlertDialog = Android.App.AlertDialog;
using System;

namespace MyToDoList
{
    [Activity(Label = "My ToDo List", Theme = "@style/AppTheme" , MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.activity_main);

            Button btnStartNewList = FindViewById<Button>(Resource.Id.btnStartNewList);  //1. Start New List Button
            btnStartNewList.Click += BtnStartNewList_Click;

            Button btnQuit = FindViewById<Button>(Resource.Id.btnQuit); //2.  Quit Button
            btnQuit.Click += BtnQuit_Click;
        }

        // Click Events

        private void BtnStartNewList_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CoverActivity));
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }

}