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
        static int mpoint = 180;
        float[] pointdata = new float[mpoint];
        byte[] fftdata;
        bool isinit;
        static int cRadius = 400;
        SKRect mClipBounds;
        static double swidth;
        static double sheight;
        int mPointRadius;
        SKPaint p;
        public BlankPage()
        {
            InitializeComponent();
            swidth = Width;
            sheight = Height;
            DependencyService.Get<IAudioVisualizer>().Init();
            DependencyService.Get<IAudioVisualizer>().OnWaveformUpdate += BlankPage_OnWaveformUpadte;
            //DependencyService.Get<IAudioVisualizer>().OnFftUpdate += BlankPage_OnFftUpdate;
            isinit = true;
            mClipBounds = new SKRect();
            mPointRadius = Math.Abs((int)(2 * cRadius * Math.Sin(Math.PI / mpoint / 3)));
            p = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Red,
                StrokeWidth = 2 * mPointRadius,
                StrokeCap=SKStrokeCap.Round
            };
        }

        private void BlankPage_OnFftUpdate(IList<byte> args)
        {
            audiodata = args;
            canvasview.InvalidateSurface();
        }

        private void BlankPage_OnWaveformUpadte(System.Collections.Generic.IList<byte> args)
        {
            audiodata = args;
            pointdata = new float[mpoint];
            canvasview.InvalidateSurface();
        }
        SKPaint l = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Shader = SKShader.CreateLinearGradient(new SKPoint(cRadius, 0), new SKPoint(cRadius + 10 * 5, 0)
               ,new SKColor[] { SKColor.Parse("#77FF5722"), SKColor.Parse("#10FF5722") }, new float[] { 0, 1 }, SKShaderTileMode.Clamp)
        };
        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            if (!isinit)
                return;
            SKCanvas canvas = e.Surface.Canvas;
            int width = e.Info.Width;
            int height = e.Info.Height;
            canvas.Clear();
            canvas.Translate(width / 2, height / 2);
            for (int i = 0; i < 360; i = i + 360 / mpoint)
            {
                float cx = (float)(Math.Cos(i*Math.PI/180)* cRadius);
                float cy = (float)(- Math.Sin(i * Math.PI/180) * cRadius);
                canvas.DrawCircle(cx,cy, mPointRadius, p);

            }
            //WaveDataUpdate();

            FftDataVisualizer();
            if (fftdata == null)
                return; int step = (int)Math.Round((double)fftdata.Length / mpoint);
            int j = 0; ;
            for (int i = 0; i < 360; i = i + 360 / mpoint)
            {
                if (fftdata[j] < 0)
                    fftdata[j] = 127;
                var value = fftdata[j];
                var meterHeight = value * (500 - cRadius) / 256;
                float cx = (float)(Math.Cos(i * Math.PI / 180) * cRadius);
                float cy = (float)(-Math.Sin(i * Math.PI / 180) * cRadius);
                float cx1 = (float)(Math.Cos(i * Math.PI / 180) * (cRadius + meterHeight));
                float cy1 = (float)(-Math.Sin(i * Math.PI / 180) * (cRadius + meterHeight));
                canvas.DrawLine(cx, cy, cx1, cy1, p);
                j++;
            }
            //double angle = 0;
            //using (SKPath path = new SKPath())
            //{
            //    path.Rewind();
            //    for (int i = 0; i < mpoint; i++, angle += (360f / mpoint))
            //    {
            //        float posX = (float)((cRadius + pointdata[i]) * Math.Cos(RadianToDegree(angle)));
            //        float posY = (float)((cRadius + pointdata[i]) * Math.Sin(RadianToDegree(angle)));
            //        if (i == 0)
            //            path.MoveTo(posX, posY);
            //        else
            //            path.LineTo(posX, posY);
            //    }
            //    path.Close();
            //    canvas.DrawPath(path, p);
            //}


        }
        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
        void FftDataVisualizer()
        {
            if (audiodata == null)
                return;
            byte[] model = new byte[audiodata.Count / 2 + 1];
            model[0] = (byte)Math.Abs(audiodata[0]);
            for (int i = 2, j = 1; j < mpoint;)
            {
                model[j] = (byte)Math.Sqrt(audiodata[i]* audiodata[i]+audiodata[i + 1]* audiodata[i+1]);
                i += 2;
                j++;
            }
            fftdata = model;
        }
        void WaveDataUpdate()
        {
            if (audiodata != null)
            {
                if (audiodata.Count == 0) return;
                for (int i = 0; i < pointdata.Length; i++)
                {
                    int x = (int)Math.Ceiling((double)(i + 1) * (audiodata.Count / mpoint));
                    int t = 0;
                    if (x < 1024)
                    {
                        t = ((byte)(-Math.Abs(audiodata[x]) + 128)) * ((int)sheight / 4) / 128;
                    }
                    pointdata[i] = t;
                }
            }
        }
    }
}
