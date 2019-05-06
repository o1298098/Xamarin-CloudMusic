using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class Artist : BaseModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public string picUrl { get; set; }
        public ObservableCollection<string> alias { get; set; }
        public int albumSize { get; set; }
        public long picId { get; set; }
        public string img1v1Url { get; set; }
        public long img1v1 { get; set; }
        public int mvSize { get; set; }
        public bool followed { get; set; }
        public string alg { get; set; }
        public object trans { get; set; }
        public ObservableCollection<string> alia { get; set; }
        public long accountId { get; set; }
    }
}
