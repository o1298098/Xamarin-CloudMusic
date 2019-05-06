using System;
using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class MusicPlayListPage : ContentPage
    {
        double scaleparam = App.Context.Scaleparam;
        public MusicPlayListPage()
        {
            InitializeComponent();
            listview.ScrollChanged += Listview_ScrollChanged;
        }

        private void Listview_ScrollChanged(object sender, CustomForms.ScrollChangedEventArgs e)
        {
            double scrolly = e.NewScrollY * scaleparam;
            if (scrolly <= -235)
            {
                playbtnlayout.IsVisible = true;
                playlayoutbg.TranslationY = -235;
                playlayoutbg.IsVisible = true;

            }
            else if (scrolly > -235)
            {
                playbtnlayout.IsVisible = false;
                background.TranslationY = scrolly;
                playlayoutbg.IsVisible = false;
                headerlayout.Opacity = 1-Math.Abs(scrolly/240);
            }
        }
    }
}
