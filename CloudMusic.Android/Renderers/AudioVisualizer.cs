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
namespace CloudMusic.Droid.Renderers
{
    public class AudioVisualizer : BaseVisualizer
    {
        private static readonly int BAR_MAX_POINTS = 240;
        private static readonly int BAR_MIN_POINTS = 30;
        private Rect mClipBounds;
        private int mPoints;
        private int mPointRadius;
        private float[] mSrcY;
        private int mRadius;
        private Paint mGPaint;
        private bool drawLine;
        public AudioVisualizer(Context context) : base(context)
        {

        }
        protected override void Init()
        {
            mPoints = (int)(BAR_MAX_POINTS * MDensity);
            if (mPoints < BAR_MIN_POINTS)
                mPoints = BAR_MIN_POINTS;
            mSrcY = new float[mPoints];
            mClipBounds = new Rect();
            SetAnimationSpeed(MAnimSpeed);
            MPaint.AntiAlias = true;
            mGPaint = new Paint();
            mGPaint.AntiAlias = true;
        }
        public bool isDrawLine()
        {
            return drawLine;
        }
        public void setDrawLine(bool drawLine)
        {
            this.drawLine = drawLine;
        }
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            mRadius = Math.Min(w, h) /3;
            mPointRadius = Math.Abs((int)(2 * mRadius * Math.Sin(Math.PI / mPoints / 3)));
            LinearGradient lg = new LinearGradient(Width / 2 + mRadius, Height / 2, Width / 2 + mRadius + mPointRadius * 5, Height / 2 , Color.ParseColor("#77FF5722"), Color.ParseColor("#10FF5722"), Shader.TileMode.Clamp);
            mGPaint.SetShader(lg);
        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.GetClipBounds(mClipBounds);
            if (IsVisualizationEnabled && MRawAudioBytes != null)
            {
                if (MRawAudioBytes.Count == 0) return;
                for (int i = 0; i < mSrcY.Length; i++)
                {
                    int x = (int)Math.Ceiling((double)((i + 1) * (MRawAudioBytes.Count / mPoints)));
                    int t = 0;
                    if (x < 1024)
                    {
                        t = ((byte)(-Math.Abs(MRawAudioBytes[x]) + 128)) * (Height / 8) / 128/5;
                    }
                    mSrcY[i] = t;
                }
            }
            // draw circle's points
            for (int i = 0; i < 360; i = i + 360 / mPoints)
            {
                float cx = (float)(Width / 2 + Math.Cos(i * Math.PI / 180) * mRadius);
                float cy = (float)(Height / 2 - Math.Sin(i * Math.PI / 180) * mRadius);
                canvas.DrawCircle(cx, cy, mPointRadius, MPaint);
            }
            // draw lines
            if (drawLine) drawLines(canvas);
            // draw bar
            for (int i = 0; i < 360; i = i + 360 / mPoints)
            {
                if (mSrcY[i * mPoints / 360] == 0) continue;
                canvas.Save();
                canvas.Rotate(-i, this.Width / 2, Height / 2);
                float cx = (float)(Width / 2 + mRadius);
                float cy = (float)(Height / 2);
                canvas.DrawRect(cx, cy - mPointRadius, cx + mSrcY[i * mPoints / 360],
                        cy + mPointRadius, MPaint);
                canvas.DrawCircle(cx + mSrcY[i * mPoints / 360], cy, mPointRadius, MPaint);
                canvas.Restore();
            }
        }
        private void drawLines(Canvas canvas)
        {
            int lineLen = 14 * mPointRadius;//default len,
            for (int i = 0; i < 360; i = i + 360 / mPoints)
            {
                canvas.Save();
                canvas.Rotate(-i, Width / 2, Height / 2);
                float cx = (float)(Width / 2 + mRadius) + mSrcY[i * mPoints / 360];
                float cy = (float)(Height / 2);
                Path path = new Path();
                path.MoveTo(cx, cy + mPointRadius);
                path.LineTo(cx, cy - mPointRadius);
                path.LineTo(cx + lineLen, cy);
                canvas.DrawPath(path, mGPaint);
                canvas.Restore();
            }
        }
    }
}