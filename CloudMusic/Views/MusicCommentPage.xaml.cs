using CloudMusic.Services;
using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class MusicCommentPage : ContentPage
    {
        public MusicCommentPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IStatusBarStyleManager>().SetLightTheme();
        }
    }
}
