using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Content;
using System.IO;
using Prism;
using Prism.Ioc;

namespace CloudMusic.Droid
{
    [Activity(Label = "CloudMusic", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Instance = this;
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
                setToImmersiveMode();
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.ClearFlags(WindowManagerFlags.Fullscreen);
            }
            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            MediaManager.CrossMediaManager.Current.Init();
            InitDownloadManager();
            Android.Glide.Forms.Init();
            PanCardView.Droid.CardsViewRenderer.Preserve();
            XF.Material.Droid.Material.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            Lottie.Forms.Droid.AnimationViewRenderer.Init();
            //FFImageLoading.Forms.Platform.CachedImageRenderer.InitImageViewHandler();
            Services.CrossPrint.Init(this);
            //Plugin.FirebaseAnalytics.FirebaseAnalytics.Init(this);
            Services.CNotification.Init();
            LoadApplication(new App(new AndroidInitializer()));
        }
        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            setToImmersiveMode();
        }
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            if (hasFocus)
            {
                setToImmersiveMode();
            }
        }
        void InitDownloadManager()
        {
            // Define where the files should be stored. MUST be an external storage. (see https://github.com/SimonSimCity/Xamarin-CrossDownloadManager/issues/10)
            // If you skip this, you neither need the permission `WRITE_EXTERNAL_STORAGE`.

            Plugin.DownloadManager.CrossDownloadManager.Current.PathNameForDownloadedFile = new Func<Plugin.DownloadManager.Abstractions.IDownloadFile, string>(file =>
            {
                string fileName = file.Headers["name"] + "." + file.Headers["type"];
                var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).AbsolutePath;
                return Path.Combine(path, fileName);
            });

            // In case you want to create your own notification :)
            //(CrossDownloadManager.Current as DownloadManagerImplementation).NotificationVisibility = DownloadVisibility.Hidden;

            // Prevents the file from appearing in the android download manager
            (Plugin.DownloadManager.CrossDownloadManager.Current as Plugin.DownloadManager.DownloadManagerImplementation).IsVisibleInDownloadsUi = true;
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            //Plugin.FirebasePushNotification.FirebasePushNotificationManager.ProcessIntent(this, intent);
            Microsoft.AppCenter.Push.Push.CheckLaunchedFromNotification(this, intent);
        }
        public override void OnTrimMemory(TrimMemory level)
        {
            FFImageLoading.ImageService.Instance.InvalidateMemoryCache();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            base.OnTrimMemory(level);
        }
        public override void OnLowMemory()
        {
            FFImageLoading.ImageService.Instance.InvalidateMemoryCache();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            base.OnLowMemory();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public static readonly int PickImageId = 1000;
        public static readonly int CorpImageId = 2000;
        public static readonly int PickVideoId = 3000;
        public static readonly int PickAudioId = 4000;
        public TaskCompletionSource<string> imguri { get; set; }
        public TaskCompletionSource<string> PickVideoTaskCompletionSource { set; get; }
        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {

                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Services.CPicturePicker.startCrop(uri);
                }
            }
            else if (requestCode == CorpImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    string path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures) + "/OFA_UserIcon.png";
                    imguri.SetResult(path);
                }
                else
                {
                    imguri.SetResult("");
                }
            }
            else if (requestCode == PickVideoId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    // Set the filename as the completion of the Task
                    PickVideoTaskCompletionSource.SetResult(intent.DataString);
                }
                else
                {
                    PickVideoTaskCompletionSource.SetResult(null);
                }
            }
            else if (requestCode == PickAudioId)
            { }
        }
        private void setToImmersiveMode()
        {
            var newUiOptions = (int)SystemUiFlags.LayoutStable;
            newUiOptions |= (int)SystemUiFlags.LayoutFullscreen;
            newUiOptions |= (int)SystemUiFlags.HideNavigation;
            newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
        }
    }
}