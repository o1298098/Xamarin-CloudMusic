//using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.Actions
{
    public class FCMPushNotification
    {
        public static void Init()
        {
            //CrossFirebasePushNotification.Current.Subscribe("general");
            //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            //};
            //System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            //CrossFirebasePushNotification.Current.OnNotificationReceived +=(s, p) =>
            //{
            //    try
            //    {
            //        System.Diagnostics.Debug.WriteLine("Received");
            //        if (p.Data.ContainsKey("body"))
            //        {
            //            string body = string.Empty;
            //            foreach (var data in p.Data)
            //            {
            //                body += $"{data.Key} : {data.Value}\r\n";
            //                System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //            }
            //            Device.BeginInvokeOnMainThread(async ()=> await App.Current.MainPage.DisplayAlert("通知", body, "ok"));
            //        }
            //    }
            //    catch (System.Exception ex)
            //    {

            //    }

            //};
            //CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Action");

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
            //        foreach (var data in p.Data)
            //        {
            //            System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //        }

            //    }

            //};
        }
    }
}
