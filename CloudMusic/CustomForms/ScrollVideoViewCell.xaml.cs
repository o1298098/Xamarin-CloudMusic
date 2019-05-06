using FormsVideoLibrary;
using CloudMusic.Models.Media;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Navigation;

namespace CloudMusic.CustomForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScrollVideoViewCell : ViewCell
    {
        UriVideoSource source;
        public ScrollVideoViewCell()
        {
            InitializeComponent();
            TapGestureRecognizer playtapGesture=new TapGestureRecognizer();
            playtapGesture.Tapped += PlaytapGesture_Tapped;
            playbtn.GestureRecognizers.Add(playtapGesture);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            videoplayer.IsVisible = false;
            videocover.IsVisible = true;
            videoinfo.IsVisible = true;
            playbtn.IsVisible = true;
            videoplayer.Source = null;
            videocover.Source = null;
            UserIcon.Source = null;
            Bgicon.Source = null;
            var item = BindingContext as VideoInfo.Datas;
            if (item == null)
            {
                return;
            }
            if(item.data.urlInfo!=null)
                source = new UriVideoSource {Uri= item.data.urlInfo.url};
            videocover.Source = item.data.coverUrl;
            if (item.data.creator != null)
            {
                UserIcon.Source = item.data.creator.avatarUrl;
                Bgicon.Source = item.data.creator.backgroundUrl;
            }
        }

        private void PlaytapGesture_Tapped(object sender, EventArgs e)
        {
            videoplayer.IsVisible = true;
            videocover.IsVisible = false;
            videoinfo.IsVisible = false;
            playbtn.IsVisible = false;
            videoplayer.Source = source;
            videoplayer.Play();
            this.ForceUpdateSize();
        }
    }
}