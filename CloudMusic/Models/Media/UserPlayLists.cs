using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class UserPlayLists
    {
            public bool more { get; set; }
            public ObservableCollection<Playlist> playlist { get; set; }
            public int code { get; set; }

    }
}
