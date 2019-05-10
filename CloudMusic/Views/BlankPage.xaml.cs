using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Threading.Tasks;
using CloudMusic.Services;
using System.Collections.Generic;

namespace CloudMusic.Views
{
    public partial class BlankPage : ContentPage
    {
        Random random = new Random(DateTime.Now.Millisecond);
        IList<byte> audiodata;
        float[] pointdata = new float[100]; 
        public BlankPage()
        {
            InitializeComponent();
            stratBtn.Clicked += StratBtn_Clicked;
        }

        private void StratBtn_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IAudioVisualizer>().Init();
            DependencyService.Get<IAudioVisualizer>().OnWaveformUpadte += BlankPage_OnWaveformUpadte;
        }

        private void BlankPage_OnWaveformUpadte(System.Collections.Generic.IList<byte> args)
        {
            audiodata = args;
            if (audiodata != null)
            {
                if (audiodata.Count == 0) return;
                for (int i = 0; i < pointdata.Length; i++)
                {
                    int x = (int)Math.Ceiling((double)(i + 1) * (audiodata.Count / 100));
                    int t = 0;
                    if (x < 1024)
                    {
                        t = ((byte)(Math.Abs(audiodata[x]) + 128)) * 100 / 128;
                    }
                    pointdata[i] = -t;
                }
            }
            canvasview.InvalidateSurface();
        }
        SKPaint p = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color=SKColors.Red,
        };

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            int width = e.Info.Width;
            int height = e.Info.Height;
            for (int i = 0; i < 360; i = 1 + 360 / 100)
            {
                float cx = (float)(width / 2+Math.Sin(i*Math.PI/180)*100);
                float cy = (float)(height / 2 - Math.Cos(i * Math.PI/180) * 100);
                canvas.DrawCircle(cx,cy,2,p);

            }
            for (int i = 0; i < 360; i = 1 + 360 / 100)
            {
                float cx = (float)(width / 2 + Math.Sin(i * Math.PI / 180) * 100);
                float cy = (float)(height / 2 - Math.Cos(i * Math.PI / 180) * 100);
                canvas.Save();
                canvas.DrawCircle(cx, cy, 2, p);
            }

        }
    }
}
