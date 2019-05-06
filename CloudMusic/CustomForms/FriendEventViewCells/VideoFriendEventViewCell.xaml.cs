using FormsVideoLibrary;
using CloudMusic.Actions.ApiHelper;
using CloudMusic.Models.Media;
using CloudMusic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CloudMusic.CustomForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoFriendEventViewCell : ViewCell
    {
        UriVideoSource source;
        public VideoFriendEventViewCell()
        {
            InitializeComponent();
            TapGestureRecognizer playtapGesture = new TapGestureRecognizer();
            playtapGesture.Tapped += PlaytapGesture_Tapped;
            playbtn.GestureRecognizers.Add(playtapGesture);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            videoplayer.Pause();
        }
        protected override void OnBindingContextChanged()
        {
            usericon.Source = null;
            videoplayer.IsVisible = false;
            videocover.IsVisible = true;
            videoinfo.IsVisible = true;
            playbtn.IsVisible = true;
            videoplayer.Source = null;
            videocover.Source = null;
            var item = BindingContext as MusicEventModel.friendEvent;

            if (item == null)
            {
                return;
            }
            usericon.Source = item.user.avatarUrl;
            if (item.Conent.video != null)
            {
                //source = new UriVideoSource { Uri = item.Conent.video.urlInfo. };
                videocover.Source = item.Conent.video.coverUrl;
            }
            base.OnBindingContextChanged();
        }
        private void PlaytapGesture_Tapped(object sender, EventArgs e)
        {
            Task.Run(() => {
                var q = CloudMusicApiHelper.GetVideoUrls(((MusicEventModel.friendEvent)BindingContext).Conent.video.videoId);
                if (q != null)
                {
                    Device.BeginInvokeOnMainThread(()=> {
                        videoplayer.IsVisible = true;
                        videocover.IsVisible = false;
                        videoinfo.IsVisible = false;
                        playbtn.IsVisible = false;
                        videoplayer.Source = new UriVideoSource { Uri = q.urls[0].url };
                        videoplayer.Play();
                        this.ForceUpdateSize();
                    });                 
                }
                else
                    Device.BeginInvokeOnMainThread(()=>DependencyService.Get<IToast>().ShortAlert("播放失败"));
                
            });
          
        }
    }
}