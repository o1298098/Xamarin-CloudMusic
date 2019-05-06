using CloudMusic.CustomForms;
using CloudMusic.Services;
using CloudMusic.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace CloudMusic.Views
{
    public partial class MusicHomePage :CustomTabbedPage
    {
        MusicHomePageViewModel vm;
        public MusicHomePage()
        {
            InitializeComponent();
            vm = BindingContext as MusicHomePageViewModel;
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().DisableSwipePaging();
            this.MenuClicked += MusicHomePage_MenuClicked;
            this.SearchClicked += MusicHomePage_SearchClicked;
        }

        private void MusicHomePage_SearchClicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IToast>().ShortAlert("菜单");
        }

        private void MusicHomePage_MenuClicked(object sender, System.EventArgs e)
        {
            vm.GoSearchAsync();
        }
    }
}
