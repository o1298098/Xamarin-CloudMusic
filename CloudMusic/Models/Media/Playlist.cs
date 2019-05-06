using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class Playlist
    {
        public long id { get; set; }
        public string name { get; set; }
        public string coverImgUrl { get; set; }
        public Creator creator { get; set; }
        public bool subscribed { get; set; }
        public int trackCount { get; set; }
        public long userId { get; set; }
        public int playCount { get; set; }
        public int bookCount { get; set; }
        public string description { get; set; }
        public bool highQuality { get; set; }
        public string alg { get; set; }
        public object[] subscribers { get; set; }
        public List<Track> tracks { get; set; }
        public List<Trackid> trackIds { get; set; }
        public object[] tags { get; set; }
        public long createTime { get; set; }
        public int status { get; set; }
        public long coverImgId { get; set; }
        public int specialType { get; set; }
        public long updateTime { get; set; }
        public string commentThreadId { get; set; }
        public bool ordered { get; set; }
        public int subscribedCount { get; set; }
        public int privacy { get; set; }
        public bool newImported { get; set; }
        public long trackUpdateTime { get; set; }
        public int cloudTrackCount { get; set; }
        public int adType { get; set; }
        public long trackNumberUpdateTime { get; set; }
        public int shareCount { get; set; }
        public string coverImgId_str { get; set; }
        public int commentCount { get; set; }
        public string playCountstr
        {
            get
            {
                if (playCount > 10000)
                    return (playCount / 10000) + "万";
                else
                    return playCount.ToString();
            }
        }

        public object artists { get; set; }
        public int totalDuration { get; set; }
    }

   


}
