using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace CloudMusic.Models.Media
{
    public class MusicEventModel
    {
        public int code { get; set; }
        public bool more { get; set; }
        [JsonProperty("event")]
        public ObservableCollection<friendEvent> friendevent { get; set; }
        public long lasttime { get; set; }
        public class friendEvent
        {
            public string actName { get; set; }
            public int forwardCount { get; set; }
            public object lotteryEventData { get; set; }
            string _json { get; set; }
            public string json { get=>_json; set { _json = value;_conent = JsonConvert.DeserializeObject<conent>(value); } }
            public int expireTime { get; set; }
            public string uuid { get; set; }
            public long eventTime { get; set; }
            public long actId { get; set; }
            public List<Pic> pics { get; set; }
            public long showTime { get; set; }
            public long tmplId { get; set; }
            public Rcmdinfo rcmdInfo { get; set; }
            public User user { get; set; }
            public long id { get; set; }
            public int type { get; set; }
            public bool topEvent { get; set; }
            public int insiteForwardCount { get; set; }
            public Info info { get; set; }
            conent _conent { get;set; }
            public conent Conent { get => _conent; }
        }

        public class Rcmdinfo
        {
            public string reason { get; set; }
            public string alg { get; set; }
            public int type { get; set; }
            public string scene { get; set; }
            public string userReason { get; set; }
        }

        public class User
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
            public string[] expertTags { get; set; }
            public Experts experts { get; set; }
            public int djStatus { get; set; }
            public int vipType { get; set; }
            public object remarkName { get; set; }
            public string backgroundImgIdStr { get; set; }
            public string avatarImgIdStr { get; set; }
            public bool urlAnalyze { get; set; }
            public string avatarImgId_str { get; set; }
            public int followeds { get; set; }
        }

        public class Info
        {
            public Commentthread commentThread { get; set; }
            public object latestLikedUsers { get; set; }
            public bool liked { get; set; }
            public object comments { get; set; }
            public int resourceType { get; set; }
            public long resourceId { get; set; }
            public int shareCount { get; set; }
            public int commentCount { get; set; }
            public int likedCount { get; set; }
            public string threadId { get; set; }
        }

        public class Commentthread
        {
            public string id { get; set; }
            public Resourceinfo resourceInfo { get; set; }
            public int resourceType { get; set; }
            public int commentCount { get; set; }
            public int likedCount { get; set; }
            public int shareCount { get; set; }
            public int hotCount { get; set; }
            public Latestlikeduser[] latestLikedUsers { get; set; }
            public int resourceOwnerId { get; set; }
            public string resourceTitle { get; set; }
            public long resourceId { get; set; }
        }

        public class Resourceinfo
        {
            public long id { get; set; }
            public long userId { get; set; }
            public string name { get; set; }
            public object imgUrl { get; set; }
            public object creator { get; set; }
            public int eventType { get; set; }
        }

        public class Latestlikeduser
        {
            public int s { get; set; }
            public long t { get; set; }
        }

        public class Pic
        {
            public string squareUrl { get; set; }
            public string rectangleUrl { get; set; }
            public string pcSquareUrl { get; set; }
            public string pcRectangleUrl { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string originUrl { get; set; }
            public string format { get; set; }
        }

        public class conent
        {
            public string msg { get; set; }
            public Video video { get; set; }
            public Song song { get; set; }
            public bool isSong { get => video == null ? true : false; }
            public string conentstr { get => video == null ? "分享单曲:" : "发布视频:"; }
        }
    }
}
