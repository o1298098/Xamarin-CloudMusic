using CloudMusic.Models;
using CloudMusic.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CloudMusic.ViewModels
{
    public class EmptyPageViewModel : BaseViewModel
    {
        public EmptyPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters["pic"] != null)
            {
                Task.Run(async ()=> {
                    var r = await DependencyService.Get<IPalette>().GetColorAsync(new UriImageSource { Uri = new Uri(parameters["pic"].ToString()) });
                    Device.BeginInvokeOnMainThread(()=> theme=r);
                });
                
            }

        }
        ThemeColors _theme;
        public ThemeColors theme
        {
            get => _theme;
            set => SetProperty(ref _theme,value, "theme");
        }
    }
}
