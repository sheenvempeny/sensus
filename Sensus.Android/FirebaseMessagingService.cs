using Firebase.Messaging;
using Android.App;
using System;
using Xamarin.Forms;

namespace Sensus.Android
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                // log for testing
                var from = message.From;
                var body = message.GetNotification().Body;
                Console.WriteLine("From: " + from);
                Console.WriteLine("Notification Message Body: " + body);
            }
            catch (Exception ex)
            {
                // TODO : better error handling
                Console.WriteLine("Error extracting message from notification: " + ex);
            }
        }
    }
}