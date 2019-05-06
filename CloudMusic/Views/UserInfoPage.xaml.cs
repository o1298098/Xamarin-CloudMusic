using CloudMusic.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class UserInfoPage : ContentPage
    {
        UserInfoPageViewModel vm;
        public UserInfoPage()
        {
            InitializeComponent();
            vm = BindingContext as UserInfoPageViewModel;
            userplaylists.ChildAdded += Userplaylists_ChildAdded;
            subplaylists.ChildAdded += Subplaylists_ChildAdded;
        }
        int playlistcount = 0;
        int subplaylistcount = 0;
        private void Subplaylists_ChildAdded(object sender, ElementEventArgs e)
        {
            TapGestureRecognizer s = new TapGestureRecognizer();
            s.Command = vm.PlaylistUnitClickedCommand;
            var param = new NavigationParameters();
            param.Add("PlayListid", vm.subscribedPlayLists[subplaylistcount].id);
            param.Add("PlayListpic", vm.subscribedPlayLists[subplaylistcount].coverImgUrl);
            s.CommandParameter = param;
            ((Grid)e.Element).GestureRecognizers.Add(s);
            subplaylistcount++;
        }
        private void Userplaylists_ChildAdded(object sender, ElementEventArgs e)
        {
            TapGestureRecognizer s = new TapGestureRecognizer();
            s.Command = vm.PlaylistUnitClickedCommand;
            var param = new NavigationParameters();
            param.Add("PlayListid", vm.userPlayLists[playlistcount].id);
            param.Add("PlayListpic", vm.userPlayLists[playlistcount].coverImgUrl);
            s.CommandParameter = param;
            ((Grid)e.Element).GestureRecognizers.Add(s);
            playlistcount++;
          
        }
    }
}
