using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Graphics;
using Android.Views;
using Android.Widget;
using CloudMusic.Droid.Services;
using CloudMusic.Models;
using CloudMusic.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Support.V7.Graphics.Palette;

[assembly: Xamarin.Forms.Dependency(typeof(CPalette))]

namespace CloudMusic.Droid.Services
{
    public class CPalette:IPalette
    {
        public async System.Threading.Tasks.Task<ThemeColors> GetColorAsync(ImageSource imageSource)
        {
            IImageSourceHandler handler;

            ThemeColors themeColors = new ThemeColors();
            if (imageSource is FileImageSource)
            {
                handler = new FileImageSourceHandler();
            }
            else if (imageSource is StreamImageSource)
            {
                handler = new StreamImagesourceHandler(); // sic
            }
            else if (imageSource is UriImageSource)
            {
                handler = new ImageLoaderSourceHandler(); // sic
            }
            else
            {
                return themeColors;
            }
            var bitmap = await handler.LoadImageAsync(imageSource, Android.App.Application.Context);
            if (bitmap == null)
                return themeColors;
            Palette.Builder builder = Palette.From(bitmap);
            Palette palette= builder.Generate();
            Swatch swatch =null;
            if (palette.VibrantSwatch != null)
                swatch = palette.VibrantSwatch;
            else if (palette.LightVibrantSwatch != null)
                swatch = palette.LightVibrantSwatch;
            else if (palette.LightMutedSwatch != null)
                swatch = palette.LightMutedSwatch;
            else if (palette.DarkVibrantSwatch != null)
                swatch = palette.DarkVibrantSwatch;
            else if (palette.DarkMutedSwatch != null)
                swatch = palette.DarkMutedSwatch;
            int bgcolor=0,textcolor=0,titltcolor=0;
            if (swatch != null)
            {
                bgcolor = swatch.Rgb;
                textcolor = swatch.BodyTextColor;
                titltcolor = swatch.TitleTextColor;
            }
            themeColors.SetColor(bgcolor,textcolor, titltcolor);
            return themeColors;
        }
    }
}