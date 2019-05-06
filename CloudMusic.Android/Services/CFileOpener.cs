using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using CloudMusic.Droid.Services;
using CloudMusic.Services;
[assembly: Xamarin.Forms.Dependency(typeof(CFileOpener))]

namespace CloudMusic.Droid.Services
{
    class CFileOpener : IFileOpener
    {
        public void OpenFile(byte[] data, string name)
        {

            string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(directory, name);
            foreach (string file in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(file) == ".apk")
                    File.Delete(file);
            }
            File.WriteAllBytes(path, data);
            Intent intent = new Intent(Intent.ActionView);
            Android.Net.Uri fileUri = null;
            if (Build.VERSION.SdkInt <= BuildVersionCodes.N|| Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                string[] command = { "chmod", "777", path };
                Java.Lang.ProcessBuilder builder = new Java.Lang.ProcessBuilder(command);
                try
                {
                    builder.Start();
                }
                catch (IOException e)
                {
                }
                fileUri = Android.Net.Uri.FromFile(new Java.IO.File(path));
                intent.SetFlags(ActivityFlags.NewTask);
            }
            else
            {
                fileUri = FileProvider.GetUriForFile(Application.Context, "CloudMusic.CloudMusic", new Java.IO.File(path));
                intent.SetFlags(ActivityFlags.GrantReadUriPermission);
            }               
            intent.SetDataAndType(fileUri, "application/vnd.android.package-archive");
            Application.Context.StartActivity(intent);
        }


    }
}