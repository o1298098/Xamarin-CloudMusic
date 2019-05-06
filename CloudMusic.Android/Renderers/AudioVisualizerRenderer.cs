using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using CloudMusic.CustomForms;
using CloudMusic.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ARelativeLayout = Android.Widget.RelativeLayout;

[assembly: ExportRenderer(typeof(AudioVisualizerView),typeof(AudioVisualizerRenderer))]
namespace CloudMusic.Droid.Renderers
{
    public class AudioVisualizerRenderer : ViewRenderer<AudioVisualizerView, ARelativeLayout>
    {
        ARelativeLayout relativeLayout;
        Com.Gauravk.Audiovisualizer.Base.BaseVisualizer Visualizer;
        Com.Chibde.Visualizer.CircleBarVisualizer CircleBarVisualizer;
        public AudioVisualizerRenderer(Context context) : base(context)
        {
           
        }
        protected override void OnElementChanged(ElementChangedEventArgs<AudioVisualizerView> e)
        {
            base.OnElementChanged(e);
            if (ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.RecordAudio) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Android.App.Application.Context as Activity, new String[] { Manifest.Permission.RecordAudio }, 0);
            }
            relativeLayout = new ARelativeLayout(Context);
            AudioVisualerInit();
            SetNativeControl(relativeLayout);
        }

        private void AudioVisualerInit()
        {
            if (Visualizer != null)
            {
                Visualizer.Release();
                relativeLayout.RemoveAllViews();
            }
            if (CircleBarVisualizer != null)
            {
                CircleBarVisualizer.Release();
                relativeLayout.RemoveAllViews();
            }
            switch (Element.AudioVisualerType)
            {
                case CustomForms.AudioVisualer.AudioVisualerType.CircleLine:
                    Visualizer = new Com.Gauravk.Audiovisualizer.Visualizer.CircleLineVisualizer(Context);
                    break;
                case CustomForms.AudioVisualer.AudioVisualerType.Hifi:
                    Visualizer = new Com.Gauravk.Audiovisualizer.Visualizer.HiFiVisualizer(Context);
                    break;
                case CustomForms.AudioVisualer.AudioVisualerType.Wave:
                    Visualizer = new Com.Gauravk.Audiovisualizer.Visualizer.WaveVisualizer(Context);
                    break;
                case CustomForms.AudioVisualer.AudioVisualerType.Blob:
                    Visualizer = new Com.Gauravk.Audiovisualizer.Visualizer.BlobVisualizer(Context);
                    break;
                case CustomForms.AudioVisualer.AudioVisualerType.Blast:
                    Visualizer = new Com.Gauravk.Audiovisualizer.Visualizer.BlastVisualizer(Context);
                    break;
                case CustomForms.AudioVisualer.AudioVisualerType.Bar:
                    Visualizer = new Com.Gauravk.Audiovisualizer.Visualizer.BarVisualizer(Context);
                    break;
                case CustomForms.AudioVisualer.AudioVisualerType.Chibde:
                    Visualizer = null;
                    CircleBarVisualizer = new Com.Chibde.Visualizer.CircleBarVisualizer(Context);
                    break;
                default:
                    Visualizer = new Com.Gauravk.Audiovisualizer.Visualizer.CircleLineVisualizer(Context);
                    break;
            }
            ARelativeLayout.LayoutParams layoutParams =
                       new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            layoutParams.AddRule(LayoutRules.CenterInParent);
            if (Visualizer != null)
            {
                Visualizer.SetAudioSessionId(0);
                Visualizer.SetAnimationSpeed(Com.Gauravk.Audiovisualizer.Model.AnimSpeed.Medium);
                Visualizer.SetDensity((float)Element.Density);
                Visualizer.SetColor(Element.Color.ToAndroid());
                Visualizer.LayoutParameters = layoutParams;
                relativeLayout.AddView(Visualizer);
            }
            else if (CircleBarVisualizer != null)
            {
                CircleBarVisualizer.SetColor(Element.Color.ToAndroid());
                CircleBarVisualizer.SetPlayer(0);
                CircleBarVisualizer.LayoutParameters = layoutParams;
                relativeLayout.AddView(CircleBarVisualizer);
            }
           
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //if (e.PropertyName == AudioVisualizerView.DensityProperty.PropertyName)
            //{
            //    AudioVisualerInit();
            //}
            if (e.PropertyName == AudioVisualizerView.ColorProperty.PropertyName)
            {
                Visualizer.SetColor(Element.Color.ToAndroid());
            }
            else if (e.PropertyName == AudioVisualizerView.AnimationSpeedProperty.PropertyName)
            {
                switch (Element.AnimationSpeed)
                {
                    case CustomForms.AudioVisualer.VisualizerAnimationSpeed.slow:
                        Visualizer.SetAnimationSpeed(Com.Gauravk.Audiovisualizer.Model.AnimSpeed.Slow);
                        break;
                    case CustomForms.AudioVisualer.VisualizerAnimationSpeed.medium:
                        Visualizer.SetAnimationSpeed(Com.Gauravk.Audiovisualizer.Model.AnimSpeed.Medium);
                        break;
                    case CustomForms.AudioVisualer.VisualizerAnimationSpeed.fast:
                        Visualizer.SetAnimationSpeed(Com.Gauravk.Audiovisualizer.Model.AnimSpeed.Fast);
                        break;
                }
            }
            else if (e.PropertyName == AudioVisualizerView.AudioVisualerTypeProperty.PropertyName)
            {
                AudioVisualerInit();
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Visualizer?.Release();
            CircleBarVisualizer?.Release();
        }

    }
}