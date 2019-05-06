using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using FFImageLoading.Forms;

namespace CloudMusic.Models
{
   public static class ColorMatrix
    {
        public static float[][] redColorMatrix =Color.Red.ColorToMatrix();
        public static float[][] GrayColorMatrix=Xamarin.Forms.Color.FromHex("#dbdbdb").ColorToMatrix();
        public static float[][] whiteColorMatrix = Color.White.ColorToMatrix();
    }
}
