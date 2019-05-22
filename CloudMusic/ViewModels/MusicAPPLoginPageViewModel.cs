using CloudMusic.Actions.ApiHelper;
using CloudMusic.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CloudMusic.ViewModels
{
    public class MusicAPPLoginPageViewModel : BaseViewModel
    {
        public MusicAPPLoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            LoginClickCommand = new DelegateCommand(LoginClick);
            TestClickCommand = new DelegateCommand(TestClick);
        }
        void LoginClick()
        {
            Login(username,password);
        }


        void TestClick()
        {
            Login("13690290528", "o1298098");
        }

        private void Login(string user,string pd)
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
        string _username, _password;
        public string username
        {
            get => _username;
            set => SetProperty(ref _username, value, "username");
        }
        public string password
        {
            get => _password;
            set => SetProperty(ref _password, value, "password");
        }
        public ICommand LoginClickCommand { get; private set; }
        public ICommand TestClickCommand { get; private set; }

    }
}
