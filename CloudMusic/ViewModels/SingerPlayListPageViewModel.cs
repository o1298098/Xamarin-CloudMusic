using CloudMusic.Actions.ApiHelper;
using CloudMusic.Models.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMusic.ViewModels
{
    public class SingerPlayListPageViewModel : BaseViewModel
    {
        ArtistsInfo _artistDetail;
        string artistid;
        public SingerPlayListPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters["artistid"] != null)
            {
                artistid = parameters["artistid"].ToString();
                GetArtistDetail();
            }
        }
        void GetArtistDetail()
        {
            Task.Run(() => {
                var r = CloudMusicApiHelper.ArtistDetial(artistid);
                if (r != null)
                    if (r.code == 200)
                        artistDetail = r;
            });
        }
       public ArtistsInfo artistDetail
        {
            get => _artistDetail;
            set => SetProperty(ref _artistDetail, value, "artistDetail");
        }
    }
}
