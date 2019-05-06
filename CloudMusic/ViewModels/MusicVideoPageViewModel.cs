using FFImageLoading.Forms;
using FormsVideoLibrary;
using Newtonsoft.Json;
using CloudMusic.Models.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using CloudMusic.Actions.ApiHelper;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CloudMusic.ViewModels
{
    public class MusicVideoPageViewModel : BaseViewModel
    {
        string mvid;
        UriVideoSource urisource;
        MVInfoModel mvInfo;
        ObservableCollection<ArtistsInfo> artists;
        MusicComment mvComment;
        SiMiMVModle siMiMvInfo;
        int Commentpage,_commentcount;
        bool _isallload;
        public MusicVideoPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ListViewItemAppearingCommand = new Command<object>(ListViewItemAppearing);
            NextMVCommand = new Command<string>(GoNextMV);
            Artists = new ObservableCollection<ArtistsInfo>();
        }
        void GoNextMV(string id)
        {
            mvid = id;
            MvComment = null;
            Commentpage = 0;
            GetMvInfo();
        }
        void GetMvInfo()
        {
            Task.Run(() =>
            {
                var mvinfo = CloudMusicApiHelper.GetMusicVideo(mvid);
                Artists = new ObservableCollection<ArtistsInfo>();
                if (mvinfo != null)
                    if (mvinfo.code == 200)
                    {
                        Device.BeginInvokeOnMainThread(()=> MvInfo = mvinfo);
                        string videourl = string.Empty;
                        if (!string.IsNullOrWhiteSpace(mvinfo.data.brs._1080))
                            videourl = mvinfo.data.brs._1080;
                        else if (!string.IsNullOrWhiteSpace(mvinfo.data.brs._720))
                            videourl = mvinfo.data.brs._720;
                        else if (!string.IsNullOrWhiteSpace(mvinfo.data.brs._480))
                            videourl = mvinfo.data.brs._480;
                        else if (!string.IsNullOrWhiteSpace(mvinfo.data.brs._240))
                            videourl = mvinfo.data.brs._240;
                        if (!string.IsNullOrWhiteSpace(videourl))
                            UriSource = new UriVideoSource { Uri = videourl };
                    }
                var _artists = new ObservableCollection<ArtistsInfo>();
                foreach (var q in mvinfo.data.artists)
                {
                    var artistsInfo = CloudMusicApiHelper.GetArtist(q.id.ToString());
                    if (artistsInfo != null)
                        if (artistsInfo.code == 200)
                            _artists.Add(artistsInfo);
                }
                Artists = _artists;
            });
            GetMiSiMv();
            ListViewLoadMore();

        }
        void GetMiSiMv()
        {
            Task.Run(() =>
            {
                var simimv = CloudMusicApiHelper.GetSiMiMusicVideo(mvid);
                if (simimv != null)
                    if (simimv.code == 200)
                    {
                        SiMiMvInfo = simimv;
                    }
            });
        }
        void ListViewLoadMore()
        {
            IsBusy = true;
            Task.Run(() => {                
                var comment = CloudMusicApiHelper.GetMvComment("50", mvid, Commentpage);
                if (comment != null)
                {
                        if (MvComment == null)
                        {
                            MvComment = comment;
                            CommentCount = comment.total;
                        }                           
                        else
                        if (comment.code == 200)
                            foreach (var q in comment.comments)
                            {
                                MvComment.comments.Add(q);
                            }
                    IsBusy = false;
                    Commentpage += 50;
                }
            });
        }
        void ListViewItemAppearing(object e)
        {
            var s = e as ItemVisibilityEventArgs;
            if (s.ItemIndex + 1 == MvComment.comments.Count())
            {

                if (s.ItemIndex + 1 == MvComment.total)
                    IsAllLoad = true;
                else
                    ListViewLoadMore();
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if(parameters["MVid"]!=null)
            {
                mvid = parameters["MVid"].ToString();
                GetMvInfo();
            }

        }
        public bool IsAllLoad
        {
            get => _isallload;
            set => SetProperty(ref _isallload, value, "IsAllLoad");
        }
        public SiMiMVModle SiMiMvInfo
        {
            get => siMiMvInfo;
            set => SetProperty(ref siMiMvInfo, value, "SiMiMvInfo");
        }
        public MusicComment MvComment
        {
            get => mvComment;
            set => SetProperty(ref mvComment, value, "MvComment");
        }
        public ObservableCollection<ArtistsInfo> Artists
        {
            get => artists;
            set => SetProperty(ref artists, value, "Artists");
        }
        public MVInfoModel MvInfo
        {
            get => mvInfo;
            set => SetProperty(ref mvInfo, value, "MvInfo");
        }
        public UriVideoSource UriSource
        {
            get => urisource;
            set => SetProperty(ref urisource,value, "UriSource");
        }
        public int CommentCount
        {
            get => _commentcount;
            set => SetProperty(ref _commentcount, value, "CommentCount");
        }
        public ICommand NextMVCommand { get; private set; }
        public ICommand ListViewItemAppearingCommand { get; private set; }
    }
}
