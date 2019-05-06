using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
   public class Personalized
    {
            public bool hasTaste { get; set; }
            public int code { get; set; }
            public int category { get; set; }
            public List<Result> result { get; set; }

        public class Result
        {
            public long id { get; set; }
            public int type { get; set; }
            public string name { get; set; }
            public string copywriter { get; set; }
            public string picUrl { get; set; }
            public bool canDislike { get; set; }
            public float playCount { get; set; }
            public int trackCount { get; set; }
            public bool highQuality { get; set; }
            public string alg { get; set; }
            public string playCountstr { get => GetPlayCountStr.PlayCountstr((int)playCount); }
        }

    }
}
