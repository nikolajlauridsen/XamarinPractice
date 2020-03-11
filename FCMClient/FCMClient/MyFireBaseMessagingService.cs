using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;
using FCMClient;

namespace FCMClient
{
    [Service]
    [IntentFilter(new []{ "com.google.firebase.MESSAGING_EVENT" })]
    class MyFireBaseMessagingService : FirebaseMessagingService
    {
        public const string TAG = "MyFirebaseMsgService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, $"From: {message.From}");

            string body = message.GetNotification().Body;
            string title = message.GetNotification().Title;
            Log.Debug(TAG, $"Notification Message Body: {body}");
            SendNotification(body, title, message.Data);
        }

        void SendNotification(string messageBody, string title, IDictionary<string, string> data)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (string key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            PendingIntent pendingIntent =
                PendingIntent.GetActivity(this, MainActivity.NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);

            var notificicationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                            .SetSmallIcon(Resource.Drawable.tab_about)
                                            .SetContentTitle(title)
                                            .SetContentText(messageBody)
                                            .SetAutoCancel(true)
                                            .SetContentIntent(pendingIntent);
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificicationBuilder.Build());

        }
    }
}