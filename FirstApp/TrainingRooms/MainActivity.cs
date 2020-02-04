using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FirstLibrary;

namespace TrainingRooms
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : ListActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            RoomRepository repo = new RoomRepository();

            List<TrainingRoom> rooms = repo.GetRooms();

            // Change our list to something android understands
            ArrayAdapter adapter = new ArrayAdapter<TrainingRoom>(this, Resource.Layout.RoomListItem, 
                rooms.ToArray());
            this.ListAdapter = adapter;
        }

	}
}

