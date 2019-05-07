using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CloudMusic.Models.Media
{
   public class ArtistsInfo
    {
            public int code { get; set; }
            public Artist artist { get; set; }
            public bool more { get; set; }
            public ObservableCollection<Hotsong> hotSongs { get; set; }

        public class Artist
        {
            public long img1v1Id { get; set; }
            public int topicPerson { get; set; }
            public long picId { get; set; }
            public string[] alias { get; set; }
            public int musicSize { get; set; }
            public string picUrl { get; set; }
            public bool followed { get; set; }
            public string trans { get; set; }
            public int albumSize { get; set; }
            public string img1v1Url { get; set; }
            public string briefDesc { get; set; }
            public string name { get; set; }
            public long id { get; set; }
            public long publishTime { get; set; }
            public string picId_str { get; set; }
            public string img1v1Id_str { get; set; }
            public int mvSize { get; set; }
        }

        public class Hotsong
        {
            public object[] rtUrls { get; set; }
            public Ar[] ar { get; set; }
            public string arstr { get => string.Join("/", ar.Select(c => c.name)); }
            public Al al { get; set; }
            public int st { get; set; }
            public object a { get; set; }
            public M m { get; set; }
            public int no { get; set; }
            public int pst { get; set; }
            public string[] alia { get; set; }
            public int pop { get; set; }
            public string rt { get; set; }
            public int mst { get; set; }
            public int v { get; set; }
            public int cp { get; set; }
            public string cf { get; set; }
            public int dt { get; set; }
            public H h { get; set; }
            public L l { get; set; }
            public string cd { get; set; }
            public string crbt { get; set; }
            public object rtUrl { get; set; }
            public int t { get; set; }
            public long djId { get; set; }
            public int mv { get; set; }
            public int fee { get; set; }
            public int ftype { get; set; }
            public int rtype { get; set; }
            public object rurl { get; set; }
            public string name { get; set; }
            public long id { get; set; }
            public Privilege privilege { get; set; }
            public string eq { get; set; }
        }

        public class Al
        {
            public long id { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public string[] tns { get; set; }
            public string pic_str { get; set; }
            public long pic { get; set; }
        }

        public class M
        {
            public int br { get; set; }
            public long fid { get; set; }
            public int size { get; set; }
            public float vd { get; set; }
        }

        public class H
        {
            public int br { get; set; }
            public long fid { get; set; }
            public int size { get; set; }
            public float vd { get; set; }
        }

        public class L
        {
            public int br { get; set; }
            public long fid { get; set; }
            public int size { get; set; }
            public float vd { get; set; }
        }

        public class Privilege
        {
            public long id { get; set; }
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
            public bool preSell { get; set; }
        }

        public class Ar
        {
            public long id { get; set; }
            public string name { get; set; }
            public string[] alia { get; set; }
        }

    }
}
