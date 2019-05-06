using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CloudMusic.CustomForms;
using Xamarin.Forms;
using CloudMusic.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Text;

[assembly: ExportRenderer(typeof(CustomEditer), typeof(CustomEditerRenderer))]
namespace CloudMusic.Droid.Renderers
{
    public class CustomEditerRenderer : EditorRenderer
    {
        public CustomEditerRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.Background = gd; 
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}