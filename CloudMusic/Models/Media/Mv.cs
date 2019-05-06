using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class Mv:BaseModel
    {
        public int id { get; set; }
        public string cover { get; set; }
        public string name { get; set; }
        public int playCount { get; set; }
        public string briefDesc { get; set; }
        public object desc { get; set; }
        public string artistName { get; set; }
        public int artistId { get; set; }
        public int duration { get; set; }
        public int mark { get; set; }
        public string arTransName { get; set; }
        public ObservableCollection<Artist> artists { get; set; }
        public object transNames { get; set; }
        public object alias { get; set; }
        public string alg { get; set; }
        public TimeSpan durationtime { get => TimeSpan.FromMilliseconds(duration); }
        public string PlayCountstr
        {
            get
            {
                if (playCount > 10000)
                    return (playCount / 10000) + "万";
                else
                    return playCount.ToString();
            }
        }
    }
}
