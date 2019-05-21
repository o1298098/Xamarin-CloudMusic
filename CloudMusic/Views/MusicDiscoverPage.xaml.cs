using CloudMusic.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class MusicDiscoverPage : ContentPage
    {
        MusicDiscoverPageViewModel vm;
        public MusicDiscoverPage()
        {
            InitializeComponent();
            vm = BindingContext as MusicDiscoverPageViewModel;
            PlayListlayout.ChildAdded += PlayListlayout_ChildAdded;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        int i = 0;
        private void PlayListlayout_ChildAdded(object sender, ElementEventArgs e)
        {
            TapGestureRecognizer s = new TapGestureRecognizer();
            s.Command = vm.PlaylistUnitClickedCommand;
            var param = new NavigationParameters();
            param.Add("PlayListid", vm.personalized.result[i].id.ToString());
            param.Add("PlayListpic", vm.personalized.result[i].picUrl);
            s.CommandParameter = param;
            ((AbsoluteLayout)e.Element).GestureRecognizers.Add(s);
            i++;
        }
    }
}
