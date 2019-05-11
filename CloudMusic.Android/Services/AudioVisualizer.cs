using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Media.Audiofx;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Com.Gauravk.Audiovisualizer.Base;
using Android.Support.Annotation;
using CloudMusic.Droid.Services;
using CloudMusic.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioVisualizer))]
namespace CloudMusic.Droid.Services
{
    public class AudioVisualizer :Java.Lang.Object, IAudioVisualizer, Visualizer.IOnDataCaptureListener
    {
        public event DataCaptureUpadteEvent OnWaveformUpdate;
        public event DataCaptureUpadteEvent OnFftUpadate;

        IList<byte> audiobytes;
        static Visualizer mVisualizer;
        bool isinit;
         void IAudioVisualizer.Init()
        {
            if (!isinit)
            {
                mVisualizer = new Visualizer(0);
                mVisualizer.SetCaptureSize(Visualizer.GetCaptureSizeRange()[1]);
                mVisualizer.SetDataCaptureListener(this, Visualizer.MaxCaptureRate / 2, true, true);
                mVisualizer.SetEnabled(true);
                isinit = true;
            }
        }

        public IList<byte> GetWaveformValue()
        {
            return audiobytes;
        }

        public void WaveformUpadt(IList<byte> args)
        {
        }

        public void OnFftDataCapture(Visualizer visualizer, byte[] fft, int samplingRate)
        {
            OnFftUpadate?.Invoke(fft);
        }

        public void OnWaveFormDataCapture(Visualizer visualizer, byte[] waveform, int samplingRate)
        {
            OnWaveformUpdate?.Invoke(waveform);
            audiobytes = waveform;
        }

        public new void Dispose()
        {
            mVisualizer?.Release();
            isinit = false;
        }


    }
}