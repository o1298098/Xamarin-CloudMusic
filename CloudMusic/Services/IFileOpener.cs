using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Services
{
    public interface IFileOpener
    {
        void OpenFile(byte[] data,string name);
    }
}
