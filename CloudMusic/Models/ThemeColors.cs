using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.Models
{
   public class ThemeColors
   {
        public Color MainColor { get; set; }
        public Color TextColor { get; set; }
        public Color TitleColor { get; set; }
        public void SetColor(int mainrgb,int textrgb,int titlergb )
        {
            MainColor = Color.FromRgb((mainrgb>>16)&0x0ff, (mainrgb >> 8) & 0x0ff, mainrgb&0x0ff);
            TextColor = Color.FromRgb((textrgb >> 16) & 0x0ff, (textrgb >> 8) & 0x0ff, textrgb & 0x0ff);
            TitleColor =Color.FromRgb((titlergb >> 16) & 0x0ff, (titlergb >> 8) & 0x0ff, titlergb & 0x0ff);
        }
   }
}
