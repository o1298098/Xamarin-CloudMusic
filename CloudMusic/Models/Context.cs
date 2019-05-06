using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CloudMusic.Models
{
    public class Context : INotifyPropertyChanged
    {
        public string APPVersion { get; private set;}
        public double Scaleparam { get => PrismApplication.Current.MainPage.Width / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width; }



    public  Context()
        {
            VersionTracking.Track();
            APPVersion = VersionTracking.CurrentVersion;
         
        }         

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
