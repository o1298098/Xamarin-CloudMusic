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
        static int mpoint = 120;
        float[] pointdata = new float[mpoint];
        float[] Changedfft;
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
            //DependencyService.Get<IAudioVisualizer>().OnWaveformUpdate += BlankPage_OnWaveformUpadte;
            DependencyService.Get<IAudioVisualizer>().OnFftUpadate += BlankPage_OnFftUpadate;

            isinit = true;
            mClipBounds = new SKRect();
            mPointRadius = Math.Abs((int)(2 * cRadius * Math.Sin(Math.PI / mpoint / 3)));
            p = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Red,
                StrokeWidth = 2 * mPointRadius,
            };
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                p.Color = Color.FromRgb(random.Next(255), random.Next(255), random.Next(255)).ToSKColor();
                return true;
            });
        }

        private void BlankPage_OnFftUpadate(IList<byte> args)
        {
            audiodata = args;
            //pointdata = new float[mpoint];
            canvasview.InvalidateSurface();
        }

        private void BlankPage_OnWaveformUpadte(System.Collections.Generic.IList<byte> args)
        {
            audiodata = args;
            //pointdata = new float[mpoint];
            canvasview.InvalidateSurface();
        }
        SKPaint l = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Shader = SKShader.CreateLinearGradient(new SKPoint((float)swidth / 2 + cRadius, (float)sheight / 2), new SKPoint((float)swidth / 2 + cRadius + 10 * 5, (float)sheight / 2)
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
            for (int i = 0; i < 360; i = i + 360 / mpoint)
            {
                float cx = (float)(width / 2 + Math.Cos(i * Math.PI / 180) * cRadius);
                float cy = (float)(height / 2 - Math.Sin(i * Math.PI / 180) * cRadius);
                canvas.DrawCircle(cx, cy, mPointRadius, p);

            }
            //int lineLen = 14 * mPointRadius;
            //for (int i = 0; i < 360; i = i + 360 / mpoint)
            //{
            //    canvas.Save();
            //    canvas.RotateDegrees(-i, width / 2, height / 2);
            //    float cx = (float)(width / 2 + cRadius) + pointdata[i * mpoint / 360];
            //    float cy = (float)(height / 2);
            //    using (SKPath path = new SKPath())
            //    {
            //        path.MoveTo(cx, cy + mPointRadius);
            //        path.LineTo(cx, cy - mPointRadius);
            //        path.LineTo(cx + lineLen, cy);
            //        canvas.DrawPath(path,l);
            //        canvas.Restore();
            //    };

            //}
            if (audiodata == null)
                return;
            var step =(int)Math.Floor((double)audiodata.Count / mpoint);
            int j = 0;
            for (int i = 0; i < 360; i = i + 360 / mpoint)
            {
                //if (pointdata[i * mpoint / 360] == 0) continue;

                canvas.RotateDegrees(-i, width / 2, height / 2);
                float cx = (float)(width / 2 + cRadius);
                float cy = (float)(height / 2);
                //canvas.DrawRect(cx, cy - mPointRadius, cx + pointdata[i * mpoint / 360],
                //        cy + mPointRadius, p);
                canvas.Save();
                canvas.DrawLine(cx, cy, cx + audiodata[j*step], cy, p);
                canvas.DrawCircle(cx + audiodata[j * step], cy, mPointRadius, p);
                canvas.Restore();
                j ++;
            }


            /*if (audiodata == null)
            {
                return;
            }
            if (pointdata == null || pointdata?.Length < audiodata.Count * 4)
            {
                pointdata = new float[audiodata.Count * 4];
            }
            for (int i = 0; i < audiodata.Count - 1; i++)
            {
                pointdata[i * 4] = width * i / (audiodata.Count - 1);
                pointdata[i * 4 + 1] =0
                        + ((byte)(audiodata[i] + 128)) * (height / 4) / 128;
                pointdata[i * 4 + 2] = width * (i + 1) / (audiodata.Count - 1);
                pointdata[i * 4 + 3] = 0
                        + ((byte)(audiodata[i + 1] + 128)) * (height / 4) / 128;
            }
            for (int i=0;i< audiodata.Count - 4;i++)
                canvas.DrawLine(pointdata[i * 4], pointdata[i * 4 + 1], pointdata[i * 4 + 2], pointdata[i * 4 + 3], p);*/

        }
        void FftDataUpdate()
        {
            if (audiodata != null)
            {
                int fftcount = audiodata.Count / 2 + 1;
                Changedfft = new float[fftcount];
                Changedfft[0] = (byte)Math.Abs(audiodata[0]);
                for (int i = 1; i < fftcount - 1; i++)
                {
                    Changedfft[i] = (byte)Math.Sqrt(audiodata[2 * i] * audiodata[2 * i] + audiodata[2 * i + 1] * audiodata[2 * i + 1]);
                }
            }
            if (Changedfft != null)
            {
                if (Changedfft.Length == 0) return;
                for (int i = 0; i < pointdata.Length; i++)
                {
                    int x = (int)Math.Ceiling((double)(i + 1) * (Changedfft.Length / mpoint));
                    float t = 0;
                    if (x < Changedfft.Length)
                    {
                        t = Changedfft[x];
                    }
                    pointdata[i] = t;
                }
            }
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
                        t = ((byte)(-Math.Abs(audiodata[x]) + 128)) * (int)(sheight / 4) / 128;
                    }
                    pointdata[i] = t;
                }
            }
        }
    }
}
