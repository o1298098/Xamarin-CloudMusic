using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
   public class MusicPlayLists
    {
        public bool more { get; set; }
        public Playlist[] playlist { get; set; }
        public int code { get; set; }

        public class Creator
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
            public string avatarImgId_str { get; set; }
        }

        public class Experts
        {
            public string _2 { get; set; }
        }
    }



}
