using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.CustomForms
{
    public class BlurredImage : Image
    {
        public event LoadFinshedHandler OnLoadFinshed;
        BindableProperty EnableFadeProperty = BindableProperty.Create("EnableFade", typeof(bool), typeof(BlurredImage), false);
        BindableProperty IsLoadFinshProperty = BindableProperty.Create("IsLoadFinsh", typeof(bool), typeof(BlurredImage), false);
        public bool IsLoadFinsh
        {
            get
            {
                return (bool)base.GetValue(IsLoadFinshProperty);
            }
          private set
            {
                SetValue(IsLoadFinshProperty, value);
            }
        }
        public bool EnableFade
        {
            get
            {
                return (bool)base.GetValue(EnableFadeProperty);
            }
            set
            {
                SetValue(EnableFadeProperty, value);
            }
        }
        BindableProperty FadeDurationProperty = BindableProperty.Create("FadeDuration", typeof(int), typeof(BlurredImage), 600);
        public void SetLoadFinsh(bool state)
        {
            IsLoadFinsh = state;
            OnLoadFinshed?.Invoke();
        }
        public int FadeDuration
        {
            get
            {
                return (int)base.GetValue(FadeDurationProperty);
            }
            set
            {
                SetValue(FadeDurationProperty, value);
            }
        }
        public delegate void LoadFinshedHandler();
    }
}
