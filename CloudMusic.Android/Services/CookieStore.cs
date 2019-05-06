using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using CloudMusic.Droid.Services;
using CloudMusic.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CookieStore))]
namespace CloudMusic.Droid.Services
{
    public class CookieStore : ICookieStore
    {
        private  string _url;
        private readonly object _refreshLock = new object();

        public IEnumerable<Cookie> CurrentCookies
        {
            get { return RefreshCookies(); }
        }


        //public CookieStore(string url = "")
        //{
        //    if (string.IsNullOrWhiteSpace(url))
        //    {
        //        throw new ArgumentNullException("url", "On Android, 'url' cannot be empty,          please provide a base URL for it to use when loading related cookies");
        //    }
        //    _url = url;
        //}

        private IEnumerable<Cookie> RefreshCookies()
        {
            lock (_refreshLock)
            {
                var allCookiesForUrl = CookieManager.Instance.GetCookie(_url);

                if (string.IsNullOrWhiteSpace(allCookiesForUrl))
                {
                    Log.Debug(string.Format("No cookies found for '{0}'. Exiting.", _url),"");
                    yield return new Cookie("none", "none");
                }
                else
                {
                    Log.Debug(string.Format("\r\n===== CookieHeader : '{0}'\r\n", allCookiesForUrl),"");

                    var cookiePairs = allCookiesForUrl.Split(' ');
                    foreach (var cookiePair in cookiePairs.Where(cp => cp.Contains("=")))
                    {
                        var cookiePieces = cookiePair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (cookiePieces.Length >= 2)
                        {
                            cookiePieces[0] = cookiePieces[0].Contains(":")
                              ? cookiePieces[0].Substring(0, cookiePieces[0].IndexOf(":"))
                              : cookiePieces[0];
                            cookiePieces[1] = cookiePieces[1].EndsWith(";")
                              ? cookiePieces[1].Substring(0, cookiePieces[1].Length - 1)
                              : cookiePieces[1];

                            yield return new Cookie()
                            {
                                Name = cookiePieces[0],
                                Value = cookiePieces[1],
                                Path = "/",
                                Domain = new Uri(_url).DnsSafeHost,
                            };
                        }
                    }
                }
            }
        }

        public void DumpAllCookiesToLog()
        {
        }


        public void DeleteAllCookiesForSite(string url)
        {
            CookieManager.Instance.RemoveAllCookie();
        }

        public void Init(string host)
        {
            _url = host;
        }

        public void SetCookie(Cookie cookie)
        {
            CookieManager.Instance.SetAcceptCookie(true);
            string f = cookie.Name + "=" + cookie.Value;
            CookieManager.Instance.SetCookie(_url, f);
        }
    }
}