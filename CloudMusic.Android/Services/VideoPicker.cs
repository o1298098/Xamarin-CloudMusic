using System;
using System.Threading.Tasks;
using Android.Content;
using FormsVideoLibrary;
using Xamarin.Forms;

// Need application's MainActivity

[assembly: Dependency(typeof(CloudMusic.Droid.VideoPicker))]

namespace CloudMusic.Droid
{
    public class VideoPicker : IVideoPicker
    {
        public Task<string> GetVideoFileAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("video/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = MainActivity.Instance;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Video"),
                MainActivity.PickVideoId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickVideoTaskCompletionSource = new TaskCompletionSource<string>();

            // Return Task object
            return activity.PickVideoTaskCompletionSource.Task;
        }
    }
}