using System;
using System.Collections.Generic;
using System.IO;
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

[assembly: Dependency(typeof(CPicturePicker))]
namespace CloudMusic.Droid.Services
{
    public class CPicturePicker : IPicturePicker
    {
       static Java.IO.File sdcardTempFile;
        public Task<string> GetImageUriAsync()
        {
            
            Intent intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            intent.SetType("image/*");
            MainActivity.Instance.StartActivityForResult(
                intent,
                MainActivity.PickImageId);
            MainActivity.Instance.imguri = new TaskCompletionSource<string>();
            return MainActivity.Instance.imguri.Task;
        }
        public Task<List<ImageModel>> GetAllImageUriAsync()
        {
            return Task.Run(() =>
            {
                Android.Net.Uri mImageUri = MediaStore.Images.Media.ExternalContentUri;
                ContentResolver mContentResolver = MainActivity.Instance.ContentResolver;
                ICursor mCursor = mContentResolver.Query(mImageUri, new string[]{
                                MediaStore.Images.Media.InterfaceConsts.Data,
                                MediaStore.Images.Media.InterfaceConsts.DisplayName,
                                MediaStore.Images.Media.InterfaceConsts.DateAdded,
                                MediaStore.Images.Media.InterfaceConsts.Id,
                                MediaStore.Images.Media.InterfaceConsts.MimeType},
                            null,
                            null,
                            MediaStore.Images.Media.InterfaceConsts.DateAdded + " DESC");
                List<ImageModel> images = new List<ImageModel>();
                while (mCursor.MoveToNext())
                {
                    string path = mCursor.GetString(
                            mCursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Data));
                    string name = mCursor.GetString(
                            mCursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.DisplayName));
                    long time = mCursor.GetLong(
                            mCursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.DateAdded));
                    string mimetype = mCursor.GetString(
                           mCursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.MimeType));
                    images.Add(new ImageModel(path, time, name, mimetype));
                }
                mCursor.Close();
                // Return Task object
                return images;
            });
        }
        public static void startCrop(Android.Net.Uri uri)
        {
            sdcardTempFile = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "OFA_UserIcon.png");
            Intent intent = new Intent("com.android.camera.action.CROP");
            intent.SetDataAndType(uri, "image/*");
            intent.PutExtra("crop", true);
            intent.PutExtra("aspectX", 1);
            intent.PutExtra("aspectY", 1);
            intent.PutExtra("outputX", 180);
            intent.PutExtra("outputY", 180);
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(sdcardTempFile));
            MainActivity.Instance.StartActivityForResult(intent,MainActivity.CorpImageId);
        }
    }
}