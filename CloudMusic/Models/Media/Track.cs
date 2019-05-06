using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class Track
    {
        public string name { get; set; }
        public int id { get; set; }
        public int pst { get; set; }
        public int t { get; set; }
        public List<Ar> ar { get; set; }
        public string arstr => string.Join("/", ar.Select(r => r.name));
        public string[] alia { get; set; }
        public int pop { get; set; }
        public int st { get; set; }
        public string rt { get; set; }
        public int fee { get; set; }
        public int v { get; set; }
        public string crbt { get; set; }
        public string cf { get; set; }
        public Al al { get; set; }
        public int dt { get; set; }
        public H h { get; set; }
        public M m { get; set; }
        public L l { get; set; }
        public object a { get; set; }
        public string cd { get; set; }
        public int no { get; set; }
        public object rtUrl { get; set; }
        public int ftype { get; set; }
        public object[] rtUrls { get; set; }
        public int djId { get; set; }
        public int copyright { get; set; }
        public int s_id { get; set; }
        public int rtype { get; set; }
        public object rurl { get; set; }
        public int mst { get; set; }
        public int cp { get; set; }
        public int mv { get; set; }
        public long publishTime { get; set; }
        public string[] tns { get; set; }
        public bool hasmv { get => mv > 0; }
    }

    public class Al
    {
        public int id { get; set; }
        public string name { get; set; }
        public string picUrl { get; set; }
        public string[] tns { get; set; }
        public string pic_str { get; set; }
        public long pic { get; set; }
    }

    public class H
    {
        public int br { get; set; }
        public int fid { get; set; }
        public int size { get; set; }
        public float vd { get; set; }
    }

    public class M
    {
        public int br { get; set; }
        public int fid { get; set; }
        public int size { get; set; }
        public float vd { get; set; }
    }

    public class L
    {
        public int br { get; set; }
        public int fid { get; set; }
        public int size { get; set; }
        public float vd { get; set; }
    }

    public class Ar
    {
        public int id { get; set; }
        public string name { get; set; }
        public object[] tns { get; set; }
        public object[] alias { get; set; }
    }

    public class Trackid
    {
        public int id { get; set; }
        public int v { get; set; }
    }
}
