using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;

namespace FCMClient
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        TextView msgText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            msgText = FindViewById<TextView>(Resource.Id.msgText);

            if (Intent.Extras != null)
            {
                foreach (string key in Intent.Extras.KeySet())
                {
                    string value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, $"Key: {key} Value: {value}");
                }
            }
            
            IsPlayServicesAvailable();

            CreateNotificationChannel();

            Button logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);
            logTokenButton.Click += (sender, e) =>
                {
                    Log.Debug(TAG, $"InstanceID token: {FirebaseInstanceId.Instance.Token}");
                };

            Button subscribeButton = FindViewById<Button>(Resource.Id.subscribeButton);
            subscribeButton.Click += (sender, e) =>
            {
                FirebaseMessaging.Instance.SubscribeToTopic("news");
                Log.Debug(TAG, "Subscribed to remote notifications");
            };

            Button unsubBtn = FindViewById<Button>(Resource.Id.unsubscribeButton);
            unsubBtn.Click += (sender, e) =>
            {
                FirebaseMessaging.Instance.UnsubscribeFromTopic("news");
                Log.Debug(TAG, "Unsubscribed from remote notifications");
            };

            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if(resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            } 
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android
                return;
            }

            NotificationChannel channel = new NotificationChannel(CHANNEL_ID,
                                                                  "FCM Notifications",
                                                                  NotificationImportance.Default)
            {
                Description = "Firebase Cloud Messages appear in this channel"
            };

            NotificationManager notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}