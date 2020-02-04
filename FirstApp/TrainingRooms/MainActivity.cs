using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
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

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            // Create an intent to signal what we intent to do, IE. Open TrainingRoomDetailActivity from MainActivity
            Intent intent = new Intent(this, typeof(TrainingRoomDetailActivity));
            // Get the selected TrainingRoom from the ListAdapter
            TrainingRoom selectedItem = ((ArrayAdapter<TrainingRoom>) ListAdapter).GetItem(position);
            // Add extra information to the intent (The room id)
            intent.PutExtra("roomId", selectedItem.Id);
            // Navigate to the TrainingRoomDetailActivity
            StartActivity(intent);
        }
    }
}

