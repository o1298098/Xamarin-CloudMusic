using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class PersonalFMPage : ContentPage
    {
        public PersonalFMPage()
        {
            InitializeComponent();
            double scaleheight = App.Context.Scaleparam * Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height;
            popmenu.PopupView.StartY = (int)(scaleheight - popmenu.HeightRequest) + 40;
        }
    }
}
