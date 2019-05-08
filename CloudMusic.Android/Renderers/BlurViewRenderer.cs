using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CloudMusic.CustomForms;
using CBlurView= Com.EightbitLab.BlurViewBinding.BlurView;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CloudMusic.Droid.Renderers;

[assembly: ExportRenderer(typeof(BlurView),
                          typeof(BlurViewRenderer))]
namespace CloudMusic.Droid.Renderers
{
    public class BlurViewRenderer: ViewRenderer<BlurView, FrameLayout>
    {
        CBlurView blurView;

        public BlurViewRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<BlurView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;
            blurView = new CBlurView(Context);
            var activity = Context as Activity;
            var rootView = (ViewGroup)activity.Window.DecorView.FindViewById(Android.Resource.Id.Content);
            var windowBackground = activity.Window.DecorView.Background;
            blurView.SetupWith(rootView)
                    .WindowBackground(windowBackground)
                    .BlurAlgorithm(new Com.EightbitLab.BlurViewBinding.RenderScriptBlur(Context))
                    .SetOverlayColor(e.NewElement.BlurColor.ToAndroid())
                    .BlurRadius(e.NewElement.Radius)
                    .SetHasFixedTransformationMatrix(e.NewElement.HasFixedTransformationMatrix)
                    .SetBlurAutoUpdate(true)
                    .SetBlurEnabled(true);
            SetNativeControl(blurView);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var c = (BlurView)sender;
            if (e.PropertyName == BlurView.RadiusProperty.PropertyName)
            {
                blurView.BlurRadius(c.Radius);
            }
            else if (e.PropertyName == BlurView.BlurColorProperty.PropertyName)
            {
                blurView.SetOverlayColor(c.BlurColor.ToAndroid());
            }
            else if (e.PropertyName == BlurView.HasFixedTransformationMatrixProperty.PropertyName)
            {
                blurView.SetHasFixedTransformationMatrix(c.HasFixedTransformationMatrix);
            }
        }
    }
}