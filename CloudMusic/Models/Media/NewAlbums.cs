using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class NewAlbums
    {
           public int total { get; set; }
            public int code { get; set; }
            public List<Album> albums { get; set; }
    }
}
