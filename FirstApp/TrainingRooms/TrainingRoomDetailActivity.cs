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
using FirstLibrary;

namespace TrainingRooms
{
    [Activity(Label = "TrainingRoomDetailActivity")]
    public class TrainingRoomDetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TrainingRoomDetail);

            // Get ID from intent
            int roomID = Intent.GetIntExtra("roomId", 0);
            // Create a repo and get the specified room
            RoomRepository repo = new RoomRepository();
            TrainingRoom room = repo.GetRoom(roomID);

            // Set data
            this.Title = "Room Detail";
            this.FindViewById<TextView>(Resource.Id.txtName).Text = room.Name;
            this.FindViewById<TextView>(Resource.Id.txtLocation).Text = room.Location;
        }
    }
}