using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class Djradio
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
}
