using CloudMusic.Actions.ApiHelper;
using CloudMusic.Models;
using CloudMusic.Models.Media;
using CloudMusic.Services;
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
    public class UserInfoPageViewModel : BaseViewModel
    {
        public UserInfoPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            PhoneMusicClickedCommand = new DelegateCommand(PhoneMusicClicked);
            PlaylistUnitClickedCommand = new DelegateCommand<NavigationParameters>(PlaylistUnitClicked);
            GetUserPlayLists();
            GetLocalMusic();
            GetSubscriptionCount();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        void PhoneMusicClicked()
        {
            NavigationService.NavigateAsync("PhoneMusicMangerPage");
        }
        async void PlaylistUnitClicked(NavigationParameters param)
        {
            await NavigationService.NavigateAsync("MusicPlayListPage", param);
        }
        void GetUserPlayLists()
        {
            userPlayLists = new ObservableCollection<Playlist>();
            subscribedPlayLists = new ObservableCollection<Playlist>();
            Task.Run(() => {
                string id = Xamarin.Essentials.Preferences.Get("userid", 76074798).ToString();
                var r = CloudMusicApiHelper.UserPlayLists(id);
                if (r != null)
                    if (r.code == 200)
                    {
                        var userlist = r.playlist.Where(q => !q.subscribed);
                        var sublist= r.playlist.Where(q => q.subscribed);
                        foreach (var q in userlist)
                            userPlayLists.Add(q);
                        foreach (var q in sublist)
                            subscribedPlayLists.Add(q);
                    }
                        
            });
        }
        void GetLocalMusic()
        {
            Task.Run(async ()=> {
                var q =await DependencyService.Get<IAudioPicker>().GetAudioFileAsync();
                localMusicCount = q.Count();
            });
           
        }
        void GetSubscriptionCount()
        {
            Task.Run(() => {
                var r = CloudMusicApiHelper.SubscriptionCount();
                if (r != null)
                    if (r.code == 200)
                    {
                        subscriptionCount = r;
                    }

            });
        }
        ObservableCollection<Playlist> _userPlayLists;
        ObservableCollection<Playlist> _subscribedPlayLists;
        SubscriptionCount _subscriptionCount;
        int _localMusicCount;
        public int localMusicCount
        {
            get => _localMusicCount;
            set => SetProperty(ref _localMusicCount, value, "localMusicCount");
        }
        public SubscriptionCount subscriptionCount
        {
            get => _subscriptionCount;
            set => SetProperty(ref _subscriptionCount, value, "subscriptionCount");
        }
        public ObservableCollection<Playlist> userPlayLists
        {
            get => _userPlayLists;
            set => SetProperty(ref _userPlayLists, value, "userPlayLists");
        }
        public ObservableCollection<Playlist> subscribedPlayLists
        {
            get => _subscribedPlayLists;
            set => SetProperty(ref _subscribedPlayLists, value, "subscribedPlayLists");
        }
        public ICommand PhoneMusicClickedCommand { get; private set; }
        public ICommand PlaylistUnitClickedCommand { get; private set; }
    }
}
