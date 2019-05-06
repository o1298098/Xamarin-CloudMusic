using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CloudMusic.ApiHelper
{
    class HttpClient
    {
        /// <summary>
        /// Seivice URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Cookie，保证登录后，所有访问持有一个Cookie；
        /// </summary>
        static CookieContainer Cookie = new CookieContainer();
        public static CookieContainer CloudMusicCookie = new CookieContainer();

        /// <summary>
        /// HTTP访问
        /// </summary>
        public string SysncRequest()
        {
            HttpWebRequest httpRequest = HttpWebRequest.Create(Url) as HttpWebRequest;
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.CookieContainer = Cookie;
            httpRequest.Timeout = 10000;
            try
            {
                using (Stream reqStream = httpRequest.GetRequestStream())
                {
                    JObject jObj = new JObject();
                    jObj.Add("format", 1);
                    jObj.Add("useragent", "ApiClient");
                    jObj.Add("rid", Guid.NewGuid().ToString().GetHashCode().ToString());
                    jObj.Add("parameters", Content);
                    jObj.Add("timestamp", DateTime.Now);
                    jObj.Add("v", "1.0");
                    string sContent = jObj.ToString();
                    var bytes = UnicodeEncoding.UTF8.GetBytes(sContent);
                    reqStream.Write(bytes, 0, bytes.Length);
                    reqStream.Flush();
                }
                using (var repStream = httpRequest.GetResponse().GetResponseStream())
                {
                    using (var reader = new StreamReader(repStream))
                    {
                        return ValidateResult(reader.ReadToEnd());
                    }
                }
            }
            catch { return "err"; }
        }
        static public string sendPost(string url, Dictionary<string, object> param)
        {
            string result = "";
            StringBuilder postData = new StringBuilder();
            if (param != null && param.Count > 0)
            {
                foreach (var p in param)
                {
                    if (postData.Length > 0)
                    {
                        postData.Append("&");
                    }
                    postData.Append(p.Key);
                    postData.Append("=");
                    postData.Append(p.Value);
                }
            }
            byte[] byteData = Encoding.UTF8.GetBytes(postData.ToString());
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = url;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = "POST";
                request.ContentLength = byteData.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        static public string sendPost(string content,string url)
        {
            string result = "";
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Referer = url;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = "POST";
                var bytes = UnicodeEncoding.UTF8.GetBytes(content);
                request.ContentLength = bytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch
            {
                result = "err";
            }
            return result;
        }

        private static string ValidateResult(string responseText)
        {
            if (responseText.StartsWith("response_error:"))
            {
                var failText = responseText.TrimStart("response_error:".ToCharArray());
            }
            return responseText;
        }

        public static string HttpGet(string Url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.CookieContainer = CloudMusicCookie;
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                request.UserAgent= "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Safari/537.36";
                request.Timeout = 30 * 1000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch(Exception ex)
            {
                return "err";
            }

        }
    }
}

