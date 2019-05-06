using CloudMusic.Actions.ApiHelper;
using CloudMusic.Models;
using CloudMusic.Models.Media;
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
    public class RecommendSongsPageViewModel : BaseViewModel
    {
        RecommendSongs _recommendSongs;
        SiMiUsers _simiUsers;
        Color _mainColor;
        bool init;
        public RecommendSongsPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (!init)
            {
                GetRecommendSongs();
                init = true;
            }
        }
        void GetRecommendSongs()
        {
            Task.Run(() => {
                var r = CloudMusicApiHelper.GetRecommendSongs();
                if (r != null)
                    if (r.code == 200)
                    {

                        recommendSongs = r;
                        if (recommendSongs.recommend.Count > 0)
                        {
                            GetSiMiUsers(recommendSongs.recommend[0].id.ToString());
                            //ThemeColors colors= await DependencyService.Get<IPalette>().GetColorAsync(new UriImageSource {Uri=new Uri(recommendSongs.recommend[0].album.picUrl)});
                            //MainColor = colors.MainColor;
                        }
                            
                    }
            });
        }
        void GetSiMiUsers(string id)
        {
            Task.Run(() => {
                var r = CloudMusicApiHelper.GetSiMiUsers(id);
                if (r != null)
                    if (r.code == 200)
                        simiUsers = r;
            });
        }
        public Color MainColor
        {
            get => _mainColor;
            set => SetProperty(ref _mainColor, value, "MainColor");
        }
        public SiMiUsers simiUsers
        {
            get => _simiUsers;
            set => SetProperty(ref _simiUsers, value, "simiUsers");
        }
        public RecommendSongs recommendSongs
        {
            get => _recommendSongs;
            set => SetProperty(ref _recommendSongs, value, "recommendSongs");
        }
    }
}
