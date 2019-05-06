using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using CloudMusic.CustomForms;
using CloudMusic.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace CloudMusic.Droid.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer, TabLayout.IOnTabSelectedListener
    {
        private TabLayout tabLayout = null;
        private ObservableCollection<Xamarin.Forms.Element> children;
        private IPageController controller;

        public CustomTabbedPageRenderer(Context context) : base(context)
        {
           
        }
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                controller = Element as IPageController;
                children = controller.InternalChildren;
            }
            tabLayout = (TabLayout)GetChildAt(1);
            tabLayout.TabGravity = TabLayout.GravityFill;
        }
        protected override void SetTabIcon(TabLayout.Tab tab, FileImageSource icon)
        {
            base.SetTabIcon(tab, icon);
            if (tab.Text == "菜单" || tab.Text == "搜索")
            {
                tab.SetCustomView(Resource.Layout.CustomTablayout);
                var customView = Element as CustomTabbedPage;
                var imagebtn = tab.CustomView.FindViewById<Android.Widget.ImageButton>(Resource.Id.tabpage_btn);
                imagebtn.Background=tab.Icon;
                imagebtn.Click += (sender, e) =>
                {
                    if (tab.Text == "菜单")
                        CustomTabbedPage.OnMenuClicked(customView, EventArgs.Empty);
                    else if(tab.Text == "搜索")
                        CustomTabbedPage.OnSearchClick(customView, EventArgs.Empty);
                };
            }
           
        }
        void TabLayout.IOnTabSelectedListener.OnTabReselected(TabLayout.Tab tab)
        {
            // Logic here to send event to desired XF component using MessagingService or other
        }

        void TabLayout.IOnTabSelectedListener.OnTabSelected(TabLayout.Tab tab)
        {
            if (Element == null)
                return;

            int selectedIndex = tab.Position;
            if (Element.Children.Count > selectedIndex && selectedIndex >=1)
                Element.CurrentPage = Element.Children[selectedIndex];

            // Logic here to send event to desired XF component using MessagingService or other
        }

        void TabLayout.IOnTabSelectedListener.OnTabUnselected(TabLayout.Tab tab)
        {
            // Logic here to send event to desired XF component using MessagingService or other
        }
        private void changeTabsFont()
        {
           
            ViewGroup vg = (ViewGroup)tabLayout.GetChildAt(0);
            int tabsCount = vg.ChildCount;
            for (int j = 0; j < tabsCount; j++)
            {
                ViewGroup vgTab = (ViewGroup)vg.GetChildAt(j);
                int tabChildsCount = vgTab.ChildCount;
                for (int i = 0; i < tabChildsCount; i++)
                {
                    Android.Views.View tabViewChild = vgTab.GetChildAt(i);
                    if (tabViewChild is TextView)
                    {
                        //((TextView)tabViewChild).Typeface = font;
                        ((TextView)tabViewChild).TextSize = 12;
                    }
                }
            }
        }
    }
}