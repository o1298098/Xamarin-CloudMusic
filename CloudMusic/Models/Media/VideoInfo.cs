using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class VideoInfo
    {
        public string msg { get; set; }
        public long code { get; set; }
        public bool hasmore { get; set; }
        public ObservableCollection<Datas> datas { get; set; }
        public int rcmdLimit { get; set; }


        public class Datas
        {
            public long type { get; set; }
            public bool displayed { get; set; }
            public string alg { get; set; }
            public object extAlg { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string alg { get; set; }
            public string threadId { get; set; }
            public string coverUrl { get; set; }
            public int height { get; set; }
            public int width { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public int commentCount { get; set; }
            public int shareCount { get; set; }
            public Resolution[] resolutions { get; set; }
            public Creator creator { get; set; }
            public Urlinfo urlInfo { get; set; }
            public Videogroup[] videoGroup { get; set; }
            public object previewUrl { get; set; }
            public int previewDurationms { get; set; }
            public bool hasRelatedGameAd { get; set; }
            public int[] markTypes { get; set; }
            public Relatesong[] relateSong { get; set; }
            public object relatedInfo { get; set; }
            public object videoUserLiveInfo { get; set; }
            public string vid { get; set; }
            public long durationms { get; set; }
            public int playTime { get; set; }
            public int praisedCount { get; set; }
            public bool praised { get; set; }
            public bool subscribed { get; set; }
            public Rcmduser[] rcmdUsers { get; set; }
            public string playTimeStr { get => GetPlayCountStr.PlayCountstr(playTime); }
            public TimeSpan durationmsStr { get => TimeSpan.FromMilliseconds(durationms); }
            public string praisedCountstr { get => convertstr(praisedCount); }
            public string commentCountstr { get => convertstr(commentCount); }

            string convertstr(int count)
            {
                if (count >= 10000)
                    return ((double)(count / 10000)).ToString("f1") + "w";
                else
                    return count.ToString();

            }
        }


        public class Urlinfo
        {
            public string id { get; set; }
            public string url { get; set; }
            public int size { get; set; }
            public int validityTime { get; set; }
            public bool needPay { get; set; }
            public object payInfo { get; set; }
            public int r { get; set; }
        }

        public class Resolution
        {
            public int resolution { get; set; }
            public int size { get; set; }
        }

        public class Videogroup
        {
            public long id { get; set; }
            public string name { get; set; }
            public string alg { get; set; }
        }

        public class Relatesong
        {
            public string name { get; set; }
            public long id { get; set; }
            public int pst { get; set; }
            public int t { get; set; }
            public Ar[] ar { get; set; }
            public string[] alia { get; set; }
            public int pop { get; set; }
            public int st { get; set; }
            public string rt { get; set; }
            public int fee { get; set; }
            public int v { get; set; }
            public object crbt { get; set; }
            public string cf { get; set; }
            public Al al { get; set; }
            public int dt { get; set; }
            public H h { get; set; }
            public M m { get; set; }
            public L l { get; set; }
            public object a { get; set; }
            public string cd { get; set; }
            public int no { get; set; }
            public object rtUrl { get; set; }
            public int ftype { get; set; }
            public object[] rtUrls { get; set; }
            public long djId { get; set; }
            public int copyright { get; set; }
            public int s_id { get; set; }
            public int cp { get; set; }
            public int mv { get; set; }
            public int rtype { get; set; }
            public object rurl { get; set; }
            public int mst { get; set; }
            public long publishTime { get; set; }
            public Privilege privilege { get; set; }
            public string[] tns { get; set; }
        }

        public class Al
        {
            public long id { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public string[] tns { get; set; }
            public string pic_str { get; set; }
            public long pic { get; set; }
        }
        public class H
        {
            public int br { get; set; }
            public long fid { get; set; }
            public int size { get; set; }
            public float vd { get; set; }
        }
        public class M
        {
            public int br { get; set; }
            public long fid { get; set; }
            public int size { get; set; }
            public float vd { get; set; }
        }

        public class L
        {
            public int br { get; set; }
            public int fid { get; set; }
            public int size { get; set; }
            public float vd { get; set; }
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

        public class Ar
        {
            public int id { get; set; }
            public string name { get; set; }
            public object[] tns { get; set; }
            public object[] alias { get; set; }
        }

        public class Rcmduser
        {
            public string slogan { get; set; }
            public Profile profile { get; set; }
        }

        public class Profile
        {
            public bool defaultAvatar { get; set; }
            public int province { get; set; }
            public int authStatus { get; set; }
            public bool followed { get; set; }
            public string avatarUrl { get; set; }
            public int accountStatus { get; set; }
            public int gender { get; set; }
            public int city { get; set; }
            public long birthday { get; set; }
            public int userId { get; set; }
            public int userType { get; set; }
            public string nickname { get; set; }
            public string signature { get; set; }
            public string description { get; set; }
            public string detailDescription { get; set; }
            public long avatarImgId { get; set; }
            public long backgroundImgId { get; set; }
            public string backgroundUrl { get; set; }
            public int authority { get; set; }
            public bool mutual { get; set; }
            public object expertTags { get; set; }
            public Experts experts { get; set; }
            public int djStatus { get; set; }
            public int vipType { get; set; }
            public object remarkName { get; set; }
            public string avatarImgIdStr { get; set; }
            public string backgroundImgIdStr { get; set; }
            public string avatarImgId_str { get; set; }
            public int followeds { get; set; }
        }
    }

}
