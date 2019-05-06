using FFImageLoading;
using CloudMusic.CustomForms;
using CloudMusic.Models;
using CloudMusic.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class RecommendSongsPage : ContentPage
    {
        RecommendSongsPageViewModel vm;
        private const int ParallaxSpeed = 5;
        private double _lastScroll;
        double totaly;
        double scaleparam = App.Context.Scaleparam;
        Dictionary<object, DragInfo> dragDictionary = new Dictionary<object, DragInfo>();
        public RecommendSongsPage()
        {
            InitializeComponent();
            vm = BindingContext as RecommendSongsPageViewModel;
            listview.ScrollChanged += Listview_ScrollChanged;
            listview.ScrollStateChanged += Listview_ScrollStateChanged;
        }


        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    listview.ScrollChanged -= Listview_ScrollChanged;
        //    listview.ScrollStateChanged -= Listview_ScrollStateChanged;
        //}
        private void Listview_ScrollStateChanged(object sender, CustomForms.ScrollStateChangedEventArgs e)
        {
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

        private void Listview_ScrollChanged(object sender, CustomForms.ScrollChangedEventArgs e)
        {
            double scrolly = e.NewScrollY * scaleparam;
            if (scrolly <= -100)
            {
                playlayout.IsVisible = true;
                playlayoutbg.IsVisible = true;
                Title.IsVisible = true;
                BackBG.IsVisible = true;

            }
            else if (scrolly > -100)
            {
                playlayout.IsVisible = false;
                playlayoutbg.IsVisible = false;
                Title.IsVisible = false;
                BackBG.IsVisible = false;
                lwheaderBG.Opacity = Math.Abs(scrolly) / 100;
            }
            if (scrolly >= -100 && scrolly <= -10)
            {
                lwheaderBG.TranslationY = scrolly;
            }
        }
    }
}
