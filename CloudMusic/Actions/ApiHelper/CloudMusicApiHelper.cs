using Newtonsoft.Json;
using CloudMusic.ApiHelper;
using CloudMusic.Models.ENUM;
using CloudMusic.Models.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CloudMusic.Services;
using System.Net;

namespace CloudMusic.Actions.ApiHelper
{
   public static class CloudMusicApiHelper
    {
       public static readonly string apihost = "http://104.207.135.233:3000";
        public static UserInfo  Login(string phone, string password)
        {
            DependencyService.Get<ICookieStore>().DeleteAllCookiesForSite(apihost);
            HttpClient.CloudMusicCookie = new CookieContainer();
            string param =string.Format("/login/cellphone?phone={0}&password={1}", phone, password);
            var r= ApiGet<UserInfo>(param);
            if(r!=null)
            {
                var q= HttpClient.CloudMusicCookie.GetCookies(new Uri(apihost));
                foreach(Cookie f in q)
                    DependencyService.Get<ICookieStore>().SetCookie(f);
            }
            return r;
        }
        public static MainBanner GetBanners()
        {
            string param = "/banner";
            return ApiGet<MainBanner>(param);
        }
        /// <summary>
        /// 获取推荐歌单
        /// </summary>
        /// <returns></returns>
        public static Personalized GetPersonalized(int limit)
        {
            string param = "/personalized?limit="+limit;
            return ApiGet<Personalized>(param);
        }
        public static string LikeSong(string id,bool like=true)
        {
            string param =string.Format("/like?id={0}&like={1}",id,like);
            return HttpClient.HttpGet(apihost+param);
        }
        public static ArtistsInfo ArtistDetial(string id)
        {
            string param = "/artists?id=" + id;
            return ApiGet<ArtistsInfo>(param);
        }
        public static MusicPlayListDetail PlayListDetial(string id)
        {
            string param = "/playlist/detail?id=" + id;
            return ApiGet<MusicPlayListDetail>(param);
        }
        public static UserPlayLists UserPlayLists(string uid)
        {
            string param = "/user/playlist?uid=" + uid;
            return ApiGet<UserPlayLists>(param);
        }
        public static SubscriptionCount SubscriptionCount()
        {
            string param = "/user/subcount";
            return ApiGet<SubscriptionCount>(param);
        }
        public static NewAlbums GetNewAlbums(int limit, int offset)
        {
            string param = string.Format("/top/album?offset={0}&limit={1}",offset,limit);
            return ApiGet<NewAlbums>(param);
        }
        public static MusicEventModel GetFriendEvents(int offset)
        {
            string param = "/event?offset="+offset;
            return ApiGet<MusicEventModel>(param);
        }
        /// <summary>
        /// 获取推荐歌曲
        /// </summary>
        /// <returns></returns>
        public static TopNewSongs GetNewSongs()
        {
            string param = "/personalized/newsong";
            return ApiGet<TopNewSongs>(param);
        }
        public static PersonalFmModel PersonalFM(int offset)
        {
            string param = "/personal_fm?offset="+ offset;
            return ApiGet<PersonalFmModel>(param);
        }
        /// <summary>
        /// 获取每日推荐歌曲
        /// </summary>
        /// <returns></returns>
        public static RecommendSongs GetRecommendSongs()
        {
            string param = "/recommend/songs";
            return ApiGet<RecommendSongs>(param);
        }
        /// <summary>
        ///获取最近 5 个听了这首歌的用户
        /// </summary>
        /// <param name="id">歌曲id</param>
        /// <returns></returns>
        public static SiMiUsers GetSiMiUsers(string id)
        {
            string param = "/simi/user?id="+id;
            return ApiGet<SiMiUsers>(param);
        }

        public static MusicInfo GetSong(string ids,CouldMusicBpsType bpsType)
        {
            string param =string.Format("/song/url?id={0}&br={1}" ,ids,(int)bpsType);
            return ApiGet<MusicInfo>(param);
        }
        /// <summary>
        /// 获取歌手信息
        /// </summary>
        /// <param name="id">歌手id</param>
        /// <returns></returns>
        public static ArtistsInfo GetArtist(string id)
        {
            string param = "/artists?id=" + id;
            return ApiGet<ArtistsInfo>(param);
        }
        /// <summary>
        /// 获取MV信息
        /// </summary>
        /// <param name="mvid"></param>
        /// <returns></returns>
        public static MVInfoModel GetMusicVideo(string mvid)
        {
            string param = "/mv/detail?mvid=" + mvid;
            return ApiGet<MVInfoModel>(param);
        }
        /// <summary>
        /// 获取相似MV
        /// </summary>
        /// <param name="mvid"></param>
        /// <returns></returns>
        public static SiMiMVModle GetSiMiMusicVideo(string mvid)
        {
            string param = "/simi/mv?mvid=" + mvid;
            return ApiGet<SiMiMVModle>(param);
        }
        /// <summary>
        /// 获取歌曲评论
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="id"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static MusicComment GetSongComment(string limit,string id,int offset)
        {
            string url = "/comment/music?";
            string param1 = string.IsNullOrWhiteSpace(limit) ? "limit=1&" : "limit=" + limit+"&";
            string param2 ="id=" + id;
            string param3= offset==0 ? "" : "&offset=" + offset;
            return ApiGet<MusicComment>(url +param1+ param2+ param3);
        }
        public static MusicComment GetMvComment(string limit, string id, int offset)
        {
            string url = "/comment/mv?";
            string param1 = string.IsNullOrWhiteSpace(limit) ? "limit=1&" : "limit=" + limit + "&";
            string param2 = "id=" + id;
            string param3 = offset == 0 ? "" : "&offset=" + offset;
            return ApiGet<MusicComment>(url + param1 + param2 + param3);
        }
        public static MusicSearchModel Search(string keywords,int limit,int offset, CloudMusicSearchType type)
        {
            if (limit == 0)
                limit = 30;
            string param = string.Format("/search?keywords={0}&limit={1}&offset={2}&type={3}", keywords,limit,offset, (int)type);
            return ApiGet<MusicSearchModel>(param);
        }
        public static MusicSearchAllModel SearchAll(string keywords, int limit, int offset)
        {
            if (limit == 0)
                limit = 30;
            string param = string.Format("/search?keywords={0}&limit={1}&offset={2}&type={3}", keywords, limit, offset, (int)CloudMusicSearchType.All);
            return ApiGet<MusicSearchAllModel>(param);
        }
        public static MusicSearchSuggestModel SearchSuggest(string keywords)
        {
            string param = "/search/suggest?keywords=" + keywords;
            return ApiGet<MusicSearchSuggestModel>(param);
        }
        public static MobileSearchSuggestModel MobileSearchSuggest(string keywords)
        {
            string param = "/search/suggest?type=mobile&keywords=" + keywords;
            return ApiGet<MobileSearchSuggestModel>(param);
        }
        public static VideoUrls GetVideoUrls(string id)
        {
            string param = "/video/url?id=" + id;
            return ApiGet<VideoUrls>(param);
        }
        public static VideoInfo GetVideoInfoByGroup(CouldMusicVideoGroup group,int limit,int offset)
        {
            string param =string.Format("/video/group?id={0}&limit={1}&offset={2}", (int)group,limit,offset);
            return ApiGet<VideoInfo>(param);
        }

        private static T ApiGet<T>(string param)
        {

            T s = default(T);
            try
            {
                string result = HttpClient.HttpGet(apihost + param);
                if (result != "err")
                {
                    s = JsonConvert.DeserializeObject<T>(result);
                }

                return s;
            }
            catch (Exception ex)
            {

                return s;
            }

           
        }
    }
}
