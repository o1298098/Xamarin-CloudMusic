using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
   public  class VideoUrls
    {
            public Url[] urls { get; set; }
            public int code { get; set; }

        public class Url
        {
            public string id { get; set; }
            public string url { get; set; }
            public int size { get; set; }
            public int validityTime { get; set; }
            public bool needPay { get; set; }
            public object payInfo { get; set; }
            public int r { get; set; }
        }

    }
}
