using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class SingerPlayListPage : ContentPage
    {
        double initheight;
        public SingerPlayListPage()
        {
            InitializeComponent();
            listview.OnOverScrollUpdate += Listview_OnOverScrollUpdate;
            initheight = bgpic.HeightRequest;
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
