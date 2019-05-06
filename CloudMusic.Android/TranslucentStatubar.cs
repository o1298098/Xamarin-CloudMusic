using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CloudMusic.Droid
{   public class TranslucentStatubar
    {
        public static void immersive(Window window, int color, float alpha)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            { // 21
                window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.SetStatusBarColor(new Android.Graphics.Color(mixtureColor(color, alpha)));

                SystemUiFlags systemUiVisibility = (SystemUiFlags)window.DecorView.SystemUiVisibility;
                systemUiVisibility |= SystemUiFlags.LayoutFullscreen;
                systemUiVisibility |= SystemUiFlags.LayoutStable;
                window.DecorView.SystemUiVisibility = (StatusBarVisibility)systemUiVisibility;
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            { // 19
                window.AddFlags(WindowManagerFlags.TranslucentStatus);
                setTranslucentView((ViewGroup)window.DecorView, color, alpha);
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
            { // 16
                SystemUiFlags systemUiVisibility = (SystemUiFlags)window.DecorView.SystemUiVisibility;
                systemUiVisibility |= SystemUiFlags.LayoutFullscreen;
                systemUiVisibility |= SystemUiFlags.LayoutStable;
                window.DecorView.SystemUiVisibility = (StatusBarVisibility)systemUiVisibility;
            }
        }

        public static int mixtureColor(int color, float alpha)
        {
            int a = (color & 0x000000) == 0 ? 0xff : color >> 24;
            return (color & 0xffffff) | (((int)(a * alpha)) << 24);
        }

        /** create a fake transparent bar*/
        public static void setTranslucentView(ViewGroup container, int color, float alpha)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            { // 19
                int _mixtureColor = mixtureColor(color, alpha);
                View translucentView = container.FindViewById(Android.Resource.Id.Custom);
                if (translucentView == null && _mixtureColor != 0)
                {
                    translucentView = new View(container.Context);
                    translucentView.Id = Android.Resource.Id.Custom;
                    ViewGroup.LayoutParams lp = new ViewGroup.LayoutParams(
                            ViewGroup.LayoutParams.MatchParent, getStatusBarHeight(container.Context));
                    container.AddView(translucentView, lp);
                }
                if (translucentView != null)
                {
                    translucentView.SetBackgroundColor(new Android.Graphics.Color(_mixtureColor));
                }
            }
        }

        /** get the height of statusbar */
        public static int getStatusBarHeight(Context context)
        {
            int result = 24;
            int resId = context.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resId > 0)
            {
                result = context.Resources.GetDimensionPixelSize(resId);
            }
            else
            {
                result = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip,
                        result, Android.Content.Res.Resources.System.DisplayMetrics);
            }
            return result;
        }
    }
}