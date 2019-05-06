using CloudMusic.Actions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class MusicComment
    {
        public bool isMusician { get; set; }
        public int userId { get; set; }
        public object[] topComments { get; set; }
        public bool moreHot { get; set; }
        public Hotcomment[] hotComments { get; set; }
        public int code { get; set; }
        public ObservableCollection<Comment> comments { get; set; }
        public int total { get; set; }
        public bool more { get; set; }

        public class Hotcomment
        {
            public User user { get; set; }
            public Bereplied[] beReplied { get; set; }
            public Pendantdata pendantData { get; set; }
            public object showFloorComment { get; set; }
            public int status { get; set; }
            public int commentLocationType { get; set; }
            public int parentCommentId { get; set; }
            public object decoration { get; set; }
            public bool repliedMark { get; set; }
            public bool liked { get; set; }
            public int likedCount { get; set; }
            public int commentId { get; set; }
            public long time { get; set; }
            public object expressionUrl { get; set; }
            public string content { get; set; }
            public bool hasbeReplied { get => beReplied?.Length > 0; }
            public DateTime Customdate { get => TimeStampToDatetime.GetTime(time); }
        }

        public class User
        {
            public object locationInfo { get; set; }
            public int userId { get; set; }
            public Viprights vipRights { get; set; }
            public int userType { get; set; }
            public object expertTags { get; set; }
            public int vipType { get; set; }
            public string nickname { get; set; }
            public object remarkName { get; set; }
            public string avatarUrl { get; set; }
            public object experts { get; set; }
            public int authStatus { get; set; }
        }

        public class Viprights
        {
            public Associator associator { get; set; }
            public object musicPackage { get; set; }
            public int redVipAnnualCount { get; set; }
        }

        public class Associator
        {
            public int vipCode { get; set; }
            public bool rights { get; set; }
        }

        public class Pendantdata
        {
            public int id { get; set; }
            public string imageUrl { get; set; }
        }

        public class Bereplied
        {
            public User user { get; set; }
            public int beRepliedCommentId { get; set; }
            public string content { get; set; }
            public int status { get; set; }
            public object expressionUrl { get; set; }
        }

        public class Comment
        {
            long _time;
            public User user { get; set; }
            public object[] beReplied { get; set; }
            public object pendantData { get; set; }
            public object showFloorComment { get; set; }
            public int status { get; set; }
            public int commentLocationType { get; set; }
            public int parentCommentId { get; set; }
            public Decoration decoration { get; set; }
            public bool repliedMark { get; set; }
            public bool liked { get; set; }
            public int likedCount { get; set; }
            public int commentId { get; set; }
            public long time { get=> _time; set=> _time=value; }
            public object expressionUrl { get; set; }
            public string content { get; set; }
            public bool isRemoveHotComment { get; set; }
            public bool hasbeReplied { get => beReplied?.Length > 0; }
            public DateTime Customdate {get => TimeStampToDatetime.GetTime(time); }
            public string likestr { get => likedCount > 0 ? likedCount.ToString() : ""; }
            
        }

        
    }
        public class Decoration
        {
        }
}
