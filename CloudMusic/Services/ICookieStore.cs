using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CloudMusic.Services
{
    public interface ICookieStore
    {
        IEnumerable<Cookie> CurrentCookies { get; }
        void Init(string host);
        void DumpAllCookiesToLog();
        void DeleteAllCookiesForSite(string url);
        void SetCookie(Cookie cookie);
    }
}
