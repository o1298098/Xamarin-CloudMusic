using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CloudMusic.Models.Media
{
   public class MainBanner
    {
        public ObservableCollection<Banner> banners { get; set; }
        public int code { get; set; }
        public class Banner
        {
            public string imageUrl { get; set; }
            public long targetId { get; set; }
            public object adid { get; set; }
            public int targetType { get; set; }
            public string titleColor { get; set; }
            public string typeTitle { get; set; }
            public string url { get; set; }
            public bool exclusive { get; set; }
            public object monitorImpress { get; set; }
            public object monitorClick { get; set; }
            public object monitorType { get; set; }
            public object monitorImpressList { get; set; }
            public object monitorClickList { get; set; }
            public object monitorBlackList { get; set; }
            public object extMonitor { get; set; }
            public object extMonitorInfo { get; set; }
            public object adSource { get; set; }
            public object adLocation { get; set; }
            public string encodeId { get; set; }
            public object program { get; set; }
            public object _event { get; set; }
            public object video { get; set; }
            public object song { get; set; }
            public string scm { get; set; }
        }
    }

}
