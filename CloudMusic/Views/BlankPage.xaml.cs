using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;

namespace CloudMusic.Views
{
    public partial class BlankPage : ContentPage
    {
        Random random = new Random(DateTime.Now.Millisecond);
        public BlankPage()
        {
            InitializeComponent();
            //Device.StartTimer(TimeSpan.FromMilliseconds(1), ()=> { canvasview.InvalidateSurface();return true; });
        }
        SKPaint CirclePaint = new SKPaint
        {
            Color=Color.Orange.ToSKColor(),
            Style=SKPaintStyle.Fill
        };
        SKPaint linepaint = new SKPaint
        {
            StrokeWidth=20,
            Style=SKPaintStyle.Stroke,
            StrokeCap=SKStrokeCap.Round,

        };
        int i = 1;
        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            int width = e.Info.Width;
            int height = e.Info.Height;
            canvas.Clear();
            canvas.DrawColor(Color.SkyBlue.ToSKColor());
            float t = (DateTime.Now.Second % 2 + DateTime.Now.Millisecond / 1000f);
            using (SKPath path = new SKPath())
            {
                SKPoint point1 = new SKPoint(width / 2+100, height / 2+200);
                SKPoint point2 = new SKPoint(width / 2+100, height / 2+500);
                SKPoint point3 = new SKPoint(width / 2-150, height / 2+300);
                canvas.Save();
                path.MoveTo(width / 2, height / 2);
                path.CubicTo(point1, point2, point3);
                canvas.DrawPath(path,linepaint);
            }


        }
    }
}
