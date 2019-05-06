using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class Video
    {
        public string coverUrl { get; set; }
        public string title { get; set; }
        public int durationms { get; set; }
        public int playTime { get; set; }
        public int type { get; set; }
        public Creator creator { get; set; }
        public object aliaName { get; set; }
        public object transName { get; set; }
        public string vid { get; set; }
        public int?[] markTypes { get; set; }
        public string alg { get; set; }
        public int duration { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int size { get; set; }
        public int state { get; set; }
        public int coverType { get; set; }
        public object urlInfo { get; set; }
        public string videoId { get; set; }
        public object threadId { get; set; }
        public string description { get; set; }
        public int videoStatus { get; set; }
        public Resolution[] resolutions { get; set; }

        public string playTimeStr { get => GetPlayCountStr.PlayCountstr(playTime); }
        public TimeSpan durationmsStr { get => TimeSpan.FromMilliseconds(durationms); }
    }
    public class Resolution
    {
        public int resolution { get; set; }
        public int size { get; set; }
    }
}
