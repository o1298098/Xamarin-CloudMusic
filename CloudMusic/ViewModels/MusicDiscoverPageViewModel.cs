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
    public class MusicDiscoverPageViewModel : BaseViewModel
    {
        MainBanner _mainBanner;
        Personalized _personalized;
        NewAlbums _newAlbums;
        TopNewSongs _topNewSongs; Color _albumTextColor, _songTextColor;
        int _albumFontSize, _songFontSize;
        FontAttributes _albumAttributes, _songAttributes;
        string _moreButtonName;
        bool _isShowAlbums, _isShowSongs, init;
        public MusicDiscoverPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ChangeButtonClickedCommand = new Command<string>(ChangeButtonClicked);
            PlaylistUnitClickedCommand = new DelegateCommand<NavigationParameters>(PlaylistUnitClicked);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (!init)
            {
                Getbanners();
                GetPersonalized();
                GetNewestAlbum();
                GetNewestSongs();
                InitUi();
                init = true;
            }
           
        }
        #region Mod
        async void PlaylistUnitClicked(NavigationParameters param)
        {            
            await NavigationService.NavigateAsync("MusicPlayListPage", param);
        }
        void Getbanners()
        {
            Task.Run(() => { 
            var r = CloudMusicApiHelper.GetBanners();
            if(r!=null)
                if(r.code==200)
                   mainBanner = r;
            });
        }
        void GetPersonalized()
        {
            Task.Run(() => {
                var r = CloudMusicApiHelper.GetPersonalized(6);
                if (r != null)
                    if (r.code == 200)
                        personalized = r;
            });
        }
        void GetNewestAlbum()
        {
            Task.Run(() => {
                var r = CloudMusicApiHelper.GetNewAlbums(3,0);
                if (r != null)
                    if (r.code == 200)
                        newAlbums = r;
            });
        }
        void GetNewestSongs()
        {
            Task.Run(() => {
                var r = CloudMusicApiHelper.GetNewSongs();
                if (r != null)
                    if (r.code == 200)
                    {
                        r.result.RemoveRange(3, r.result.Count - 3);
                        topNewSongs = r;
                    }
            });
        }
        void ChangeButtonClicked(string mode)
        {
            if (mode== "song")
            {
                isShowAlbums = false;
                isShowSongs = true;
                songFontSize = 18;
                albumFontSize = 14;
                songTextColor = Color.FromHex("#333333");
                albumTextColor = Color.FromHex("#515151");
                MoreButtonName = "新歌推荐";
                albumAttributes = FontAttributes.None;
                songAttributes = FontAttributes.Bold;
            }
            else
            {
                isShowAlbums = true;
                isShowSongs = false;
                songFontSize = 14;
                albumFontSize = 18;
                songTextColor = Color.FromHex("#515151");
                albumTextColor = Color.FromHex("#333333"); 
                MoreButtonName = "更多新碟";
                albumAttributes = FontAttributes.Bold;
                songAttributes = FontAttributes.None;
            }
        }
        void InitUi()
        {
            isShowAlbums = true;
            isShowSongs = false;
            songFontSize = 14;
            albumFontSize = 18;
            songTextColor = Color.FromHex("#515151");
            albumTextColor = Color.FromHex("#333333");
            MoreButtonName = "更多新碟";
            albumAttributes = FontAttributes.Bold;
            songAttributes = FontAttributes.None;
        }
        #endregion
        #region bindingable
        public int songFontSize
        {
            get => _songFontSize;
            set => SetProperty(ref _songFontSize, value, "songFontSize");
        }
        public int albumFontSize
        {
            get => _albumFontSize;
            set => SetProperty(ref _albumFontSize, value, "albumFontSize");
        }
        public FontAttributes albumAttributes
        {
            get => _albumAttributes;
            set => SetProperty(ref _albumAttributes, value, "albumAttributes");
        }
        public FontAttributes songAttributes
        {
            get => _songAttributes;
            set => SetProperty(ref _songAttributes, value, "songAttributes");
        }
        public Color albumTextColor
        {
            get => _albumTextColor;
            set => SetProperty(ref _albumTextColor, value, "albumTextColor");
        }
        public Color songTextColor
        {
            get => _songTextColor;
            set => SetProperty(ref _songTextColor, value, "songTextColor");
        }
        public string MoreButtonName
        {
            get => _moreButtonName;
            set => SetProperty(ref _moreButtonName, value, "MoreButtonName");
        }
        public bool isShowAlbums
        {
            get => _isShowAlbums;
            set => SetProperty(ref _isShowAlbums, value, "isShowAlbums");
        }
        public bool isShowSongs
        {
            get => _isShowSongs;
            set => SetProperty(ref _isShowSongs, value, "isShowSongs");
        }
        public MainBanner mainBanner
        {
            get => _mainBanner;
            set => SetProperty(ref _mainBanner,value, "mainBanner");
        }
        public Personalized personalized
        {
            get => _personalized;
            set => SetProperty(ref _personalized, value, "personalized");
        }
        public NewAlbums newAlbums
        {
            get => _newAlbums;
            set => SetProperty(ref _newAlbums, value, "newAlbums");
        }
        public TopNewSongs topNewSongs
        {
            get => _topNewSongs;
            set => SetProperty(ref _topNewSongs, value, "topNewSongs");
        }
        #endregion

        public ICommand ChangeButtonClickedCommand { get; private set; }
        public ICommand PlaylistUnitClickedCommand { get; private set; }

    }
}
