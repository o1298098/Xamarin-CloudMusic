using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models
{
   public class AudioModel:BaseModel
    {
        private string path;
        private long time;
        private string name;
        private string mimetype;
        private string album;
        private string albumArt;
        private string albumId;
        private string artist;
        private string artistId;
        private long duration;

        public AudioModel(string path, long time, string name, string mimetype,string album,string albumart,string albumid,string artist,string artistId,long duration)
        {
            this.Path = path;
            this.Time = time;
            this.Name = name;
            this.Mimetype = mimetype;
            this.Album = album;
            this.AlbumArt = albumart;
            this.AlbumId = albumid;
            this.Artist = artist;
            this.ArtistId = artistId;
            this.Duration = duration;

    }
        public string AlbumArt
        {
            get => albumArt;
            set => albumArt = value;
        }
        public string AlbumId
        {
            get => albumId;
            set => albumId = value;
        }
        public string Artist
        {
            get => artist;
            set => artist = value;
        }
        public string ArtistId
        {
            get => artistId;
            set => artistId = value;
        }
        public long Duration
        {
            get => duration;
            set => duration = value;
        }
        public string Album
        {
            get => album;
            set => album = value;
        }
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }
        public long Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
        public string Name
        {
            get=> name;
            set => name = value;
        }
        public string Mimetype
        {
           get=> mimetype;
            set => mimetype = value;
        }
    }
}
