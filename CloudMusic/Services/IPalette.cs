using CloudMusic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.Services
{
    public interface IPalette
    {
        System.Threading.Tasks.Task<ThemeColors>GetColorAsync(ImageSource imageSource);
    }
}
