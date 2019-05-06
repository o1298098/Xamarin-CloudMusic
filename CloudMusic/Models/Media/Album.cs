using CloudMusic.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class Album
    {
        public string name { get; set; }
        public long id { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public long picId { get; set; }
        public string blurPicUrl { get; set; }
        public int companyId { get; set; }
        public long pic { get; set; }
        public string picUrl { get; set; }
        public long publishTime { get; set; }
        public DateTime publishDateTime { get => TimeStampToDatetime.GetTime(publishTime); }
        public string description { get; set; }
        public string tags { get; set; }
        public string company { get; set; }
        public string briefDesc { get; set; }
        public Artist artist { get; set; }
        public object songs { get; set; }
        public string[] alias { get; set; }
        public int status { get; set; }
        public int copyrightId { get; set; }
        public string commentThreadId { get; set; }
        public Artist[] artists { get; set; }
        public bool paid { get; set; }
        public bool onSale { get; set; }
        public string alg { get; set; }
        public string containedSong { get; set; }
        public string picId_str { get; set; }
    }

}
