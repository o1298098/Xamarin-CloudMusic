using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CloudMusic.Models.Media
{
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
        public long userId { get; set; }
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
        public List<string> expertTags { get; set; }
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
        [JsonProperty("1")]
        public string _1 { get; set; }
        [JsonProperty("2")]
        public string _2 { get; set; }
    }
}
