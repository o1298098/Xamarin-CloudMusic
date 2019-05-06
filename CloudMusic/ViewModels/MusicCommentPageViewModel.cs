using Newtonsoft.Json;
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
    public class MusicCommentPageViewModel : BaseViewModel
    {
        string commenturl= "https://music.aityp.com/comment/music?limit=50&id=";
        int page,_commentcount;
        string MusicID;
        MusicComment _musicComment;
        bool _isallload;
        public MusicCommentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ListViewItemAppearingCommand = new Command<object>(ListViewItemAppearing);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters["MusicID"] != null)
            {

                MusicID = parameters["MusicID"].ToString();
                Task.Run(() => {
                    string result = ApiHelper.HttpClient.HttpGet(commenturl + parameters["MusicID"].ToString());

                if (result != "err")
                {
                    var comment = JsonConvert.DeserializeObject<MusicComment>(result);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        musicComment = comment;
                        CommentCount = comment.total;
                    });
                }
                });
            }
        }
        void ListViewLoadMore()
        {
            page+=50;
            IsBusy = true;
            Task.Run(()=> {
                string loadurl = commenturl + MusicID + "&offset=" + page;
                string result = ApiHelper.HttpClient.HttpGet(loadurl);
                if (result != "err")
                {
                    var comment = JsonConvert.DeserializeObject<MusicComment>(result);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        foreach (var q in comment.comments)
                        {
                            musicComment.comments.Add(q);
                        }
                        IsBusy = false;
                    });
                }
            });
        }
        void ListViewItemAppearing(object e)
        {
            var s = e as ItemVisibilityEventArgs;
            if (s.ItemIndex + 1 == musicComment.comments.Count())
            {

                if (s.ItemIndex + 1 == musicComment.total)
                    IsAllLoad = true;
                else
                    ListViewLoadMore();
            }
        }
        public MusicComment musicComment
        {
            get=> _musicComment;
            set => SetProperty(ref _musicComment,value, "musicComment");
        }
        public bool IsAllLoad
        {
            get => _isallload;
            set => SetProperty(ref _isallload, value, "IsAllLoad");
        }
        public int CommentCount
        {
            get => _commentcount;
            set => SetProperty(ref _commentcount, value, "CommentCount");
        }

        public ICommand ListViewItemAppearingCommand { get; private set; } 

    }
}
