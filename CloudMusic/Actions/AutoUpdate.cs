using Newtonsoft.Json;
using CloudMusic.Models;
using CloudMusic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CloudMusic.Actions
{
    public class AutoUpdate
    {
        readonly static string url = "https://api.github.com/repos/o1298098/CloudMusic/releases/latest";
        private static int size;
        public static async Task<bool> GetUpdate()
        {
            TaskCompletionSource<bool> r = new TaskCompletionSource<bool>();
            string currentVersion = VersionTracking.CurrentVersion;
            string result = CloudMusic.ApiHelper.HttpClient.HttpGet(url);
            if (!result.Equals("err"))
            {
                GitHubReleasesModel releasesModel = JsonConvert.DeserializeObject<GitHubReleasesModel>(result);
                string lastReleasesVersion = releasesModel.tag_name;
                string IgnoreVersion = Preferences.Get("IgnoreVersion", "");
                if (lastReleasesVersion.Equals(IgnoreVersion))
                {
                    r.SetResult(false);
                    return await r.Task;
                }
                string downloadurl = releasesModel.assets[0].browser_download_url;
                size = releasesModel.assets[0].size;
                bool hasupdate = ContainsVersion(currentVersion, lastReleasesVersion);
                if (hasupdate && Device.RuntimePlatform.Equals(Device.Android))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            string content = string.Format("{0}\r\n\r\n大小({1}MB)", releasesModel.body, ((double)size / 1024 / 1024).ToString("f2"));
                            if (await App.Current.MainPage.DisplayAlert("发现新版本" + lastReleasesVersion, content, "更新", "忽略此版本"))
                            {

                                DependencyService.Get<IToast>().ShortAlert("更新已在后台下载,请稍后");
                                System.Net.WebClient webClient = new System.Net.WebClient();
                                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                                webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;
                                byte[] data = await webClient.DownloadDataTaskAsync(downloadurl);
                                string fileName = releasesModel.assets[0].name;
                                DependencyService.Get<IFileOpener>().OpenFile(data, fileName);
                            }
                            else
                            {
                                Preferences.Set("IgnoreVersion", lastReleasesVersion);
                            }
                        }
                        catch(Exception ex)
                        {
                            DependencyService.Get<IToast>().ShortAlert("更新下载失败:"+ex.Message);
                            DependencyService.Get<INotification>().NotificationCancel(1);
                        }
                    });
                }
                else
                {
                    r.SetResult(false);
                    return await r.Task;
                }
            }
            r.SetResult(true);
            return await r.Task;
        }

        private static void WebClient_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            DependencyService.Get<INotification>().NotificationCancel(1);
        }

        private static void WebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            int p = (int)(e.BytesReceived * 100 / size);
            DependencyService.Get<INotification>().ProgressUpdate(1, p);
        }

        static bool ContainsVersion(string now, string releases)
        {
            string[] arr1 = now.Replace("v", "").Split('.');
            string[] arr2 = releases.Replace("v", "").Split('.');
            List<string> v1 = new List<string>();
            List<string> v2 = new List<string>();
            v1.AddRange(arr1);
            v2.AddRange(arr2);
            var arrLen = Math.Max(v1.Count, v2.Count);
            int l = Math.Abs(v1.Count - v2.Count);
            if (v1.Count < v2.Count)
                for (int i = 0; i < l; i++)
                    v1.Add("0");
            else if (v1.Count > v2.Count)
                for (int i = 0; i < l; i++)
                    v2.Add("0");
            if (v1.Count == 0 && v2.Count == 0)
            {
                return false;
            }
            else if (v1.Count == 0)
            {
                return true;
            }
            else if (v2.Count == 0)
            {
                return false;
            }
            for (var i = 0; i < arrLen; i++)
            {
                int result = Comp(int.Parse(v1[i]), int.Parse(v2[i]));
                if (result == 0)
                {
                    continue;
                }
                else
                {
                    if (result == -1)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        static int Comp(int n1, int n2)
        {
            if (n1 > n2)
            {
                return 1;
            }
            else if (n1 < n2)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

    }
}
