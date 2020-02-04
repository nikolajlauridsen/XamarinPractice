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
using Android.Content;
using Android.Net;

[assembly:Xamarin.Forms.Dependency(typeof(HPlusSports.Droid.NetworkManager))]
namespace HPlusSports.Droid
{
    class NetworkManager : INetworkManager
    {
        private Context ctx = Android.App.Application.Context;
        public bool IsNetworkConnected()
        {
            ConnectivityManager cSvc = (ConnectivityManager) ctx.GetSystemService(Context.ConnectivityService);
            return cSvc.ActiveNetworkInfo != null && cSvc.ActiveNetworkInfo.IsConnected;
        }
    }
}