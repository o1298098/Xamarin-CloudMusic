using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CloudMusic.ViewModels
{
    public class MusicHomePageViewModel : BaseViewModel
    {
      
        public MusicHomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SearchClickCommand = new DelegateCommand(GoSearchAsync);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);                
            
        }
        public async void GoSearchAsync()
        {
            await NavigationService.NavigateAsync("SearchMusicPage");
        }
        public ICommand SearchClickCommand { get; private set; }
    }
}
