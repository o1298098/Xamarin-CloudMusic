using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CloudMusic.CustomForms;
using CloudMusic.iOS.Renderers;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace CloudMusic.iOS.Renderers
{
    public class CustomTabbedPageRenderer: TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (TabBar?.Items == null)
                return;
            var tabs = Element as TabbedPage;
            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                {
                    UpdateTabBarItem(TabBar.Items[i], tabs.Children[i].Icon);
                }
            }
        }
        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            /*TabBar.InvalidateIntrinsicContentSize();

            var orientation = UIApplication.SharedApplication.StatusBarOrientation;

            nfloat tabSize = 44.0f;

            if (orientation == UIInterfaceOrientation.LandscapeLeft ||
               orientation == UIInterfaceOrientation.LandscapeRight)
            {
                tabSize = 32.0f;
            }

            var tabFrame = TabBar.Frame;
            tabFrame.Height = tabSize;
            tabFrame.Y = View.Frame.Y+30;
            TabBar.Frame = tabFrame;
            TabBar.Translucent = false;
            TabBar.Translucent = true;*/

        }
        public override void ViewWillAppear(bool animated)
        {
            if(TabBar?.Items == null)
                return;

            // Go through our elements and change the icons
            var tabs = Element as TabbedPage;
            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                {
                    UpdateTabBarItem(TabBar.Items[i], tabs.Children[i].Icon);
                }
            }

            base.ViewWillAppear(animated);
        }
        private void UpdateTabBarItem(UITabBarItem item, string icon)
        {
            if (item == null)
                return;
            if(icon!=null)
            {
               
                var image= UIImage.FromFile(icon);
                var imgR = ResizeImage(image, 30, 30);
                item.Image = imgR;
                item.SelectedImage = imgR;
                item.ImageInsets = new UIEdgeInsets(15f, 0, -15f,0);
            }
            if (item.Title == "菜单" || item.Title == "搜索")
                item.Title = string.Empty;
            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.SystemFontOfSize(12),TextColor = Xamarin.Forms.Color.FromHex("#757575").ToUIColor()}, UIControlState.Normal);
            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.SystemFontOfSize(16),TextColor = Xamarin.Forms.Color.FromHex("#333333").ToUIColor()}, UIControlState.Selected);
            item.TitlePositionAdjustment= new UIOffset(0, -4);

        }
        public UIImage ResizeImage(UIImage sourceImage, float width, float height)
        {
            UIGraphics.BeginImageContext(new SizeF(width, height));
            sourceImage.Draw(new RectangleF(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
    }
}