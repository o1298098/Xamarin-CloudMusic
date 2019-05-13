using CloudMusic.Models;
using CloudMusic.Models.Media;
using CloudMusic.Services;
using MediaManager;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CloudMusic.ViewModels
{
    public class PhoneMusicMangerPageViewModel : BaseViewModel
    {
        public PhoneMusicMangerPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GetLocalMusic();
            PlayBtnClickedCommand = new DelegateCommand(PlayBtnClicked);
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
        async void PlayBtnClicked()
        {
            var MusicPlayList = new MusicPlayListDetail();
            MusicPlayList.playlist.trackCount = songList.Count;
            MusicPlayList.playlist.tracks = new List<Track>();
            foreach (var q in songList)
                MusicPlayList.playlist.tracks.Add(new Track
                {
                    dt = (int)q.Duration,
                    ar = new List<Ar> { new Ar { name = q.Artist } },
                    name=q.Name,
                    
                });
            await CrossMediaManager.Current.Stop();
            var param = new NavigationParameters();
            param.Add("PlayListModel", MusicPlayList);
            await NavigationService.NavigateAsync("BlurImagePage", param);
        }
        ObservableCollection<AudioModel> _songList;
        public ObservableCollection<AudioModel> songList
        {
            get => _songList;
            set => SetProperty(ref _songList,value, "songList");
        }
        public ICommand PlayBtnClickedCommand { get; private set; }
    }
}
