using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using CloudMusic.Models;
using CloudMusic.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Commands;

namespace CloudMusic.ViewModels
{
    public class BaseViewModel :BindableBase, INotifyPropertyChanged, INavigationAware, IDestructible
    {
        public System.DateTime? lastBackKeyDownTime;
        protected INavigationService NavigationService { get; private set; }
        bool isBusy = false;

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            GoBackCommand = new DelegateCommand(async ()=>await NavigationService.GoBackAsync());
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value, "IsBusy"); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

      

        public bool BackKeyPressed()
        {
            if (!lastBackKeyDownTime.HasValue || System.DateTime.Now - lastBackKeyDownTime.Value > new System.TimeSpan(0, 0, 2))
            {
                lastBackKeyDownTime = System.DateTime.Now;
                Device.BeginInvokeOnMainThread(() => DependencyService.Get<IToast>().ShortAlert("再按一次退出"));
                return true;
            }
            else
            {
                return false;
            }
        }



        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }
        public DelegateCommand GoBackCommand { get; private set; }
    }
}
