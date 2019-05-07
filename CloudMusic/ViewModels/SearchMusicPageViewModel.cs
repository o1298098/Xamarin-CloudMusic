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
using CloudMusic.Models.ENUM;

namespace CloudMusic.ViewModels
{
    public class SearchMusicPageViewModel : BaseViewModel
    {
        string _keyWords;
        MusicSearchModel _searchresult;
        EventHandler KeyWordsChanged;
        bool _isallload;
        CloudMusicSearchType currentType;
        MusicSearchAllModel _searchAllResult;
        int offset;

        public SearchMusicPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            currentType =CloudMusicSearchType.SingleSong;
            KeyWordsChanged += GetSuggest;
            AutoCompleteSelectionChangedCommand = new Command<object>(AutoCompleteSelectionChanged);
            AutoCompletedCommand = new Command(AutoCompleted); ;
            ListViewItemTappedCommand = new Command<object>(ListViewItemTappedAsync);
            ListViewItemAppearingCommand = new Command<object>(ListViewItemAppearing);
            TabViewSelectionChangedCommand = new Command<object>(TabViewSelectionChanged);
            PlayListItemTappedCommand = new Command<object>(PlayListItemTapped);
            ArtistsListViewItemTappedCommand = new Command<object>(ArtistsListViewItemTappedAsync);
        }
        async void PlayListItemTapped(object e)
        {
            var s = e as ItemTappedEventArgs;
            var playlist = s.Item as Playlist;
            var param = new NavigationParameters();
            param.Add("PlayListid", playlist.id);
            param.Add("PlayListpic", playlist.coverImgUrl);
            await NavigationService.NavigateAsync("MusicPlayListPage", param);
        }
        void AutoCompleted()
        {
             if (currentType == CloudMusicSearchType.All)
                    SearchAll();
                else
                    Search();
        }
        void TabViewSelectionChanged(object e)
        {
            var s =e as Syncfusion.XForms.TabView.SelectionChangedEventArgs;
            IsAllLoad = false;
            SearchResult = null;
            SearchAllResult = null;
            switch (s.Name)
            {
                case "单曲":
                    currentType = CloudMusicSearchType.SingleSong;
                    break;
                case "综合":
                    currentType = CloudMusicSearchType.All;
                    break;
                case "视频":
                    currentType = CloudMusicSearchType.Video;
                    break;
                case "歌手":
                    currentType = CloudMusicSearchType.Singer;
                    break;
                case "专辑":
                    currentType = CloudMusicSearchType.Album;
                    break;
                case "歌单":
                    currentType = CloudMusicSearchType.PlayList;
                    break;
                case "主播电台":
                    currentType =CloudMusicSearchType.FM;
                    break;
                case "用户":
                    currentType = CloudMusicSearchType.User;
                    break;
                case "MV":
                    currentType = CloudMusicSearchType.MV;
                    break;

            }
            if (!string.IsNullOrWhiteSpace(KeyWords))
                if (currentType == CloudMusicSearchType.All)
                    SearchAll();
                else
                    Search();

        }
        async void ListViewItemTappedAsync(object e)
        {
            var s = e as ItemTappedEventArgs;
            Mv mv = s.Item as Mv;
            var param =new NavigationParameters();
            param.Add("MVid", mv.id);
            await MediaManager.CrossMediaManager.Current.Pause();
            await NavigationService.NavigateAsync("MusicVideoPage", param);
        }
        async void ArtistsListViewItemTappedAsync(object e)
        {
            var s = e as ItemTappedEventArgs;
            Artist ar = s.Item as Artist;
            var param = new NavigationParameters();
            param.Add("artistid", ar.id);
            await NavigationService.NavigateAsync("SingerPlayListPage", param);
        }
        void GetSuggest()
        {
            Task.Run(() =>
            {
                var suggest = CloudMusicApiHelper.MobileSearchSuggest(KeyWords);
                if (suggest != null)
                    if (suggest.code == 200)
                        MobileSearchSuggest = suggest;
            });
        }
        void Search()
        {
            IsBusy = true;
            Task.Run(() =>
            {
                offset = 0;
                var info = CloudMusicApiHelper.Search(KeyWords, 30, offset, currentType);
                if (info != null)
                    if (info.code == 200)
                    {
                        if (SearchResult == null)
                            SearchResult = info;
                        offset +=30;
                        IsBusy = false;
                    }
            });
        }
        void SearchAll()
        {
            IsBusy = true;
            Task.Run(() =>
            {
                offset = 0;
                var info = CloudMusicApiHelper.SearchAll(KeyWords, 30, offset);
                if (info != null)
                    if (info.code == 200)
                    {
                        if (SearchResult == null)
                            SearchAllResult = info;
                        offset += 30;
                        IsBusy = false;
                    }
            });
        }
        void ListViewItemAppearing(object e)
        {
            var s = e as ItemVisibilityEventArgs;
            int total = 0;
            int loadedcount = 0;
            switch (currentType)
            {
                case CloudMusicSearchType.SingleSong:
                    total = SearchResult.result.songCount;
                    loadedcount = SearchResult.result.songs.Count();
                    break;
                case CloudMusicSearchType.Video:
                    total = SearchResult.result.videoCount;
                    loadedcount = SearchResult.result.videos.Count();
                    break;
                case CloudMusicSearchType.Singer:
                    total = SearchResult.result.artistCount;
                    loadedcount = SearchResult.result.artists.Count();
                    break;
                case CloudMusicSearchType.Album:
                    total = SearchResult.result.albumCount;
                    loadedcount = SearchResult.result.albums.Count();
                    break;
                case CloudMusicSearchType.PlayList:
                    total = SearchResult.result.playlistCount;
                    loadedcount = SearchResult.result.playlists.Count();
                    break;
                case CloudMusicSearchType.FM:
                    total = SearchResult.result.djRadiosCount;
                    loadedcount = SearchResult.result.djRadios.Count();
                    break;
                case CloudMusicSearchType.User:
                    total = SearchResult.result.userprofileCount;
                    loadedcount = SearchResult.result.userprofiles.Count();
                    break;
                case CloudMusicSearchType.MV:
                    total = SearchResult.result.mvCount;
                    loadedcount = SearchResult.result.mvs.Count();
                    break;
            }
            if (s.ItemIndex + 1 == loadedcount)
            {

                if (s.ItemIndex + 1 == total)
                    IsAllLoad = true;
                else
                    ListViewLoadMore();
            }
        }
        void ListViewLoadMore()
        {
            IsBusy = true;
            Task.Run(() => {
                var info = CloudMusicApiHelper.Search(KeyWords, 30, offset, currentType);
                if (info != null)
                    if (info.code == 200)
                    {
                        switch (currentType)
                        {
                            case CloudMusicSearchType.SingleSong:
                                foreach (var q in info.result.songs)
                                    SearchResult.result.songs.Add(q);
                                break;
                            case CloudMusicSearchType.Video:
                                foreach (var q in info.result.videos)
                                    SearchResult.result.videos.Add(q);
                                break;
                            case CloudMusicSearchType.Singer:
                                foreach (var q in info.result.artists)
                                    SearchResult.result.artists.Add(q);
                                break;
                            case CloudMusicSearchType.Album:
                                foreach (var q in info.result.albums)
                                    SearchResult.result.albums.Add(q);
                                break;
                            case CloudMusicSearchType.PlayList:
                                foreach (var q in info.result.playlists)
                                    SearchResult.result.playlists.Add(q);
                                break;
                            case CloudMusicSearchType.FM:
                                foreach (var q in info.result.djRadios)
                                    SearchResult.result.djRadios.Add(q);
                                break;
                            case CloudMusicSearchType.User:
                                foreach (var q in info.result.userprofiles)
                                    SearchResult.result.userprofiles.Add(q);
                                break;
                            case CloudMusicSearchType.MV:
                                foreach (var q in info.result.mvs)
                                    SearchResult.result.mvs.Add(q);
                                break;
                        }
                       
                        offset += 30;
                    }
                IsBusy = false;
        });
        }
        void AutoCompleteSelectionChanged(object e)
        {
            Task.Run(() =>
            {
                Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs items = e as Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs;
                var i = items.AddedItems as MobileSearchSuggestModel.Allmatch;
                var info = CloudMusicApiHelper.Search(i.keyword, 30, 0, currentType);
                if (info != null)
                    if (info.code == 200)
                        SearchResult = info;

            });
         }
        MobileSearchSuggestModel _mobileSearchSuggest;

        public MusicSearchAllModel SearchAllResult
        {
            get => _searchAllResult;
            set => SetProperty(ref _searchAllResult, value, "SearchAllResult");
        }
        public MobileSearchSuggestModel MobileSearchSuggest
        {
            get => _mobileSearchSuggest;
            set => SetProperty(ref _mobileSearchSuggest, value, "MobileSearchSuggest");
        }
        public MusicSearchModel SearchResult
        {
            get => _searchresult;
            set => SetProperty(ref _searchresult, value, "SearchResult");
        }
        public string KeyWords
        {
            get => _keyWords;
            set
            {
                SetProperty(ref _keyWords, value, "KeyWords");
                KeyWordsChanged?.Invoke();
            }
        }
        public bool IsAllLoad
        {
            get => _isallload;
            set => SetProperty(ref _isallload, value, "IsAllLoad");
        }
        public ICommand AutoCompleteSelectionChangedCommand { get;private set; }
        public ICommand AutoCompletedCommand { get; private set; }
        public ICommand ListViewItemTappedCommand { get; private set; }
        public ICommand ArtistsListViewItemTappedCommand { get; private set; }
        public ICommand ListViewItemAppearingCommand { get; private set; }
        public ICommand TabViewSelectionChangedCommand { get; private set; }
        public ICommand PlayListItemTappedCommand { get; private set; }
    }
}
