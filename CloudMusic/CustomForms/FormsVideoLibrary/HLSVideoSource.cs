using FormsVideoLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FormsVideoLibrary
{
    public class HLSVideoSource : VideoSource
    {
        public static readonly BindableProperty UriProperty =
            BindableProperty.Create(nameof(Uri), typeof(string), typeof(UriVideoSource));

        public string Uri
        {
            set { SetValue(UriProperty, value); }
            get { return (string)GetValue(UriProperty); }
        }
    }
}
