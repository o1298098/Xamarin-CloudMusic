using MediaManager;
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
    public class ScrollVideoPageViewModel : BaseViewModel
    {
        VideoInfo _videoInfo;
        int offset;
        Random ran;
        bool _isallload,_isLoading;
        public ScrollVideoPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ran = new Random(DateTime.Now.Millisecond);
            ListViewItemAppearingCommand = new Command<object>(ListViewItemAppearing);
            ListViewRefreahCommand = new Command(LoadVideos);
            ViewCommentCommand = new DelegateCommand<VideoInfo.Data>(ViewComment);
            LoadVideos();

        }
        void LoadVideos()
        {
            offset = 0;
            IsBusy = true;
            Task.Run(() => {                
                var o = Enum.GetValues(typeof(Models.ENUM.CouldMusicVideoGroup));
                int c = ran.Next(o.Length);
                var r = CloudMusicApiHelper.GetVideoInfoByGroup((Models.ENUM.CouldMusicVideoGroup)o.GetValue(c), 20,offset);
                if (r != null)
                    if (r.code == 200)
                    {
                        VideoInfo = r;
                    }
                IsLoading = false;
                IsBusy = false;
            });
        }
        void LoadMoreVideo()
        {
            //offset += 20;
            IsBusy = true;
            Task.Run(() => {
                var o = Enum.GetValues(typeof(Models.ENUM.CouldMusicVideoGroup));
                int c = ran.Next(o.Length);
                var r = CloudMusicApiHelper.GetVideoInfoByGroup((Models.ENUM.CouldMusicVideoGroup)o.GetValue(c), 20, offset);
                if (r != null)
                    if (r.code == 200)
                    {
                        if(VideoInfo!=null)
                            foreach(var q in r.datas)
                                VideoInfo.datas.Add(q);
                    }
                IsBusy = false;
            });

        }
      public void ViewComment(VideoInfo.Data e)
        {
            var s = e;
        }

        void ListViewItemAppearing(object e)
        {
            var s = e as ItemVisibilityEventArgs;
            if (s.ItemIndex + 1 == VideoInfo.datas.Count())
            {
                LoadMoreVideo();
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        public VideoInfo VideoInfo
        {
            get => _videoInfo;
            set => SetProperty(ref _videoInfo ,value, "VideoInfo");
        }
        public bool IsAllLoad
        {
            get => _isallload;
            set => SetProperty(ref _isallload, value, "IsAllLoad");
        }
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value, "IsLoading");
        }
        public ICommand ListViewItemAppearingCommand { get; private set; }
        public ICommand ListViewRefreahCommand { get; private set; }
        public ICommand ViewCommentCommand { get; private set; }
    }
}
