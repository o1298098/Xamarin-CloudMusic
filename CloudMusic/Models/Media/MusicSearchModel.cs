using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CloudMusic.Models.Media
{
    public class MusicSearchModel: BaseModel
    {
        public Result result { get; set; }
        public int code { get; set; }


       
        public class Result : BaseModel
        {
            ObservableCollection<Song> _songs;
            ObservableCollection<Mv> _mvs;
            ObservableCollection<Album> _albums;
            ObservableCollection<Artist> _artists;
            ObservableCollection<Playlist> _playlists;
            ObservableCollection<Userprofile> _userprofiles;
            ObservableCollection<Djradio> _djRadios;
            ObservableCollection<Video> _videos;
            public ObservableCollection<Song> songs { get => _songs; set => SetProperty(ref _songs, value, "songs"); }
            public int songCount { get; set; }
            public ObservableCollection<Mv> mvs { get => _mvs; set => SetProperty(ref _mvs, value, "mvs"); }
            public int mvCount { get; set; }
            public ObservableCollection<Album> albums { get => _albums; set => SetProperty(ref _albums, value, "albums"); }
            public int albumCount { get; set; }
            public ObservableCollection<Artist> artists { get => _artists; set => SetProperty(ref _artists, value, "artists"); }
            public int artistCount { get; set; }
            public ObservableCollection<Playlist> playlists { get => _playlists; set => SetProperty(ref _playlists, value, "playlists"); }
            public int playlistCount { get; set; }
            public ObservableCollection<Userprofile> userprofiles { get => _userprofiles; set => SetProperty(ref _userprofiles, value, "userprofiles"); }
            public int userprofileCount { get; set; }
            public ObservableCollection<Djradio> djRadios { get => _djRadios; set => SetProperty(ref _djRadios, value, "djRadios"); }
            public int djRadiosCount { get; set; }
            public ObservableCollection<Video> videos { get => _videos; set => SetProperty(ref _videos, value, "videos"); }
            public int videoCount { get; set; }
        }       
       


      


    }

}







