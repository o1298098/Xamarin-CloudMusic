using FFImageLoading;
using MediaManager;
using Newtonsoft.Json;
using CloudMusic.Actions.ApiHelper;
using CloudMusic.Models.Media;
using CloudMusic.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace CloudMusic.ViewModels
{
    public class PersonalFMPageViewModel : BaseViewModel
    {
        bool init, _isAutoAnimationRunning, _isUserInteractionRunning, _isShowLrc, _isShowDisc, _isMoreMenu;
        string imageurl, description, songname, artname, commentcount, _likeIcon,_playicon;
        int index, _currentIndex, offset, maxvulome, nowvulome;
        double nowdurationnum;
        TimeSpan nowduration, musicduration;
        PersonalFmModel _personalFm;
        MusicInfo _musicInfo;
        PersonalFmModel.Datum nowsongInfo;
        public PersonalFMPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            IsShowDisc = true;
            IsShowLrc = false;
            likeIcon = "PlayLike";
            NowVulome = CrossMediaManager.Current.VolumeManager.CurrentVolume;
            MaxVulome = CrossMediaManager.Current.VolumeManager.MaxVolume;
            NowDuration = TimeSpan.FromMilliseconds(200);
            MusicDuration = TimeSpan.FromMilliseconds(1000);
            GetPersonalFM();
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            SliderDragCompletedCommand = new Command(async () => { await CrossMediaManager.Current.SeekTo(TimeSpan.FromMilliseconds(NowDurationNum)); });
            DiscSwipeCommand = new Command(DiscSwipe);
            LikeSongClickCommand = new Command(LikeSongClicked);
            MoreBtnClickCommand = new Command(() => IsMoreMenu = !IsMoreMenu); 
            NextClickedCommand = new Command(NextClicked);
            PlayClickedCommand = new Command(PlayClicked);
            ArtistClickedCommand = new Command(async()=>await ArtistClicked());
            MusicCommentClickedCommand = new DelegateCommand(MusicCommentClicked);
            MVBtnClickedCommand = new DelegateCommand(MVBtnClicked);
        }
        void GetPersonalFM()
        {
            IsBusy = true;
            Task.Run(() => {
                var s = CloudMusicApiHelper.PersonalFM(offset);
                if (s != null)
                    if (s.code == 200)
                    {
                        if (personalFm == null)
                        {
                            personalFm = s;
                            ChangeMusic(0);
                        }
                        else
                        {
                            foreach (var q in s.data)
                                personalFm.data.Add(q);
                        }
                        offset += 3;
                    }
                IsBusy = false;
            });
        }
        private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            NowDuration = e.Position;
            NowDurationNum = e.Position.TotalMilliseconds;

        }
        void DiscSwipe(object e)
        {
            var s = e as PanCardView.EventArgs.ItemSwipedEventArgs;
            int newindex = s.Index;
                newindex++;
            if (newindex >= 0)
                ChangeMusic(newindex);
        }
        void ChangeMusic(int s)
        {
            ImageUrl = personalFm.data[s].album.picUrl;
            NowSongInfo = personalFm.data[s];

            likeIcon = personalFm.data[s].starred? "PlayHeart_red" : "PlayHeart";
            NowDuration = new TimeSpan(0);
            MusicDuration = TimeSpan.FromMilliseconds(personalFm.data[s].duration);
             Task.Run(async () =>
            {
                MusicInfo result = CloudMusicApiHelper.GetSong(personalFm.data[s].id.ToString(), Models.ENUM.CouldMusicBpsType.high);
                if (result != null)
                {
                    musicInfo = result;
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(result.data[0].url))
                            await CrossMediaManager.Current.Play(result.data[0].url);
                        else
                            Device.BeginInvokeOnMainThread(() => DependencyService.Get<IToast>().ShortAlert("没有版权"));
                        if(s+2>= personalFm.data.Count)
                            GetPersonalFM();

                    }
                    catch { }
                    var r = CloudMusicApiHelper.GetSongComment("50", personalFm.data[s].id.ToString(), 0);
                    if (result != null)
                    {
                        int cCount = r.total;
                        if (cCount < 1000)
                            CommentCount = cCount.ToString();
                        else if (cCount > 1000 & cCount < 10000)
                            CommentCount = "999+";
                        else if (cCount > 10000 & cCount < 100000)
                            CommentCount = "1w+";
                        else if (cCount > 100000)
                            CommentCount = "10w+";
                    }
                }
            });
        }
        async Task ArtistClicked()
        {
            var param = new NavigationParameters();
            if (NowSongInfo.artists.Count > 1)
            {
                var artists = NowSongInfo.artists.Select(v => v.name).ToArray();
                var result = await XF.Material.Forms.UI.Dialogs.MaterialDialog.Instance.SelectActionAsync(title: "选择歌手",
                                                              actions: artists,configuration:new MaterialSimpleDialogConfiguration {TitleTextColor=Color.Black });
                if (result!=-1)
                    param.Add("artistid", NowSongInfo.artists[result].id);
                else
                    return;
            }
            else
            {
                
                param.Add("artistid", NowSongInfo.artists[0].id);
               
            }
            await NavigationService.NavigateAsync("SingerPlayListPage", param);
        }
        void LikeSongClicked()
        {
            Task.Run(() => {
               var r= CloudMusicApiHelper.LikeSong(NowSongInfo.id.ToString(), !NowSongInfo.starred);
                if (r != "err")
                    likeIcon = !NowSongInfo.starred? "PlayHeart_red": "PlayHeart";
            });
        }
        async void PlayClicked()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                playicon = "ic_play";
                await CrossMediaManager.Current.Pause();
            }
            else
            {
                playicon = "ic_pause";
                await CrossMediaManager.Current.Play();
            }
        }
        void NextClicked()
        {
            if (CurrentIndex < personalFm.data.Count)
            {
                int x = CurrentIndex;
                CurrentIndex++;
                x++;
                ChangeMusic(x);
            }
        }
        async void MVBtnClicked()
        {
            IsMoreMenu = false;
            await CrossMediaManager.Current.Pause();
            var q = new NavigationParameters();
            q.Add("MVid", NowSongInfo.mvid);
            await NavigationService.NavigateAsync("MusicVideoPage", q);
        }
        async void MusicCommentClicked()
        {
            var param = new NavigationParameters();
            param.Add("MusicID", musicInfo.data[0].id);
            await NavigationService.NavigateAsync("MusicCommentPage", param);
        }

        public PersonalFmModel.Datum NowSongInfo
        {
            get => nowsongInfo;
            set => SetProperty(ref nowsongInfo, value, "NowSongInfo");
        }
        public PersonalFmModel personalFm
        {
            get => _personalFm;
            set => SetProperty(ref _personalFm, value, "personalFm");
        }
        public int NowVulome
        {
            get => nowvulome;
            set => SetProperty(ref nowvulome, value, "NowVulome");
        }
        public int MaxVulome
        {
            get => maxvulome;
            set => SetProperty(ref maxvulome, value, "MaxVulome");
        }
        public bool IsMoreMenu
        {
            get => _isMoreMenu;
            set => SetProperty(ref _isMoreMenu, value, "IsMoreMenu");
        }
        public bool IsShowDisc
        {
            get => _isShowDisc;
            set => SetProperty(ref _isShowDisc, value, "IsShowDisc");
        }
        public string ImageUrl
        {
            get { return imageurl; }
            set => SetProperty(ref imageurl, value, "ImageUrl");
        }
        public string playicon
        {
            get { return _playicon; }
            set => SetProperty(ref _playicon, value, "playicon");
        }
        public bool IsShowLrc
        {
            get => _isShowLrc;
            set => SetProperty(ref _isShowLrc, value, "IsShowLrc");
        }
        public bool IsAutoAnimationRunning
        {
            get => _isAutoAnimationRunning;
            set => SetProperty(ref _isAutoAnimationRunning, value, "IsAutoAnimationRunning");
        }
        public bool IsUserInteractionRunning
        {
            get => _isUserInteractionRunning;
            set => SetProperty(ref _isUserInteractionRunning, value, "IsUserInteractionRunning");
        }
        public string likeIcon
        {
            get => _likeIcon;
            set => SetProperty(ref _likeIcon, value, "likeIcon");
        }
        public string CommentCount
        {
            get => commentcount;
            set => SetProperty(ref commentcount, value, "CommentCount");
        }
        public int CurrentIndex
        {
            get => _currentIndex;
            set { SetProperty(ref _currentIndex, value, "CurrentIndex");  }
        }
        public TimeSpan MusicDuration
        {
            get => musicduration;
            set => SetProperty(ref musicduration, value, "MusicDuration");
        }
        public TimeSpan NowDuration
        {
            get => nowduration;
            set => SetProperty(ref nowduration, value, "NowDuration");
        }
        public double NowDurationNum
        {
            get => nowdurationnum;
            set => SetProperty(ref nowdurationnum, value, "NowDurationNum");
        }
        public MusicInfo musicInfo
        {
            get => _musicInfo;
            set => SetProperty(ref _musicInfo, value, "musicInfo");
        }
        public ICommand DiscSwipeCommand { get; private set; }
        public ICommand SliderDragCompletedCommand { get; private set; }
        public ICommand LikeSongClickCommand { get; private set; }
        public ICommand MoreBtnClickCommand { get; private set; }
        public ICommand NextClickedCommand { get; private set; }
        public ICommand PlayClickedCommand { get; private set; }
        public ICommand ArtistClickedCommand { get; private set; }
        public ICommand MusicCommentClickedCommand { get; private set; }
        public ICommand MVBtnClickedCommand { get; private set; }
    }
}
