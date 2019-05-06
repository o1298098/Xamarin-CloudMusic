using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.Models
{
    public class ImageModel
    {
        private string path;
        private long time;
        private string name;
        private string mimetype;

        public ImageModel(string path, long time, string name, string mimetype)
        {
            this.path = path;
            this.time = time;
            this.name = name;
            this.mimetype = mimetype;
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
            get
            {
                return name;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string getMimetype()
        {
            return mimetype;
        }

        public void setMimetype(string mimetype)
        {
            this.mimetype = mimetype;
        }
        public ImageSource Uri
        {
            get { return ImageSource.FromFile(this.path); }
        }
    }
}
