using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Services
{
    public interface ICrossPrint
    {
        void Print();
        void WebPrint(string Html);
    }
}
