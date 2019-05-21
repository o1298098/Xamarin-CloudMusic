using System;
using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using CloudMusic.Services;
using Xamarin.Forms;
using CloudMusic.Droid.Services;
using Android.Views;

[assembly: Dependency(typeof(StatusBarStyleManager))]

namespace CloudMusic.Droid.Services
{
    public class StatusBarStyleManager : IStatusBarStyleManager
    {
        public void SetDarkTheme()
        {
            var activity = MainActivity.Instance as Activity;
            activity.Window.DecorView.SystemUiVisibility = 0;
        }

        public void SetLightTheme()
        {
            var activity = MainActivity.Instance as Activity;
            activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
        }

    }
}
