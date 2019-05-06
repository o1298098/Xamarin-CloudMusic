using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class MusicPlayListDetail
    {
        public Playlist playlist { get; set; }
        public int code { get; set; }
        public Privilege[] privileges { get; set; }        
        

        public class Privilege
        {
            public int id { get; set; }
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
    }

   

}
