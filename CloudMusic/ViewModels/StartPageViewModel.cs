using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CloudMusic.Actions.ApiHelper;
using CloudMusic.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace CloudMusic.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        public StartPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            TestAcountCommand = new DelegateCommand(TestClick);
        }
        void TestClick()
        {
            Login("13690290528", "o1298098");
        }

        private void Login(string user, string pd)
        {
            Task.Run(() =>
            {
                var r = CloudMusicApiHelper.Login(user, pd);
                if (r != null)
                {
                    if (r.code == 200)
                    {
                        Xamarin.Essentials.Preferences.Set("userid", r.profile.userId);
                        Xamarin.Essentials.Preferences.Set("username", r.account.userName);
                        Device.BeginInvokeOnMainThread(async () => await NavigationService.NavigateAsync("/NavigationPage/MusicHomePage?selectedTab=MusicDiscoverPage"));
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => DependencyService.Get<IToast>().ShortAlert("错误"));
                }

            });
        }

        public ICommand TestAcountCommand { get; private set; }
    }
}
