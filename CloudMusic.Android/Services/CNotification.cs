using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CloudMusic.Droid.Services;
using CloudMusic.Services;

[assembly: Xamarin.Forms.Dependency(typeof(CNotification))]
namespace CloudMusic.Droid.Services
{
    public class CNotification : INotification
    {
       static NotificationManager mNotifyManager;
       static NotificationCompat.Builder mBuilder;
        public static void Init()
        {
#if (!DEBUG)
            AudioManager audioManager = (AudioManager) Application.Context.GetSystemService(Context.AudioService);
            audioManager.SetStreamVolume(Stream.Music,audioManager.GetStreamMaxVolume(Stream.Music),VolumeNotificationFlags.AllowRingerModes);        
#endif
            mNotifyManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            mBuilder = new NotificationCompat.Builder(Application.Context, "2")
              .SetContentTitle("下载进度")
              .SetProgress(100, 0, false)
              .SetSmallIcon(17301634);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                Intent notificationIntent = new Intent(Application.Context, Application.Context.Class);
                PendingIntent notificationPendingIntent = PendingIntent.GetActivity(Application.Context, 1, notificationIntent, PendingIntentFlags.UpdateCurrent);
                mBuilder.SetContentIntent(notificationPendingIntent);
                NotificationChannel mChannel = new NotificationChannel("2", "haha", Android.App.NotificationImportance.Low);
                mNotifyManager.CreateNotificationChannel(mChannel);
            }
        }

        public void ProgressUpdate(int id, int ProgressPercentage)
        {
            mBuilder.SetProgress(100, ProgressPercentage, false).SetContentText(ProgressPercentage + "%");
            mNotifyManager.Notify(id, mBuilder.Build());           
        }
        public void NotificationCancel(int id)
        {
            ((NotificationManager)Application.Context.GetSystemService(Context.NotificationService)).Cancel(id);
        }
    }
}