using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Push;
using Xamarin.Forms;

namespace CloudMusic.Actions
{
    public class MicrosoftAppCenterHandler
    {
        public static void Init()
        {
            if (!AppCenter.Configured)
            {
                AppCenter.LogLevel = LogLevel.Verbose;
               // Distribute.ReleaseAvailable = OnReleaseAvailable;
                Push.PushNotificationReceived += (sender, e) =>
                {
                    // Add the notification message and title to the message
                    var summary = $"Push notification received:" +
                                        $"\n\tNotification title: {e.Title}" +
                                        $"\n\tMessage: {e.Message}";

                    // If there is custom data associated with the notification,
                    // print the entries
                    if (e.CustomData != null)
                    {
                        summary += "\n\tCustom data:\n";
                        foreach (var key in e.CustomData.Keys)
                        {
                            summary += $"\t\t{key} : {e.CustomData[key]}\n";
                        }
                    }

                    // Send the notification summary to debug output
                    System.Diagnostics.Debug.WriteLine(summary);
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => await App.Current.MainPage.DisplayAlert("通知", summary, "ok"));
                };
            }

            AppCenter.Start("android=79620d9c-3f11-4764-8ffc-4ca0f19c7a5c;ios=852b0096-9e07-4b4e-b6c3-5e594ac237a2", typeof(Analytics), typeof(Crashes), typeof(Push));            
        }
        static bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            AppCenterLog.Info("AppCenterDemo", "OnReleaseAvailable id=" + releaseDetails.Id
                                            + " version=" + releaseDetails.Version
                                            + " releaseNotesUrl=" + releaseDetails.ReleaseNotesUrl);
            var custom = releaseDetails.ReleaseNotes?.ToLowerInvariant().Contains("custom") ?? false;
            if (custom)
            {
                var title = "Version " + releaseDetails.ShortVersion + " available!";
                Task answer;
                if (releaseDetails.MandatoryUpdate)
                {
                    answer = App.Current.MainPage.DisplayAlert(title, releaseDetails.ReleaseNotes, "更新");
                }
                else
                {
                    answer = App.Current.MainPage.DisplayAlert(title, releaseDetails.ReleaseNotes, "更新", "稍后");
                }
                answer.ContinueWith((task) =>
                {
                    if (releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                    {
                        Distribute.NotifyUpdateAction(UpdateAction.Update);
                    }
                    else
                    {
                        Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                    }
                });
            }
            return custom;
        }
    }
}
