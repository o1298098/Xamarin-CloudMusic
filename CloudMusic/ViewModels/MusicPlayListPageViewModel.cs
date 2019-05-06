using MediaManager;
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

namespace CloudMusic.ViewModels
{
    public class MusicPlayListPageViewModel : BaseViewModel
    {
        string playlistId,_bgPic;
        MusicPlayListDetail musicPlayList;
        public MusicPlayListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            MusicPlayList = new MusicPlayListDetail();
            BgPic = "CacheBG.jpg";
            PlayBtnClickedCommand = new DelegateCommand(PlayBtnClicked);
            DownLoadClickedCommand = new DelegateCommand(DownLoad);
        }
        void DownLoad()
        {
            DependencyService.Get<IToast>().ShortAlert("后台下载中");
            Task.Run(()=> {
                var downloadManager = Plugin.DownloadManager.CrossDownloadManager.Current;
                string ids = string.Join(",", MusicPlayList.playlist.trackIds.Select(q => q.id));
                var r = CloudMusicApiHelper.GetSong(ids, Models.ENUM.CouldMusicBpsType.high);
                if (r != null)
                    if (r.code == 200)
                        for (int i = 0; i < r.data.Count(); i++)
                        {
                            if (r.data[i].url == null)
                                continue;
                            Dictionary<string, string> filedic = new Dictionary<string, string>();
                            filedic["name"] = MusicPlayList.playlist.tracks[i].name;
                            filedic["type"] = r.data[i].encodeType;
                            var file = downloadManager.CreateDownloadFile(r.data[i].url, filedic);
                            downloadManager.Start(file);
                        }
            });
           
        }
       async void PlayBtnClicked()
        {
            await CrossMediaManager.Current.Stop();
            var param = new NavigationParameters();
            param.Add("PlayListModel", MusicPlayList);
            await NavigationService.NavigateAsync("BlurImagePage", param);
        } 
        void GetPlayListData()
        {
            IsBusy = true;
            Task.Run(()=>{
                var s = CloudMusicApiHelper.PlayListDetial(playlistId);
                if (s != null)
                    if (s.code == 200)
                    {
                        MusicPlayList = s;
                        if(string.IsNullOrWhiteSpace(BgPic))
                            BgPic = s.playlist.coverImgUrl;
                    }
                IsBusy = false;
            });
           
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters["PlayListpic"] != null)
                BgPic = parameters["PlayListpic"].ToString();
            if (parameters["PlayListid"] != null)
            {
                playlistId = parameters["PlayListid"].ToString();
                GetPlayListData();
            }

        }
        public string BgPic
        {
            get => _bgPic;
            set => SetProperty(ref _bgPic, value, "BgPic");
        }
        public MusicPlayListDetail MusicPlayList
        {
            get => musicPlayList;
            set => SetProperty(ref musicPlayList, value, "MusicPlayList");
        }
        public ICommand PlayBtnClickedCommand { get; private set; }
        public ICommand DownLoadClickedCommand { get; private set; }
    }
}
