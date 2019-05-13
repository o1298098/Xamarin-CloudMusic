using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CloudMusic.Droid.Services;
using CloudMusic.Models;
using CloudMusic.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CAudioPicker))]
namespace CloudMusic.Droid.Services
{
    public class CAudioPicker : IAudioPicker
    {
        public Task<List<AudioModel>> GetAudioFileAsync()
        {
            return Task.Run(() =>
            {
                Android.Net.Uri mAudioUri = MediaStore.Audio.Media.ExternalContentUri;
                ContentResolver mContentResolver = MainActivity.Instance.ContentResolver;
                ICursor mCursor = mContentResolver.Query(mAudioUri, new string[]{
                                MediaStore.Audio.Media.InterfaceConsts.Data,
                                MediaStore.Audio.Media.InterfaceConsts.DisplayName,
                                MediaStore.Audio.Media.InterfaceConsts.DateAdded,
                                MediaStore.Audio.Media.InterfaceConsts.Id,
                                MediaStore.Audio.Media.InterfaceConsts.MimeType,
                                MediaStore.Audio.Media.InterfaceConsts.Album,
                                //MediaStore.Audio.Media.InterfaceConsts.AlbumArt,
                                MediaStore.Audio.Media.InterfaceConsts.Artist,
                                 MediaStore.Audio.Media.InterfaceConsts.ArtistId,
                                 MediaStore.Audio.Media.InterfaceConsts.Duration
                },
                            null,
                            null,
                            MediaStore.Audio.Media.InterfaceConsts.DisplayName);
                List<AudioModel> audios = new List<AudioModel>();
                while (mCursor.MoveToNext())
                {
                    string path = mCursor.GetString(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Data));
                    string name = mCursor.GetString(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.DisplayName));
                    long time = mCursor.GetLong(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.DateAdded));
                    string mimetype = mCursor.GetString(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.MimeType));
                    string album = mCursor.GetString(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Album));
                    string albumArt = "";// mCursor.GetString(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.AlbumArt));
                    string albumId = "";// mCursor.GetString(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.AlbumId));
                    string artist = mCursor.GetString(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Artist));
                    string artistId = mCursor.GetString(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.ArtistId));
                    long duration = mCursor.GetLong(mCursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Duration));
                    audios.Add(new AudioModel(path, time, name, mimetype, album, albumArt, albumId, artist, artistId, duration));
                }
                mCursor.Close();
                return audios;
            });
        }
    }
}