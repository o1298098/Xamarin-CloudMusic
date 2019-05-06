using FFImageLoading.Forms;
using FFImageLoading.Transformations;
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
    public partial class FriendEventViewCell : ViewCell
    {
        public FriendEventViewCell()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            usericon.Source = null;
            BindableLayout.SetItemsSource(Imagelayout,null);     
            var item = BindingContext as MusicEventModel.friendEvent;

            if (item == null)
            {
                return;
            }
            usericon.Source = item.user.avatarUrl;
            if(item.pics.Count()>0)
                BindableLayout.SetItemsSource(Imagelayout, item.pics);
            //if (item.Conent.isSong && item.pics.Count() > 0)
            //    foreach (var q in item.pics)
            //        Imagelayout.Children.Add(new CachedImage
            //        {
            //            Source = q.squareUrl,
            //            DownsampleToViewSize = true,
            //            HeightRequest = 90,
            //            WidthRequest = 90,
            //            LoadingPlaceholder = "CacheBG.jpg",
            //            Aspect = Aspect.AspectFill,
            //            Transformations = new List<FFImageLoading.Work.ITransformation>() { new RoundedTransformation() { CropHeightRatio = 90, CropWidthRatio = 90 } },
            //            Margin = new Thickness(0, 2, 0, 0),
            //            CacheKeyFactory = new Actions.CustomCacheKeyFactory(),
            //            FadeAnimationEnabled = false,
            //            FadeAnimationForCachedImages=false
            //        });
            base.OnBindingContextChanged();
            this.ForceUpdateSize();
        }
    }
}