using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class Song
    {
        public long id { get; set; }
        public string name { get; set; }
        public List<Artist> artists { get; set; }
        public Album album { get; set; }
        public int duration { get; set; }
        public int copyrightId { get; set; }
        public int status { get; set; }
        public string[] alias { get; set; }
        public int rtype { get; set; }
        public int ftype { get; set; }
        public int mvid { get; set; }
        public int fee { get; set; }
        public object rUrl { get; set; }
        public TimeSpan durationtime { get => TimeSpan.FromMilliseconds(duration); }
        public bool hasMv { get => mvid > 0; }
    }
}
