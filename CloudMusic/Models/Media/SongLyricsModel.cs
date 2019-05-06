using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.Models.Media
{
    public class SongLyricsModel: INotifyPropertyChanged
    {
        Color color;
        public SongLyricsModel()
       {
            Color = Xamarin.Forms.Color.FromHex("#c1c1c1");
        }
        public double Time { get; set; }
        public string LyricsText { get; set; }
        public string cnText { get; set; }
        public Color Color { get=> color; set{ color = value;OnPropertyChanged("Color"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
