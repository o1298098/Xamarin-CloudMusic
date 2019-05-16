using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudMusic.CustomForms;
using CloudMusic.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace CloudMusic.iOS.Renderers
{
    public class CustomTabbedPageRenderer:TabbedRenderer
    {
    }
}