using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class MusicSearchAllModel : BaseModel
    {

            public Result result { get; set; }
            public int code { get; set; }

        public class Result
        {
            public Song song { get; set; }
            public int code { get; set; }
            public Djradio djRadio { get; set; }
            public Playlist playList { get; set; }
            public Artist3 artist { get; set; }
            public Album1 album { get; set; }
            public Video video { get; set; }
            public Sim_Query sim_query { get; set; }
            public User user { get; set; }
            public string[] order { get; set; }
        }

        public class Song
        {
            public bool more { get; set; }
            public Song1[] songs { get; set; }
        }

        public class Song1
        {
            public string name { get; set; }
            public int id { get; set; }
            public int pst { get; set; }
            public int t { get; set; }
            public Ar[] ar { get; set; }
            public object[] alia { get; set; }
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
            public int djId { get; set; }
            public int copyright { get; set; }
            public int s_id { get; set; }
            public int rtype { get; set; }
            public object rurl { get; set; }
            public int mst { get; set; }
            public int cp { get; set; }
            public int mv { get; set; }
            public long publishTime { get; set; }
            public Privilege privilege { get; set; }
            public string alg { get; set; }
            public string[] tns { get; set; }
        }

        public class Al
        {
            public int id { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public object[] tns { get; set; }
            public long pic { get; set; }
            public string pic_str { get; set; }
        }

        public class H
        {
            public int br { get; set; }
            public int fid { get; set; }
            public int size { get; set; }
            public float vd { get; set; }
        }

        public class M
        {
            public int br { get; set; }
            public int fid { get; set; }
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
            public int id { get; set; }
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
        }

        public class Ar
        {
            public int id { get; set; }
            public string name { get; set; }
            public object[] tns { get; set; }
            public object[] alias { get; set; }
        }

        public class Djradio
        {
            public Djradio1[] djRadios { get; set; }
            public bool more { get; set; }
        }

        public class Djradio1
        {
            public int id { get; set; }
            public Dj dj { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public string desc { get; set; }
            public int subCount { get; set; }
            public int programCount { get; set; }
            public long createTime { get; set; }
            public int categoryId { get; set; }
            public string category { get; set; }
            public int radioFeeType { get; set; }
            public int feeScope { get; set; }
            public bool buyed { get; set; }
            public object videos { get; set; }
            public bool finished { get; set; }
            public bool underShelf { get; set; }
            public int purchaseCount { get; set; }
            public int price { get; set; }
            public int originalPrice { get; set; }
            public object discountPrice { get; set; }
            public long lastProgramCreateTime { get; set; }
            public string lastProgramName { get; set; }
            public int lastProgramId { get; set; }
            public long picId { get; set; }
            public object rcmdText { get; set; }
            public bool composeVideo { get; set; }
            public int shareCount { get; set; }
            public object rcmdtext { get; set; }
            public int likedCount { get; set; }
            public string alg { get; set; }
            public int commentCount { get; set; }
        }

        public class Dj
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
            public object experts { get; set; }
            public int djStatus { get; set; }
            public int vipType { get; set; }
            public object remarkName { get; set; }
            public string avatarImgIdStr { get; set; }
            public string backgroundImgIdStr { get; set; }
            public string avatarImgId_str { get; set; }
        }

        public class Playlist
        {
            public bool more { get; set; }
            public Playlist1[] playLists { get; set; }
        }

        public class Playlist1
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
            public Track track { get; set; }
            public object alg { get; set; }
        }

        public class Creator
        {
            public string nickname { get; set; }
            public long userId { get; set; }
            public int userType { get; set; }
            public int authStatus { get; set; }
            public object expertTags { get; set; }
            public object experts { get; set; }
        }

        public class Track
        {
            public string name { get; set; }
            public long id { get; set; }
            public int position { get; set; }
            public object[] alias { get; set; }
            public int status { get; set; }
            public int fee { get; set; }
            public int copyrightId { get; set; }
            public string disc { get; set; }
            public int no { get; set; }
            public Artist2[] artists { get; set; }
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
            public object crbt { get; set; }
            public object audition { get; set; }
            public string copyFrom { get; set; }
            public string commentThreadId { get; set; }
            public object rtUrl { get; set; }
            public int ftype { get; set; }
            public object[] rtUrls { get; set; }
            public int copyright { get; set; }
            public int mvid { get; set; }
            public Bmusic bMusic { get; set; }
            public object mp3Url { get; set; }
            public int rtype { get; set; }
            public object rurl { get; set; }
            public Hmusic hMusic { get; set; }
            public Mmusic mMusic { get; set; }
            public Lmusic lMusic { get; set; }
        }
        public class Artist
        {
            public string name { get; set; }
            public int id { get; set; }
            public int picId { get; set; }
            public int img1v1Id { get; set; }
            public string briefDesc { get; set; }
            public string picUrl { get; set; }
            public string img1v1Url { get; set; }
            public int albumSize { get; set; }
            public object[] alias { get; set; }
            public string trans { get; set; }
            public int musicSize { get; set; }
        }

        public class Artist1
        {
            public string name { get; set; }
            public int id { get; set; }
            public int picId { get; set; }
            public int img1v1Id { get; set; }
            public string briefDesc { get; set; }
            public string picUrl { get; set; }
            public string img1v1Url { get; set; }
            public int albumSize { get; set; }
            public object[] alias { get; set; }
            public string trans { get; set; }
            public int musicSize { get; set; }
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
        }

        public class Hmusic
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
        }

        public class Artist2
        {
            public string name { get; set; }
            public int id { get; set; }
            public int picId { get; set; }
            public int img1v1Id { get; set; }
            public string briefDesc { get; set; }
            public string picUrl { get; set; }
            public string img1v1Url { get; set; }
            public int albumSize { get; set; }
            public object[] alias { get; set; }
            public string trans { get; set; }
            public int musicSize { get; set; }
        }

        public class Artist3
        {
            public Artist4[] artists { get; set; }
            public bool more { get; set; }
        }

        public class Artist4
        {
            public int id { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public object[] alias { get; set; }
            public int albumSize { get; set; }
            public long picId { get; set; }
            public string img1v1Url { get; set; }
            public int accountId { get; set; }
            public long img1v1 { get; set; }
            public int mvSize { get; set; }
            public bool followed { get; set; }
            public string alg { get; set; }
            public object trans { get; set; }
        }

        public class Album1
        {
            public Album[] albums { get; set; }
            public bool more { get; set; }
        }

       

        public class Artist5
        {
            public string name { get; set; }
            public int id { get; set; }
            public long picId { get; set; }
            public long img1v1Id { get; set; }
            public string briefDesc { get; set; }
            public string picUrl { get; set; }
            public string img1v1Url { get; set; }
            public int albumSize { get; set; }
            public object[] alias { get; set; }
            public string trans { get; set; }
            public int musicSize { get; set; }
            public int topicPerson { get; set; }
            public string picId_str { get; set; }
            public string img1v1Id_str { get; set; }
            public object[] alia { get; set; }
        }

        public class Artist6
        {
            public string name { get; set; }
            public int id { get; set; }
            public int picId { get; set; }
            public long img1v1Id { get; set; }
            public string briefDesc { get; set; }
            public string picUrl { get; set; }
            public string img1v1Url { get; set; }
            public int albumSize { get; set; }
            public object[] alias { get; set; }
            public string trans { get; set; }
            public int musicSize { get; set; }
            public int topicPerson { get; set; }
            public string img1v1Id_str { get; set; }
        }

        public class Video
        {
            public bool more { get; set; }
            public Video1[] videos { get; set; }
        }

        public class Video1
        {
            public string coverUrl { get; set; }
            public string title { get; set; }
            public long durationms { get; set; }
            public int playTime { get; set; }
            public int type { get; set; }
            public Creator1[] creator { get; set; }
            public object aliaName { get; set; }
            public object transName { get; set; }
            public string vid { get; set; }
            public object[] markTypes { get; set; }
            public string alg { get; set; }
            public TimeSpan durationtime { get => new TimeSpan(durationms); }
            public string playTimestr { get => GetPlayCountStr.PlayCountstr(playTime); }
        }

        public class Creator1
        {
            public int userId { get; set; }
            public string userName { get; set; }
        }

        public class Sim_Query
        {
            public Sim_Querys[] sim_querys { get; set; }
            public bool more { get; set; }
        }

        public class Sim_Querys
        {
            public string keyword { get; set; }
            public string alg { get; set; }
        }

        public class User
        {
            public bool more { get; set; }
            public User1[] users { get; set; }
        }

        public class User1
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
            public object experts { get; set; }
            public int djStatus { get; set; }
            public int vipType { get; set; }
            public object remarkName { get; set; }
            public string avatarImgIdStr { get; set; }
            public string backgroundImgIdStr { get; set; }
            public string alg { get; set; }
        }

    }
}
