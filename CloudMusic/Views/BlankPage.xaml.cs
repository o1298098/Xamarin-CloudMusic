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
            Device.StartTimer(TimeSpan.FromSeconds(1f/60), ()=> { canvasview.InvalidateSurface();return true; });
        }
        SKPaint CirclePaint = new SKPaint
        {
            Color=Color.Accent.ToSKColor(),
            Style=SKPaintStyle.Stroke
        };
        SKPaint linepaint = new SKPaint
        {
            StrokeWidth=20,
            Style=SKPaintStyle.Stroke,
            StrokeCap=SKStrokeCap.Round,

        };
        SKPaint eyepaint = new SKPaint
        {
            Style = SKPaintStyle.Fill
        };
        int i = 1;
        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            int width = e.Info.Width;
            int height = e.Info.Height;
            canvas.Clear();
            //canvas.DrawColor(Color.SkyBlue.ToSKColor());
            float t = (float)Math.Sin((DateTime.Now.Second % 2 + DateTime.Now.Millisecond / 1000f)*Math.PI);
            /*using (SKPath path = new SKPath())
            {
                SKPoint point1 = new SKPoint(width / 2+50*t, height / 2+200);
                SKPoint point2 = new SKPoint(width / 2, height / 2 + 250 - Math.Abs(50*t));
                SKPoint point3 = new SKPoint(width / 2-150*t, height / 2 + 250 - Math.Abs(75 * t));
                canvas.Save();
                path.MoveTo(width / 2, height / 2);
                path.CubicTo(point1, point2, point3);
                canvas.DrawPath(path,linepaint);
                canvas.Restore();
            }*/
            canvas.Save();
            canvas.DrawCircle(width / 2f, height / 2f, 100, CirclePaint);
            canvas.DrawCircle(width / 2f - 40, height / 2f - 30, 10, eyepaint);
            canvas.DrawCircle(width / 2f + 40, height / 2f - 30, 10, eyepaint);
            canvas.Restore();
            using (SKPath path = new SKPath())
            {
                SKPoint point1 = new SKPoint(width / 2 + 50 * t, height / 2 + 200);
                SKPoint point2 = new SKPoint(width / 2, height / 2 + 250 - Math.Abs(50 * t));
                SKPoint point3 = new SKPoint(width / 2 - 50 * t, height / 2 + 250 - Math.Abs(75 * t));
                canvas.Save();
                path.MoveTo(width / 2, height / 2 + 30);
                path.CubicTo(point1, point2, point3);
                canvas.DrawPath(path, linepaint);
                canvas.Restore();
            }
        }
    }
}
