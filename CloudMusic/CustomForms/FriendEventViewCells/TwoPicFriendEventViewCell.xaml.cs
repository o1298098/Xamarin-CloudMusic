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
    public partial class TwoPicFriendEventViewCell : ViewCell
    {
        public TwoPicFriendEventViewCell()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            usericon.Source = null;
            image.Source = null;
            image2.Source = null;
            songimage.Source = null;
            var item = BindingContext as MusicEventModel.friendEvent;

            if (item == null)
            {
                return;
            }
            usericon.Source = item.user.avatarUrl;
            if (item.Conent.song != null)
                songimage.Source = item.Conent.song.album.picUrl;
            if (item.pics?.Count() >= 2)
            {
                image.Source = item.pics[0].squareUrl;
                image2.Source = item.pics[1].squareUrl;
            }
            base.OnBindingContextChanged();
        }
    }
}