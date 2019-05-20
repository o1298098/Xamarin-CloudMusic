using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using CloudMusic.Actions.Effect;
using CloudMusic.Models;
using MediaManager;
using CloudMusic.ViewModels;
using CloudMusic.Actions;

namespace CloudMusic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlurImagePage : ContentPage
    {
        Dictionary<object, DragInfo> dragDictionary = new Dictionary<object, DragInfo>();
        Grid ngrid;
        BlurImagePageViewModel vm;
        PlayNeedleAnimationManager playNeedleAnimationManager;
        delegate void EventHandler();
        public BlurImagePage()
        {
            InitializeComponent();
            vm = BindingContext as BlurImagePageViewModel;
            double scaleheight = App.Context.Scaleparam * Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height;
            popmenu.PopupView.StartY = (int)(scaleheight - popmenu.HeightRequest)+40;
            playNeedleAnimationManager = new PlayNeedleAnimationManager(PlayNeedle,30,300,Models.ENUM.PlayNeedleRunMode.down);
            //FontImage.OnLoadFinshed += delegate
            //{
            //    if (FontImage.IsLoadFinsh)
            //    {
            //        new Animation(v => FontImage.Opacity = v, 0, 0.9).Commit(this, "SimpleAnimation", 16, 700, Easing.Linear, (v, c) => { ImageSource source = FontImage.Source; BackImage.Source = source; });
            //    }
            //    else
            //    {
            //        FontImage.Opacity = 0;
            //    }
            //};
            //FontImage.Success += delegate
            //{
            //        new Animation(v => FontImage.Opacity = v, 0, 0.9).Commit(this, "SimpleAnimation", 16, 700, Easing.Linear, (v, c) => { ImageSource source = FontImage.Source; BackImage.Source = source; });
            //        FontImage.Opacity = 0;
            //};
            //FontImage.Finish += delegate {
            //    FontImage.Opacity = 0;
            //};
            CrossMediaManager.Current.PlayingChanged += Current_PlayingChangedAsync;
            PlayBtn.Clicked += PlayBtn_ClickedAsync;
            PlayBackBtn.Clicked += playNeedleAnimationAsync;
            PlayNextBtn.Clicked+= playNeedleAnimationAsync;
            vm.OnMusicChange += Vm_OnMusicChangeAsync;
            coverFlowView.UserInteracted += CoverFlowView_UserInteractedAsync;
            vm.OnLrcChange += Vm_OnLrcChange;

            // blurredImage.On<iOS>().UseBlurEffect(BlurEffectStyle.Dark);
        }

        private void Vm_OnLrcChange(Models.Media.SongLyricsModel e)
        {
            LyricsView.ScrollTo(e,ScrollToPosition.Center,true);
        }

        private async void CoverFlowView_UserInteractedAsync(PanCardView.CardsView view, PanCardView.EventArgs.UserInteractedEventArgs args)
        {
            if (args.Status == PanCardView.Enums.UserInteractionStatus.Started)
            {
                await playNeedleAnimationManager.RunAnimationAsync(Models.ENUM.PlayNeedleRunMode.up);
            }
            else if (args.Status == PanCardView.Enums.UserInteractionStatus.Ended)
            {
                await playNeedleAnimationManager.RunAnimationAsync(Models.ENUM.PlayNeedleRunMode.down);
            }
        }
        async void playNeedleAnimationAsync(object sender, EventArgs e)
        {
            await playNeedleAnimationManager.RunAnimationAsync(Models.ENUM.PlayNeedleRunMode.up);
            await playNeedleAnimationManager.RunAnimationAsync(Models.ENUM.PlayNeedleRunMode.down);
            Vm_OnMusicChangeAsync();
        }

        private void Vm_OnMusicChangeAsync()
        {
            ngrid = coverFlowView.CurrentView as Grid;
        }

        private async void Current_PlayingChangedAsync(object sender, MediaManager.Playback.PlayingChangedEventArgs e)
        {
            if (ngrid != null)
                await ngrid?.RelRotateTo(36, 1000);
            else
            {
                ngrid = coverFlowView.CurrentView as Grid;
            }
        }
        private async void PlayBtn_ClickedAsync(object sender, EventArgs e)
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                PlayBtn.Source = "ic_play.png";
                await playNeedleAnimationManager.RunAnimationAsync(Models.ENUM.PlayNeedleRunMode.up);
            }
            else
            {
                PlayBtn.Source = "ic_pause.png";
                await playNeedleAnimationManager.RunAnimationAsync(Models.ENUM.PlayNeedleRunMode.down);

            }
        }
    }
}