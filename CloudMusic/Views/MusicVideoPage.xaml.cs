using FFImageLoading;
using CloudMusic.CustomForms;
using CloudMusic.ViewModels;
using Xamarin.Forms;
using CloudMusic.Services;

namespace CloudMusic.Views
{
    public partial class MusicVideoPage : ContentPage
    {
        MusicVideoPageViewModel vm;
        int i = 0;
        public MusicVideoPage()
        {
            InitializeComponent();
            vm = BindingContext as MusicVideoPageViewModel;
            SimiMVsl.ChildAdded += SimiMVsl_ChildAdded;
            SimiMVsl.ChildRemoved += SimiMVsl_ChildRemoved;
            listview.ScrollStateChanged += Listview_ScrollStateChanged;
            listview.ScrollChanged += Listview_ScrollChanged;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IStatusBarStyleManager>().SetDarkTheme();
        }

        private void Listview_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
                sublayout2.IsVisible = sublayout2.Y +(e.NewScrollY*App.Context.Scaleparam)<= 0? true:false;
        }

        private void Listview_ScrollStateChanged(object sender, CustomForms.ScrollStateChangedEventArgs e)
        {
            var r = sublayout.Y;
            switch (e.CurScrollState)
            {
                case ScrollStateChangedEventArgs.ScrollState.Running:
                    ImageService.Instance.SetPauseWork(true); 
                    break;
                case ScrollStateChangedEventArgs.ScrollState.Idle:
                    ImageService.Instance.SetPauseWork(false); 
                    break;
            }
        }
        private void SimiMVsl_ChildRemoved(object sender, ElementEventArgs e)
        {
            i = 0;
        }

        private void SimiMVsl_ChildAdded(object sender, ElementEventArgs e)
        {
            TapGestureRecognizer s = new TapGestureRecognizer();
            s.Command = vm.NextMVCommand;
            s.CommandParameter = vm.SiMiMvInfo.mvs[i].id.ToString();
            ((AbsoluteLayout)e.Element).GestureRecognizers.Add(s);
            i++;
        }
    }
}
