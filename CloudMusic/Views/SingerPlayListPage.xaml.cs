using CloudMusic.ViewModels;
using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class SingerPlayListPage : ContentPage
    {
        double initheight;
        double scaleparam = App.Context.Scaleparam;
        SingerPlayListPageViewModel vm;
        public SingerPlayListPage()
        {
            InitializeComponent();
            vm = BindingContext as SingerPlayListPageViewModel;
            listview.OnOverScrollUpdate += Listview_OnOverScrollUpdate;
            initheight = bgpic.HeightRequest;
            listview.ScrollChanged += Listview_ScrollChanged;
        }

        private void Listview_ScrollChanged(object sender, CustomForms.ScrollChangedEventArgs e)
        {
            double scrolly = e.NewScrollY * scaleparam;
            if (floatlayout.Y+scrolly-80 <= 0)
            {
                vm.show = true;
                bgpic.TranslationY = scrolly;
            }
            else if (floatlayout.Y + scrolly-80 > 0)
            {
                vm.show = false;
                bgpic.TranslationY = scrolly;
                var pr =System.Math.Abs(scrolly / floatlayout.Y);
                listviewheader.Opacity = 1 - pr;
                bgcover.Opacity = 0.2 + 0.6 * pr;
            }
        }

        private void Listview_OnOverScrollUpdate(object sender, float offset)
        {
            if (offset > 0)
            {
                bgpic.HeightRequest = initheight + offset;
                bgpic.Scale = 1 + offset / (initheight * 20);
            }
        }
    }
}
