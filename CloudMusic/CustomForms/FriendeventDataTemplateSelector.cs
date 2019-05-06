using CloudMusic.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.CustomForms
{
    public class FriendeventDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OneTemplate { get; set; }
        public DataTemplate TwoTemplate { get; set; }
        public DataTemplate ThreeTemplate { get; set; }
        public DataTemplate FourTemplate { get; set; }
        public DataTemplate FiveTemplate { get; set; }
        public DataTemplate SixTemplate { get; set; }
        public DataTemplate SevenTemplate { get; set; }
        public DataTemplate EightTemplate { get; set; }
        public DataTemplate NineTemplate { get; set; }
        public DataTemplate VideoTemplate { get; set; }
        public FriendeventDataTemplateSelector()
        {
            this.OneTemplate = new DataTemplate(typeof(OnePicFriendEventViewCell));
            this.TwoTemplate = new DataTemplate(typeof(TwoPicFriendEventViewCell));
            this.ThreeTemplate = new DataTemplate(typeof(ThreePicFriendEventViewCell));
            this.FourTemplate = new DataTemplate(typeof(FourPicFriendEventViewCell));
            this.FiveTemplate = new DataTemplate(typeof(FivePicFriendEventViewCell));
            this.SixTemplate = new DataTemplate(typeof(SixPicFriendEventViewCell));
            this.SevenTemplate = new DataTemplate(typeof(SevenPicFriendEventViewCell));
            this.EightTemplate = new DataTemplate(typeof(EightPicFriendEventViewCell));
            this.NineTemplate = new DataTemplate(typeof(NinePicFriendEventViewCell));
            this.VideoTemplate = new DataTemplate(typeof(VideoFriendEventViewCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var w = ((MusicEventModel.friendEvent)item);
            if (!w.Conent.isSong)
                return VideoTemplate;
            switch (w.pics?.Count())
            {
                case 1:
                    return OneTemplate;
                case 2:
                    return TwoTemplate;
                case 3:
                    return ThreeTemplate;
                case 4:
                    return FourTemplate;
                case 5:
                    return FiveTemplate;
                case 6:
                    return SixTemplate;
                case 7:
                    return SevenTemplate;
                case 8:
                    return EightTemplate;
                case 9:
                    return NineTemplate;
                default:
                    return OneTemplate;
            }
        }
    }
}
