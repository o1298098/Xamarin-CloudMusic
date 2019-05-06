using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CloudMusic.Models.Media
{
   public class SiMiMVModle:BaseModel
    {
        ObservableCollection<Mv> _mvs;
            public ObservableCollection<Mv> mvs { get=>_mvs; set=>SetProperty(ref _mvs,value, "mvs"); }
            public int code { get; set; }

        public class Mv
        {
            public int id { get; set; }
            public string cover { get; set; }
            public string name { get; set; }
            public int playCount { get; set; }
            public object briefDesc { get; set; }
            public object desc { get; set; }
            public string artistName { get; set; }
            public int artistId { get; set; }
            public double duration { get; set; }
            public int mark { get; set; }
            public List<Artist> artists { get; set; }
            public string alg { get; set; }
            public TimeSpan durationtime { get => TimeSpan.FromMilliseconds(duration); }
            public string PlayCountstr {
                get
                {
                    if (playCount > 10000)
                        return (playCount / 10000) + "万";
                    else
                        return playCount.ToString();
                }
            }
        }

        public class Artist
        {
            public int id { get; set; }
            public string name { get; set; }
            public string[] alias { get; set; }
            public string[] transNames { get; set; }
        }

    }
}

