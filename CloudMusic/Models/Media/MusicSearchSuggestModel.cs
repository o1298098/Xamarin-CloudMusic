using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class MusicSearchSuggestModel
    {
            public Result result { get; set; }
            public int code { get; set; }

        public class Result
        {
            public Album[] albums { get; set; }
            public Artist[] artists { get; set; }
            public Song[] songs { get; set; }
            public Playlist[] playlists { get; set; }
            public Mv[] mvs { get; set; }
            public string[] order { get; set; }
        }

        public class Album
        {
            public int id { get; set; }
            public string name { get; set; }
            public Artist artist { get; set; }
            public long publishTime { get; set; }
            public int size { get; set; }
            public int copyrightId { get; set; }
            public int status { get; set; }
            public long picId { get; set; }
        }
      
        public class Artist
        {
            public int id { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public string[] alias { get; set; }
            public int albumSize { get; set; }
            public long picId { get; set; }
            public string img1v1Url { get; set; }
            public long img1v1 { get; set; }
            public string[] transNames { get; set; }
            public string[] alia { get; set; }
            public string trans { get; set; }
        }

        public class Song
        {
            public int id { get; set; }
            public string name { get; set; }
            public Artist[] artists { get; set; }
            public Album album { get; set; }
            public int duration { get; set; }
            public int copyrightId { get; set; }
            public int status { get; set; }
            public object[] alias { get; set; }
            public int rtype { get; set; }
            public int ftype { get; set; }
            public int mvid { get; set; }
            public int fee { get; set; }
            public object rUrl { get; set; }
        }       

        public class Playlist
        {
            public long id { get; set; }
            public string name { get; set; }
            public string coverImgUrl { get; set; }
            public object creator { get; set; }
            public bool subscribed { get; set; }
            public int trackCount { get; set; }
            public int userId { get; set; }
            public int playCount { get; set; }
            public int bookCount { get; set; }
            public string description { get; set; }
            public bool highQuality { get; set; }
        }

        public class Mvs
        {
            public int id { get; set; }
            public string cover { get; set; }
            public string name { get; set; }
            public int playCount { get; set; }
            public object briefDesc { get; set; }
            public object desc { get; set; }
            public string artistName { get; set; }
            public int artistId { get; set; }
            public int duration { get; set; }
            public int mark { get; set; }
            public object transNames { get; set; }
            public object alias { get; set; }
            public Artist[] artists { get; set; }
            public bool subed { get; set; }
            public Mv mv { get; set; }
        }

        public class Mv
        {
            public int authId { get; set; }
            public int status { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public object subTitle { get; set; }
            public object appTitle { get; set; }
            public object aliaName { get; set; }
            public object transName { get; set; }
            public long pic4v3 { get; set; }
            public long pic16v9 { get; set; }
            public int caption { get; set; }
            public string captionLanguage { get; set; }
            public string style { get; set; }
            public object mottos { get; set; }
            public object oneword { get; set; }
            public object appword { get; set; }
            public object stars { get; set; }
            public object desc { get; set; }
            public string area { get; set; }
            public string type { get; set; }
            public string subType { get; set; }
            public int neteaseonly { get; set; }
            public int upban { get; set; }
            public object topWeeks { get; set; }
            public string publishTime { get; set; }
            public int online { get; set; }
            public int score { get; set; }
            public int plays { get; set; }
            public int monthplays { get; set; }
            public int weekplays { get; set; }
            public int dayplays { get; set; }
            public int fee { get; set; }
            public Artist[] artists { get; set; }
            public Video[] videos { get; set; }
        }

        public class Video
        {
            public Tagsign tagSign { get; set; }
            public string tag { get; set; }
            public string url { get; set; }
            public int duration { get; set; }
            public int size { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string container { get; set; }
            public string md5 { get; set; }
            public bool check { get; set; }
        }

        public class Tagsign
        {
            public int br { get; set; }
            public string type { get; set; }
            public string tagSign { get; set; }
            public int resolution { get; set; }
            public string mvtype { get; set; }
        }

    }
}
