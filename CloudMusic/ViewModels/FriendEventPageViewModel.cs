using CloudMusic.Actions.ApiHelper;
using CloudMusic.Models.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CloudMusic.ViewModels
{
    public class FriendEventPageViewModel :BaseViewModel
    {
        int offset;
        bool init;
        MusicEventModel _friendEvents;
        bool _isallload,_isRefresh;
        public FriendEventPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ListViewItemAppearingCommand = new Command<object>(ListViewItemAppearing);
            ListViewRefreahCommand = new Command(GetEventData);
            GetEventData();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        void GetEventData()
        {
            offset = 0;
            IsRefresh = true;
            Task.Run(() => {
                var r = CloudMusicApiHelper.GetFriendEvents(offset);
                if (r != null)
                    if (r.code == 200)
                        friendEvents = r;
                IsRefresh = false;
            });
        }
        void ListViewLoadMore()
        {
            offset += 50;
            IsBusy = true;
            Task.Run(() => {
                var r = CloudMusicApiHelper.GetFriendEvents(offset);
            if (r != null)
                if (r.code == 200)
                    {
                        foreach (var q in r.friendevent)
                        {
                            friendEvents.friendevent.Add(q);
                        }
                        IsBusy = false;
                    };
            });
        }
        void ListViewItemAppearing(object e)
        {
            var s = e as ItemVisibilityEventArgs;
            if (s.ItemIndex + 1 == friendEvents.friendevent.Count())
            {

                if (!friendEvents.more)
                    IsAllLoad = true;
                else
                    ListViewLoadMore();
            }
        }

        public bool IsRefresh
        {
            get => _isRefresh;
            set => SetProperty(ref _isRefresh, value, "IsRefresh");
        }
        public bool IsAllLoad
        {
            get => _isallload;
            set => SetProperty(ref _isallload, value, "IsAllLoad");
        }
        public MusicEventModel friendEvents
        {
            get => _friendEvents;
            set => SetProperty(ref _friendEvents, value, "friendEvents");
        }


        public ICommand ListViewItemAppearingCommand { get; private set; }
        public ICommand ListViewRefreahCommand { get; private set; }

    }
}
