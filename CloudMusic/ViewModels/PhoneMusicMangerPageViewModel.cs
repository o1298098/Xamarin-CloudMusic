using CloudMusic.Models;
using CloudMusic.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CloudMusic.ViewModels
{
    public class PhoneMusicMangerPageViewModel : BaseViewModel
    {
        public PhoneMusicMangerPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GetLocalMusic();
        }
        void GetLocalMusic()
        {
            Task.Run(async () => {
                var q = await DependencyService.Get<IAudioPicker>().GetAudioFileAsync();
                if (q?.Count > 0)
                {
                    ObservableCollection<AudioModel> r = new ObservableCollection<AudioModel>();
                    foreach (var f in q)
                        r.Add(f);
                    Device.BeginInvokeOnMainThread(()=> songList = r);
                }
            });

        }
        ObservableCollection<AudioModel> _songList;
        public ObservableCollection<AudioModel> songList
        {
            get => _songList;
            set => SetProperty(ref _songList,value, "songList");
        }
    }
}
