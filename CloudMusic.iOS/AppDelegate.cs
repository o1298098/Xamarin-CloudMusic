using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Prism;
using Prism.Ioc;

namespace CloudMusic.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init();
            Xamarin.Forms.FormsMaterial.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Syncfusion.XForms.iOS.PopupLayout.SfPopupLayoutRenderer.Init();
            Syncfusion.XForms.iOS.BadgeView.SfBadgeViewRenderer.Init();
            PanCardView.iOS.CardsViewRenderer.Preserve();
            XF.Material.iOS.Material.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            MediaManager.CrossMediaManager.Current.Init();
            LoadApplication(new App(new iOSInitializer()));
            return base.FinishedLaunching(app, options);
        }
        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations
            }
        }
        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, System.Action completionHandler)
        {
            Plugin.DownloadManager.CrossDownloadManager.BackgroundSessionCompletionHandler = completionHandler;
            Plugin.DownloadManager.CrossDownloadManager.Current.PathNameForDownloadedFile=new Func<Plugin.DownloadManager.Abstractions.IDownloadFile, string> (file =>
            {
                string fileName = file.Headers["name"] + "." + file.Headers["type"];
                var path = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
                return System.IO.Path.Combine(path, fileName);
            });
        }
    }
}
