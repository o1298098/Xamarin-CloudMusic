using System;
using Xamarin.Forms;
namespace CloudMusic.Services
{
    public interface IStatusBarStyleManager
    {
        void SetLightTheme();
        void SetDarkTheme();
    }
}
