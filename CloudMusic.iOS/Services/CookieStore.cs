using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CloudMusic.iOS.Services;
using CloudMusic.Services;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(CookieStore))]
namespace CloudMusic.iOS.Services
{
    public class CookieStore: ICookieStore
    {
        private readonly object _refreshLock = new object();
        private string _url;
        public IEnumerable<Cookie> CurrentCookies
        {
            get { return RefreshCookies(); }
        }

        //public CookieStore(string url = "")
        //{
        //}

        private IEnumerable<Cookie> RefreshCookies()
        {
            lock (_refreshLock)
            {
                foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
                {
                    yield return new Cookie
                    {
                        Comment = cookie.Comment,
                        Domain = cookie.Domain,
                        HttpOnly = cookie.IsHttpOnly,
                        Name = cookie.Name,
                        Path = cookie.Path,
                        Secure = cookie.IsSecure,
                        Value = cookie.Value,
                        Version = Convert.ToInt32(cookie.Version)
                    };
                }
            }
        }

        public void DumpAllCookiesToLog()
        {
            //if (!CurrentCookies.Any())
            //{
            //    LogDebug("No cookies in your iOS cookie store. Srsly? No cookies? At all?!?");
            //}
            //CurrentCookies.ToList()
            //              .ForEach(cookie =>
            //                      Log.Debug(string.Format("Cookie dump: {0} = {1}",
            //                                              cookie.Name,
            //                                              cookie.Value)));
        }

        public void DeleteAllCookiesForSite(string url)
        {
            var cookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in cookieStorage.CookiesForUrl(new NSUrl(url)).ToList())
            {
                cookieStorage.DeleteCookie(cookie);
            }
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        public void Init(string host)
        {
            _url = host;
        }

        public void SetCookie(Cookie cookie)
        {
            var cookieUrl = new Uri(_url);
            var cookieJar = NSHttpCookieStorage.SharedStorage;
            cookieJar.AcceptPolicy = NSHttpCookieAcceptPolicy.Always;
            var c=new NSHttpCookie(cookie.Name,cookie.Value,cookie.Path,cookie.Domain);
            cookieJar.SetCookie(c);
        }
    }
}