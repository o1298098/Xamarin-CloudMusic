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
        static int mpoint = 100;
        float[] pointdata = new float[mpoint];
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
            DependencyService.Get<IAudioVisualizer>().OnWaveformUpadte += BlankPage_OnWaveformUpadte;
            isinit = true;
            mClipBounds = new SKRect();
            mPointRadius = Math.Abs((int)(2 * cRadius * Math.Sin(Math.PI / mpoint / 3)));
            p = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Red,
                StrokeWidth = 2 * mPointRadius,
            };
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
            if (audiodata != null)
            {
                if (audiodata.Count == 0) return;
                for (int i = 0; i < pointdata.Length; i++)
                {
                    int x = (int)Math.Ceiling((double)(i + 1) * (audiodata.Count / mpoint));
                    int t = 0;
                    if (x < 1024)
                    {
                        t = ((byte)(-Math.Abs(audiodata[x]) + 128)) * (height / 4) / 128;
                    }
                    if (t > 0 && t < 50)
                        t += 10;
                    else if (t > 150)
                        t = 150;
                    pointdata[i] = t;
                }
            }
            for (int i = 0; i < 360; i = i + 360 / mpoint)
            {
                float cx = (float)(width / 2+Math.Cos(i*Math.PI/180)* cRadius);
                float cy = (float)(height / 2 - Math.Sin(i * Math.PI/180) * cRadius);
                canvas.DrawCircle(cx,cy, mPointRadius, p);

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
            for (int i = 0; i < 360; i = i + 360 / mpoint)
            {
                if (pointdata[i * mpoint / 360] == 0) continue;
                
                canvas.RotateDegrees(-i, width / 2, height / 2);
                float cx = (float)(width / 2 + cRadius);
                float cy = (float)(height / 2);

                //canvas.DrawRect(cx, cy - mPointRadius, cx + pointdata[i * mpoint / 360],
                //        cy + mPointRadius, p);
                canvas.Save();
                canvas.DrawLine(cx,cy,cx + pointdata[i * mpoint / 360], cy, p);
                canvas.DrawCircle(cx + pointdata[i * mpoint / 360], cy, mPointRadius, p);
                canvas.Restore();
            }

        }
    }
}
