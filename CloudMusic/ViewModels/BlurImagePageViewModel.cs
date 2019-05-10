using MediaManager;
using Newtonsoft.Json;
using CloudMusic.Models;
using CloudMusic.Models.Media;
using CloudMusic.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CloudMusic.ViewModels
{
    public class BlurImagePageViewModel : BaseViewModel
    {

        Random random;
        public event ColorChangedHandler OnColorChange;
        public event EventHandler OnMusicChange;
        public event LrcEventHandler OnLrcChange;
        private ObservableCollection<Item> discImage;
        MusicPlayListDetail musicPlayList;
        double nowdurationnum;
        int maxvulome, nowvulome;
        MusicInfo _musicInfo;
        //static string url = "https://api.unsplash.com/photos/random?client_id=cb802c915f1be81dad83df10d3eac7f2d7dd56355f068a452b3410151f1d45b1";
        string playlisturl = "https://music.aityp.com/playlist/detail?id=543014929";
        string musicurl = "https://music.aityp.com/song/url?&br=999000&id=";
        string musicCommenturl = "https://music.aityp.com/comment/music?limit=1&id=";
        ThemeColors theme;
        string imageurl, description, songname, artname, commentcount;
        int index, _currentIndex;
        TimeSpan nowduration, musicduration;
        Track nowsongInfo;
        ObservableCollection<SongLyricsModel> _songLyricsModels;
        float _opacity;
        bool init, _isAutoAnimationRunning, _isUserInteractionRunning, _isShowLrc, _isShowDisc, _isMoreMenu;
        string[] urls =
        {
            "http://hbimg.b0.upaiyun.com/a8cc159f81e65d0a1e89603901c9e564793f803418540-WrQ7fN",
            "https://is2-ssl.mzstatic.com/image/thumb/Music122/v4/36/df/34/36df34e4-000e-4414-2511-ec4d207fb149/5052917024599.jpg/1200x630bb.jpg",
            "http://a0.att.hudong.com/28/25/20200000013920144721251845631_s.jpg",
            "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRUcQgLZenU2F4n8zcheXDgzR-93J0nZx3MFvTpj03cCZLrGino",
            "https://wx2.sinaimg.cn/mw690/007cj4a0gy1fwu1bvforvj30mt0mkgng.jpg",
            "http://www.people.com.cn/mediafile/pic/20190114/99/4777642898675812815.jpg",
            "http://img1.oss.ifensi.com/2018/0518/20180518034617796.png",
            "http://pic.sc.chinaz.com/files/pic/pic9/201801/bpic5121.jpg",
            "https://ccm.ddcdn.com/discovery/production/1517213656_RackMultipart20180129-1-17ly3f8.jpg",
            "http://upload.art.ifeng.com/2016/1212/1481521585293.jpg",
            "http://www.deskier.com/uploads/allimg/161002/1-161002131914.jpg"
        };
        public BlurImagePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            CrossMediaManager.Current.Init();
            MusicPlayList = new MusicPlayListDetail();
            ImageUrl = urls[0];
            Description = "life is fantasy";
            IsShowDisc = true;
            IsShowLrc = false;
            SongLyricsModels = new ObservableCollection<SongLyricsModel>();
            ImageSource im = ImageSource.FromUri(new Uri(ImageUrl));
            DiscImage = new ObservableCollection<Item>();
            foreach (var q in urls)
            {
                DiscImage.Add(new Item { Text = q });
            }
            DiscSwipeCommand = new Command(DiscSwipe);
            MusicDuration = TimeSpan.FromMilliseconds(1000);
            NowVulome = CrossMediaManager.Current.VolumeManager.CurrentVolume;
            MaxVulome = CrossMediaManager.Current.VolumeManager.MaxVolume;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.MediaItemFinished += Current_MediaItemFinished;
            CrossMediaManager.Current.MediaItemFailed += Current_MediaItemFailed;
            CrossMediaManager.Current.VolumeManager.VolumeChanged += VolumeManager_VolumeChanged;
            NowDuration = TimeSpan.FromMilliseconds(200);
            BackClickedCommand = new Command(async () => await BackClickedAsync());
            NextClickedCommand = new Command(async () => await NextClickedAsync());
            PlayClickedCommand = new Command(PlayClickedAsync);
            ChangeViewCommand = new Command(ChangeView);
            MoreBtnClickCommand = new Command(() => IsMoreMenu = !IsMoreMenu);
            ShareBtnClickedCommand = new Command(async () => { await NavigationService.NavigateAsync("SearchMusicPage"); });
            MusicCommentClickedCommand = new DelegateCommand(async () =>
            {
                var param = new NavigationParameters();
                param.Add("MusicID", musicInfo.data[0].id);
                await NavigationService.NavigateAsync("MusicCommentPage", param);
            });
            SliderDragCompletedCommand = new Command(async () => { await CrossMediaManager.Current.SeekTo(TimeSpan.FromMilliseconds(NowDurationNum)); });
            VulomeDragCompletedCommand = new Command(() => { CrossMediaManager.Current.VolumeManager.CurrentVolume = NowVulome; });
            MVBtnClickedCommand = new DelegateCommand(async () =>
            {
                IsMoreMenu = false;
                await CrossMediaManager.Current.Pause();
                var q = new NavigationParameters();
                q.Add("MVid", NowSongInfo.mv);
                await NavigationService.NavigateAsync("MusicVideoPage", q);
            });
            TreeBtnClickedCommand = new DelegateCommand(async () => { await NavigationService.NavigateAsync("ScrollVideoPage"); });
            DownLoadBtnClickedCommand = new Command(SongDownLoad);
            ShareTappedCommand = new Command(async () => await ShareTapped());
            TestCommand = new DelegateCommand(async () =>
            {
                var param = new NavigationParameters();
                param.Add("pic", NowSongInfo.al.picUrl);
                await NavigationService.NavigateAsync("BlankPage", param);
            });

        }

        #region Mod
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters["PlayListModel"] != null)
            {
                MusicPlayList = parameters["PlayListModel"] as MusicPlayListDetail;
                await ChangeMusicAsync(0);
            }
            else if (!init)
            {
                await Task.Run(async () => await GetPlayListAsync());
            }
            init = true;
        }
        async Task ShareTapped()
        {
            //await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
            //{
            //    Uri = string.Format("https://music.163.com/song/media/outer/url?id={0}.mp3",NowSongInfo.id),
            //    Title = "共享歌曲"
            //});
            string downloadurl = string.Format("https://music.163.com/song/media/outer/url?id={0}.mp3", NowSongInfo.id);
            var fn = NowSongInfo.name + "." + musicInfo.data[0].encodeType;
            var file = System.IO.Path.Combine(Xamarin.Essentials.FileSystem.CacheDirectory, fn);
            using (await XF.Material.Forms.UI.Dialogs.MaterialDialog.Instance.LoadingDialogAsync(message: "生成音乐文件中......", new XF.Material.Forms.UI.Dialogs.Configurations.MaterialLoadingDialogConfiguration { TintColor = Color.FromHex("#94BBFF") }))
            {
                await Task.Run(async () =>
                {
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    byte[] data = await webClient.DownloadDataTaskAsync(downloadurl);
                    System.IO.File.WriteAllBytes(file, data);
                    await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareFileRequest
                    {
                        Title = Title,
                        File = new Xamarin.Essentials.ShareFile(file,"*/*")
                    });
                });

            };

        }
        void SongDownLoad()
        {
            if (!string.IsNullOrWhiteSpace(musicInfo.data[0].url))
            {
                var downloadManager = Plugin.DownloadManager.CrossDownloadManager.Current;
                Dictionary<string, string> filedic = new Dictionary<string, string>();
                filedic["name"] = NowSongInfo.name;
                filedic["type"] = musicInfo.data[0].encodeType;
                var file = downloadManager.CreateDownloadFile(musicInfo.data[0].url, filedic);
                downloadManager.Start(file);
                DependencyService.Get<IToast>().ShortAlert("下载中");
            }
            else
                DependencyService.Get<IToast>().ShortAlert("没有下载地址");

        }

        void ChangeView()
        {
            if (IsShowDisc)
            {
                IsShowDisc = false;
                IsShowLrc = true;
                GetLyrics();
            }
            else
            {
                IsShowDisc = true;
                IsShowLrc = false;
            }
        }
        private void Current_MediaItemFailed(object sender, MediaManager.Media.MediaItemFailedEventArgs e)
        {
            // DependencyService.Get<IToast>().LongAlert(e.Message);
            //NextClicked();
        }

        private async void Current_MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            await NextClickedAsync();
        }

        private void VolumeManager_VolumeChanged(object sender, MediaManager.Volume.VolumeChangedEventArgs e)
        {
            NowVulome = e.NewVolume;
        }
        async Task ChangeMusicAsync(int s)
        {
            ImageUrl = MusicPlayList.playlist.tracks[s].al.picUrl;
            ImageSource im = ImageSource.FromUri(new Uri(ImageUrl));
            NowDuration = new TimeSpan(0);
            MusicDuration = TimeSpan.FromMilliseconds(MusicPlayList.playlist.tracks[s].dt);
            NowSongInfo = MusicPlayList.playlist.tracks[s];
            await Task.Run(async () =>
             {
                 string result = ApiHelper.HttpClient.HttpGet(musicurl + NowSongInfo.id);
                 if (result != "err")
                 {
                     var music = JsonConvert.DeserializeObject<MusicInfo>(result);
                     Device.BeginInvokeOnMainThread(() =>
                     {
                         SongName = NowSongInfo.name;
                         ArtName = NowSongInfo.arstr;
                         musicInfo = music;
                     });
                     try
                     {
                        //var item = new MediaManager.Media.MediaItem(music.data[0].url)
                        //{
                        //    AlbumArtUri = NowSongInfo.al.picUrl,
                        //    Artist = NowSongInfo.arstr,
                        //    DisplayIconUri = NowSongInfo.al.picUrl,
                        //    Album= NowSongInfo.al.name,
                        //    DisplaySubtitle= NowSongInfo.name,
                        //};
                        if (!string.IsNullOrWhiteSpace(music.data[0].url))
                             await CrossMediaManager.Current.Play(music.data[0].url);
                         else
                             Device.BeginInvokeOnMainThread(() => DependencyService.Get<IToast>().ShortAlert("没有版权"));

                     }
                     catch { }
                     result = ApiHelper.HttpClient.HttpGet(musicCommenturl + MusicPlayList.playlist.tracks[s].id);
                     if (result != "err")
                     {
                         var musicComments = JsonConvert.DeserializeObject<MusicComment>(result);
                         int cCount = musicComments.total;
                         if (cCount < 1000)
                             CommentCount = cCount.ToString();
                         else if (cCount > 1000 & cCount < 10000)
                             CommentCount = "999+";
                         else if (cCount > 10000 & cCount < 100000)
                             CommentCount = "1w+";
                         else if (cCount > 100000)
                             CommentCount = "10w+";
                     }
                     if (IsShowLrc)
                         GetLyrics();
                 }
             });
            //var s = await DependencyService.Get<IPalette>().GetColorAsync(im);
            //Device.BeginInvokeOnMainThread(() => Theme = s);
        }
        void GetLyrics()
        {
            string result = ApiHelper.HttpClient.HttpGet("https://music.aityp.com/lyric?id=" + NowSongInfo.id);
            if (result != "err")
            {
                var lyric = JsonConvert.DeserializeObject<MusicLyric>(result);
                LoadLyrics(lyric);
            }
        }
        void LoadLyrics(MusicLyric lyrics)
        {
            Task.Run(() =>
            {
                ObservableCollection<SongLyricsModel> newLyrics = new ObservableCollection<SongLyricsModel>();
                string[] lyr = lyrics.lrc.lyric.Split('\n');
                foreach (string str in lyr)
                {
                    if (str.Length > 0 && str.IndexOf(":") != -1)
                    {
                        TimeSpan time = GetLyricsTime(str);
                        string lrc = str.Split(']')[1];
                        var lrmodel = new SongLyricsModel()
                        {
                            LyricsText = lrc,
                            Time = time.TotalMilliseconds

                        };
                        newLyrics.Add(lrmodel);
                    }
                }
                if (lyrics.tlyric.lyric != null)
                {
                    string[] tlylyr = lyrics.tlyric.lyric.Split('\n');
                    foreach (string str in tlylyr)
                    {
                        if (str.Length > 0 && str.IndexOf(":") != -1)
                        {
                            TimeSpan tlytime = GetLyricsTime(str);
                            string lrc = str.Split(']')[1];
                            var g = newLyrics?.Where(q => q.Time == tlytime.TotalMilliseconds).FirstOrDefault();
                            if (g != null)
                                g.cnText = lrc;
                        }
                    }
                }

                Device.BeginInvokeOnMainThread(() => SongLyricsModels = newLyrics);
            });
        }
        public TimeSpan GetLyricsTime(string str)
        {
            Regex reg = new Regex(@"\[(?<time>.*)\]", RegexOptions.IgnoreCase);
            int m = 0, s = 0, f = 0;
            try
            {
                string timestr = reg.Match(str).Groups["time"].Value;
                m = Convert.ToInt32(timestr.Split(':')[0]);
                if (timestr.Split(':')[1].IndexOf(".") != -1)
                {
                    s = Convert.ToInt32(timestr.Split(':')[1].Split('.')[0]);
                    f = Convert.ToInt32(timestr.Split(':')[1].Split('.')[1]);
                }
                else
                {
                    s = Convert.ToInt32(timestr.Split(':')[1]);
                }
            }
            catch
            {
                return new TimeSpan(0, 0, m, s, f);
            }
            return new TimeSpan(0, 0, m, s, f);


        }
        SongLyricsModel now = new SongLyricsModel();
        private async void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            NowDuration = e.Position;
            NowDurationNum = e.Position.TotalMilliseconds;
            if (IsShowLrc)
            {
                var g = SongLyricsModels?.Where(q => q.Time <= e.Position.TotalMilliseconds).LastOrDefault();
                if (now != g && g != null)
                {
                    g.Color = Xamarin.Forms.Color.White;
                    now.Color = Xamarin.Forms.Color.FromHex("#c1c1c1");
                    now = g;
                    OnLrcChange?.Invoke(g);
                }
            }
            if (NowDuration >= MusicDuration && init)
                await NextClickedAsync();

        }
        async void PlayClickedAsync()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.Pause();
            }
            else
            {
                await CrossMediaManager.Current.Play();
            }
        }
        async Task BackClickedAsync()
        {
            if (CurrentIndex > 0)
            {
                int x = CurrentIndex;
                CurrentIndex--;
                x--;
                await ChangeMusicAsync(x);
            }
        }
        async Task NextClickedAsync()
        {
            if (CurrentIndex < MusicPlayList.playlist.trackCount)
            {
                //int x = CurrentIndex;
                //CurrentIndex++;
                //x++;
                if (random == null)
                    random = new Random(DateTime.Now.Millisecond);
                int x = random.Next(MusicPlayList.playlist.trackCount - 1);
                CurrentIndex = x;
                await ChangeMusicAsync(x);
            }
        }
        //public override async void OnNavigatedFrom(INavigationParameters parameters)
        //{
        //    base.OnNavigatedFrom(parameters);
        //    CrossMediaManager.Current.PositionChanged -= Current_PositionChanged;
        //    await CrossMediaManager.Current.Stop();
        //}
        async Task GetPlayListAsync()
        {
            using (await XF.Material.Forms.UI.Dialogs.MaterialDialog.Instance.LoadingDialogAsync(message: "加载中", new XF.Material.Forms.UI.Dialogs.Configurations.MaterialLoadingDialogConfiguration { TintColor = Color.FromHex("#94BBFF"), ScrimColor = Color.FromHex("#8791E8"), }))
            {
                await Task.Run(async () =>
             {
                 string result = ApiHelper.HttpClient.HttpGet(playlisturl);
                 if (result != "err")
                 {
                     var source = JsonConvert.DeserializeObject<MusicPlayListDetail>(result);
                     MusicPlayList = source;
                     await ChangeMusicAsync(0);
                 }
             });
            }
        }
        async void DiscSwipe(object e)
        {
            var s = e as PanCardView.EventArgs.ItemSwipedEventArgs;
            int newindex = s.Index;
            if (s.Direction == PanCardView.Enums.ItemSwipeDirection.Left)
                newindex++;
            else if (s.Direction == PanCardView.Enums.ItemSwipeDirection.Right)
                newindex--;
            if (newindex >= 0)
                await ChangeMusicAsync(newindex);
        }
        #endregion
        #region Bindingable
        public Track NowSongInfo
        {
            get => nowsongInfo;
            set => SetProperty(ref nowsongInfo, value, "NowSongInfo");
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
        public bool IsShowLrc
        {
            get => _isShowLrc;
            set => SetProperty(ref _isShowLrc, value, "IsShowLrc");
        }
        public ObservableCollection<SongLyricsModel> SongLyricsModels
        {
            get => _songLyricsModels;
            set => SetProperty(ref _songLyricsModels, value, "SongLyricsModels");
        }
        public string CommentCount
        {
            get => commentcount;
            set => SetProperty(ref commentcount, value, "CommentCount");
        }
        public string SongName
        {
            get => songname;
            set => SetProperty(ref songname, value, "SongName");
        }
        public string ArtName
        {
            get => artname;
            set => SetProperty(ref artname, value, "ArtName");
        }
        public float Opacity
        {
            get => _opacity;
            set => SetProperty(ref _opacity, value, "Opacity");
        }
        public string ImageUrl
        {
            get { return imageurl; }
            set => SetProperty(ref imageurl, value, "ImageUrl");
        }
        public string Description
        {
            get { return description; }
            set => SetProperty(ref description, value, "Description");
        }
        public ThemeColors Theme
        {
            get { return theme; }
            set
            {
                SetProperty(ref theme, value, "Theme");
                OnColorChange();
            }
        }
        public ObservableCollection<Item> DiscImage
        {
            get => discImage;
            set => SetProperty(ref discImage, value, "DiscImage");
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
        public int CurrentIndex
        {
            get => _currentIndex;
            set { SetProperty(ref _currentIndex, value, "CurrentIndex"); OnMusicChange?.Invoke(); }
        }
        public MusicPlayListDetail MusicPlayList
        {
            get => musicPlayList;
            set => SetProperty(ref musicPlayList, value, "MusicPlayList");
        }
        public MusicInfo musicInfo
        {
            get => _musicInfo;
            set => SetProperty(ref _musicInfo, value, "musicInfo");
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
        #endregion
        #region Command
        public ICommand ChangeViewCommand { get; private set; }
        public ICommand DiscSwipeCommand { get; private set; }
        public ICommand NextClickedCommand { get; private set; }
        public ICommand BackClickedCommand { get; private set; }
        public ICommand PlayClickedCommand { get; private set; }
        public ICommand SliderDragStartedCommand { get; private set; }
        public ICommand SliderDragCompletedCommand { get; private set; }
        public ICommand MusicCommentClickedCommand { get; private set; }
        public ICommand VulomeDragCompletedCommand { get; private set; }
        public ICommand MoreBtnClickCommand { get; private set; }
        public ICommand MVBtnClickedCommand { get; private set; }
        public ICommand ShareBtnClickedCommand { get; private set; }
        public ICommand DownLoadBtnClickedCommand { get; private set; }
        public ICommand TreeBtnClickedCommand { get; private set; }
        public ICommand TestCommand { get; private set; }
        public ICommand ShareTappedCommand { get; private set; }
        #endregion
    }
    public delegate void ColorChangedHandler();
    public delegate void EventHandler();
    public delegate void LrcEventHandler(SongLyricsModel e);
}
