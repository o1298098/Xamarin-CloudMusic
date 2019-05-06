using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CloudMusic.Models.Media
{
    public class MVInfoModel
    {
        public string loadingPic { get; set; }
        public string bufferPic { get; set; }
        public string loadingPicFS { get; set; }
        public string bufferPicFS { get; set; }
        public bool subed { get; set; }
        public Data data { get; set; }
        public int code { get; set; }

    public class Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public int artistId { get; set; }
        public string artistName { get; set; }
        public string briefDesc { get; set; }
        public string desc { get; set; }
        public string cover { get; set; }
        public long coverId { get; set; }
        public int playCount { get; set; }
        public int subCount { get; set; }
        public int shareCount { get; set; }
        public int likeCount { get; set; }
        public int commentCount { get; set; }
        public int duration { get; set; }
        public int nType { get; set; }
        public string publishTime { get; set; }
        public Brs brs { get; set; }
        public Artist[] artists { get; set; }
        public string arstr => string.Join("/", artists.Select(r => r.name));
        public bool isReward { get; set; }
        public string commentThreadId { get; set; }
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

        public class Brs
        {
            [JsonProperty("240")]
            public string _240 { get; set; }
            [JsonProperty("480")]
            public string _480 { get; set; }
            [JsonProperty("720")]
            public string _720 { get; set; }
            [JsonProperty("1080")]
            public string _1080 { get; set; }
        }

        public class Artist
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    }

}
