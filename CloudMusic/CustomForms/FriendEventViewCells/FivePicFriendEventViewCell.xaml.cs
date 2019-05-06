using CloudMusic.Models.Media;
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
    public partial class FivePicFriendEventViewCell : ViewCell
    {
        public FivePicFriendEventViewCell()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            usericon.Source = null;
            image.Source = null;
            image2.Source = null;
            image3.Source = null;
            image4.Source = null;
            image5.Source = null;
            songimage.Source = null;
            var item = BindingContext as MusicEventModel.friendEvent;

            if (item == null)
            {
                return;
            }
            usericon.Source = item.user.avatarUrl;
            if (item.Conent.song != null)
                songimage.Source = item.Conent.song.album.picUrl;
            if (item.pics?.Count() >=5)
            {
                image.Source = item.pics[0].squareUrl;
                image2.Source = item.pics[1].squareUrl;
                image3.Source = item.pics[2].squareUrl;
                image4.Source = item.pics[3].squareUrl;
                image5.Source = item.pics[4].squareUrl;
            }
           
            base.OnBindingContextChanged();
        }
    }
}