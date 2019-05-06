using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
   public class RecommendSongs
    {
            public int code { get; set; }
            public List<Recommend> recommend { get; set; }

        public class Recommend
        {
            public string name { get; set; }
            public long id { get; set; }
            public int position { get; set; }
            public object[] alias { get; set; }
            public int status { get; set; }
            public int fee { get; set; }
            public long copyrightId { get; set; }
            public string disc { get; set; }
            public long no { get; set; }
            public Artist[] artists { get; set; }
            public Album album { get; set; }
            public bool starred { get; set; }
            public int popularity { get; set; }
            public int score { get; set; }
            public int starredNum { get; set; }
            public int duration { get; set; }
            public int playedNum { get; set; }
            public int dayPlays { get; set; }
            public int hearTime { get; set; }
            public string ringtone { get; set; }
            public string crbt { get; set; }
            public object audition { get; set; }
            public string copyFrom { get; set; }
            public string commentThreadId { get; set; }
            public object rtUrl { get; set; }
            public int ftype { get; set; }
            public object[] rtUrls { get; set; }
            public int copyright { get; set; }
            public object transName { get; set; }
            public object sign { get; set; }
            public Hmusic hMusic { get; set; }
            public Mmusic mMusic { get; set; }
            public Lmusic lMusic { get; set; }
            public Bmusic bMusic { get; set; }
            public object mp3Url { get; set; }
            public long mvid { get; set; }
            public int rtype { get; set; }
            public object rurl { get; set; }
            public string reason { get; set; }
            public Privilege privilege { get; set; }
            public string alg { get; set; }
            public bool hasMv { get => mvid > 0; }
        }

      

        public class Hmusic
        {
            public string name { get; set; }
            public long id { get; set; }
            public int size { get; set; }
            public string extension { get; set; }
            public int sr { get; set; }
            public long dfsId { get; set; }
            public int bitrate { get; set; }
            public int playTime { get; set; }
            public float volumeDelta { get; set; }
            public object dfsId_str { get; set; }
        }

        public class Mmusic
        {
            public string name { get; set; }
            public long id { get; set; }
            public int size { get; set; }
            public string extension { get; set; }
            public int sr { get; set; }
            public int dfsId { get; set; }
            public int bitrate { get; set; }
            public int playTime { get; set; }
            public float volumeDelta { get; set; }
            public object dfsId_str { get; set; }
        }

        public class Lmusic
        {
            public string name { get; set; }
            public long id { get; set; }
            public int size { get; set; }
            public string extension { get; set; }
            public int sr { get; set; }
            public int dfsId { get; set; }
            public int bitrate { get; set; }
            public int playTime { get; set; }
            public float volumeDelta { get; set; }
            public object dfsId_str { get; set; }
        }

        public class Bmusic
        {
            public string name { get; set; }
            public long id { get; set; }
            public int size { get; set; }
            public string extension { get; set; }
            public int sr { get; set; }
            public int dfsId { get; set; }
            public int bitrate { get; set; }
            public int playTime { get; set; }
            public float volumeDelta { get; set; }
            public object dfsId_str { get; set; }
        }

        public class Privilege
        {
            public long id { get; set; }
            public int fee { get; set; }
            public int payed { get; set; }
            public int st { get; set; }
            public int pl { get; set; }
            public int dl { get; set; }
            public int sp { get; set; }
            public int cp { get; set; }
            public int subp { get; set; }
            public bool cs { get; set; }
            public int maxbr { get; set; }
            public int fl { get; set; }
            public bool toast { get; set; }
            public int flag { get; set; }
            public bool preSell { get; set; }
        }

      

    }
}
