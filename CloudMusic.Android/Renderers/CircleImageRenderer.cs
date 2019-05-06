using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CloudMusic.CustomForms;
using CloudMusic.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CircleImage), typeof(CircleImageRenderer))]
namespace CloudMusic.Droid
{

    public class CircleImageRenderer : ImageRenderer
    {
        public CircleImageRenderer(Context context) : base(context)
        {

        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {

            base.OnElementChanged(e);          
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Image.IsLoadingProperty.PropertyName && !this.Element.IsLoading
                && this.Control.Drawable != null && this.Element.Aspect != Aspect.AspectFit)
            {
                using (var sourceBitmap = Bitmap.CreateBitmap(this.Control.Drawable.IntrinsicWidth, this.Control.Drawable.IntrinsicHeight, Bitmap.Config.Argb8888))
                using (var canvas = new Canvas(sourceBitmap))
                {
                    this.Control.Drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
                    this.Control.Drawable.Draw(canvas);
                    this.ReshapeImage(sourceBitmap);
                }
            }
        }
        protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            if (this.Element.Aspect != Aspect.AspectFit)
            {
                return base.DrawChild(canvas, child, drawingTime);
            }

            using (var path = new Path())
            {
                path.AddCircle(Width / 2, Height / 2, (Math.Min(Width, Height) - 10) / 2, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
        private void ReshapeImage(Bitmap sourceBitmap)
        {
            if (sourceBitmap == null) return;

            using (var sourceRect = GetScaledRect(sourceBitmap.Height, sourceBitmap.Width))
            using (var rect = this.GetTargetRect(sourceBitmap.Height, sourceBitmap.Width))
            using (var output = Bitmap.CreateBitmap(rect.Width(), rect.Height(), Bitmap.Config.Argb8888))
            using (var canvas = new Canvas(output))
            using (var paint = new Paint())
            using (var rectF = new RectF(rect))
            {
                var roundRx = rect.Width() / 2;
                var roundRy = rect.Height() / 2;

                paint.AntiAlias = true;
                canvas.DrawARGB(0, 0, 0, 0);
                paint.Color = Android.Graphics.Color.ParseColor("#ff424242");
                canvas.DrawRoundRect(rectF, roundRx, roundRy, paint);

                paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
                canvas.DrawBitmap(sourceBitmap, sourceRect, rect, paint);

                //this.DrawBorder(canvas, rect.Width(), rect.Height());

                using (var drawable = this.Control.Drawable) // don't remove, this is making sure memory isn't leaked
                {
                    this.Control.SetImageBitmap(output);
                }

                // Forces the internal method of InvalidateMeasure to be called.
                this.Element.WidthRequest = this.Element.WidthRequest;
            }
        }
        private Rect GetScaledRect(int sourceHeight, int sourceWidth)
        {
            int height = 0;
            int width = 0;
            int top = 0;
            int left = 0;

            switch (this.Element.Aspect)
            {
                case Aspect.AspectFill:
                    height = sourceHeight;
                    width = sourceWidth;
                    height = MakeSquare(height, ref width);
                    left = (int)((sourceWidth - width) / 2);
                    top = (int)((sourceHeight - height) / 2);
                    break;
                case Aspect.Fill:
                    height = sourceHeight;
                    width = sourceWidth;
                    break;
                case Aspect.AspectFit:
                    height = sourceHeight;
                    width = sourceWidth;
                    height = MakeSquare(height, ref width);
                    left = (int)((sourceWidth - width) / 2);
                    top = (int)((sourceHeight - height) / 2);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new Rect(left, top, width + left, height + top);
        }
        private static int MakeSquare(int height, ref int width)
        {
            if (height < width)
            {
                width = height;
            }
            else
            {
                height = width;
            }
            return height;
        }

        /// <summary>
        /// Gets the target rect.
        /// </summary>
        /// <param name="sourceHeight">Height of the source.</param>
        /// <param name="sourceWidth">Width of the source.</param>
        /// <returns>Rect.</returns>
        private Rect GetTargetRect(int sourceHeight, int sourceWidth)
        {
            var height = this.Element.HeightRequest > 0
                ? (int)Math.Round(this.Element.HeightRequest, 0)
                : sourceHeight;
            var width = this.Element.WidthRequest > 0
                ? (int)Math.Round(this.Element.WidthRequest, 0)
                : sourceWidth;

            // Make Square
            height = MakeSquare(height, ref width);

            return new Rect(0, 0, width, height);
        }
    }
}
