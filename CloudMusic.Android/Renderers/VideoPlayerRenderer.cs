using System;
using System.ComponentModel;
using System.IO;

using Android.Content;
using Android.Widget;
using ARelativeLayout = Android.Widget.RelativeLayout;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FormsVideoLibrary;
using Com.Google.Android.Exoplayer2.UI;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Source.Smoothstreaming;
using Android.OS;
using Com.Google.Android.Exoplayer2.Source.Hls;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Extractor;
using Android.Views;
using Android.App;
using Android.Support.V4.Content;
using Com.Google.Android.Exoplayer2.Util;
using Java.Lang;
using Com.Google.Android.Exoplayer2.Source.Hls.Playlist;
using System.Collections.Generic;
using Com.Google.Android.Exoplayer2.Offline;
using Com.Google.Android.Exoplayer2.Source.Smoothstreaming.Manifest;
using static Com.Google.Android.Exoplayer2.Offline.DownloadAction;
using Com.Google.Android.Exoplayer2.Source.Dash.Offline;
using Com.Google.Android.Exoplayer2.Source.Hls.Offline;
using Com.Google.Android.Exoplayer2.Source.Smoothstreaming.Offline;
using Com.Google.Android.Exoplayer2.Upstream.Cache;

[assembly: ExportRenderer(typeof(FormsVideoLibrary.VideoPlayer),
                          typeof(CloudMusic.Droid.VideoPlayerRenderer))]

namespace CloudMusic.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        SimpleExoPlayer ExoPlayer;
        PlayerView PlayerView;
        bool isPrepared;
        Android.Widget.ImageView mFullScreen;
        private static bool mExoPlayerFullscreen = false;
        private Dialog mFullScreenDialog;
        ViewGroup mainpage;
        ARelativeLayout relativeLayout;
        private const string DOWNLOAD_ACTION_FILE = "actions";
        private const string DOWNLOAD_TRACKER_ACTION_FILE = "tracked_actions";
        private const string DOWNLOAD_CONTENT_DIRECTORY = "downloads";
        private const int MAX_SIMULTANEOUS_DOWNLOADS = 2;
        private Deserializer[] DOWNLOAD_DESERIALIZERS =
          new Deserializer[] {
            DashDownloadAction.Deserializer,
            HlsDownloadAction.Deserializer,
            SsDownloadAction.Deserializer,
            ProgressiveDownloadAction.Deserializer
        };

        protected string userAgent;

        public VideoPlayerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    
                    // Save the VideoView for future reference
                    InitializePlayer();
                    //videoView = new VideoView(Context);
                   
                    // Put the VideoView in a RelativeLayout
                    relativeLayout = new ARelativeLayout(Context);
                    //relativeLayout.AddView(videoView);
                    relativeLayout.AddView(PlayerView);
                    // Center the VideoView in the RelativeLayout
                    ARelativeLayout.LayoutParams layoutParams =
                        new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                    layoutParams.AddRule(LayoutRules.CenterInParent);
                   // videoView.LayoutParameters = layoutParams;
                    PlayerView.LayoutParameters = layoutParams;
                    PlayerView.SetShowBuffering(true);
                    PlayerView.ControllerAutoShow = false;
                   // Handle a VideoView event
                   // videoView.Prepared += OnVideoViewPrepared;
                   SetNativeControl(relativeLayout);
                    mFullScreen=PlayerView.FindViewById<Android.Widget.ImageView>(Resource.Id.exo_fullscreen_icon);
                    mainpage = ((ViewGroup)PlayerView.Parent);
                    initFullscreenDialog();
                    mFullScreen.Click += MFullScreen_Click;
                }

               
                SetAreTransportControlsEnabled();
                SetSource();

                args.NewElement.UpdateStatus += OnUpdateStatus;
                args.NewElement.PlayRequested += OnPlayRequested;
                args.NewElement.PauseRequested += OnPauseRequested;
                args.NewElement.StopRequested += OnStopRequested;
            }

            if (args.OldElement != null)
            {
                args.OldElement.UpdateStatus -= OnUpdateStatus;
                args.OldElement.PlayRequested -= OnPlayRequested;
                args.OldElement.PauseRequested -= OnPauseRequested;
                args.OldElement.StopRequested -= OnStopRequested;
            }
        }

        private void MFullScreen_Click(object sender, EventArgs e)
        {
            if (!mExoPlayerFullscreen)
                FullscreenDialog();
            else
                closeFullscreenDialog();
        }

        private void MFullScreenExit_Click(object sender, EventArgs e)
        {
            closeFullscreenDialog();
        }

        private void initFullscreenDialog()
        {

            mFullScreenDialog = new Dialog(Context, Android.Resource.Style.ThemeBlackNoTitleBarFullScreen);
            mFullScreenDialog.KeyPress += MFullScreenDialog_KeyPress;
        }

        private void MFullScreenDialog_KeyPress(object sender, DialogKeyEventArgs e)
        {
            if (e.KeyCode == Keycode.Back)
            {
                if (mExoPlayerFullscreen)
                    closeFullscreenDialog();
            }
        }
        private  void closeFullscreenDialog()
        {

            ((ViewGroup)PlayerView.Parent).RemoveView(PlayerView);
            mainpage.AddView(PlayerView);
            ((Activity)Context).RequestedOrientation = Android.Content.PM.ScreenOrientation.SensorPortrait;
            mFullScreen.SetImageDrawable(ContextCompat.GetDrawable(Context, Resource.Drawable.exo_controls_fullscreen_enter));
            mExoPlayerFullscreen = false;
            mFullScreenDialog.Dismiss();
        }
        private void FullscreenDialog()
        {
            mainpage.RemoveView(PlayerView);
            ((Activity)Context).RequestedOrientation=Android.Content.PM.ScreenOrientation.SensorLandscape;
            mFullScreenDialog.AddContentView(PlayerView, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            mFullScreen.SetImageDrawable(ContextCompat.GetDrawable(Context, Resource.Drawable.exo_controls_fullscreen_exit));
            mExoPlayerFullscreen = true;
            mFullScreenDialog.Show();
        }
        private void InitializePlayer()
        {
            ExoPlayer = ExoPlayerFactory.NewSimpleInstance(Context, new DefaultTrackSelector());
            PlayerView = new PlayerView(Context) { Player = ExoPlayer};
        }

        protected override void Dispose(bool disposing)
        {
            //if (Control != null && videoView != null)
            //{
            //    videoView.Prepared -= OnVideoViewPrepared;
            //}
            if (Element != null)
            {
                Element.UpdateStatus -= OnUpdateStatus;
            }
            ExoPlayer.Stop();
            ExoPlayer.Release();
            PlayerView.Dispose();
            base.Dispose(disposing);
        }

        void OnVideoViewPrepared(object sender, EventArgs args)
        {
            isPrepared = true;
            //((IVideoPlayerController)Element).Duration = TimeSpan.FromMilliseconds(videoView.Duration);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == VideoPlayer.AreTransportControlsEnabledProperty.PropertyName)
            {
                SetAreTransportControlsEnabled();
            }
            else if (args.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                SetSource();
            }
            else if (args.PropertyName == VideoPlayer.PositionProperty.PropertyName)
            {
                if (System.Math.Abs(ExoPlayer.CurrentPosition - Element.Position.TotalMilliseconds) > 1000)
                {
                    ExoPlayer.SeekTo((int)Element.Position.TotalMilliseconds);
                }
            }
            else if (args.PropertyName == VideoPlayer.IsVisibleProperty.PropertyName)
            {
                if (!Element.IsVisible)
                {
                    ExoPlayer.Stop();
                }  
            }
        }

        void SetAreTransportControlsEnabled()
        {
            //if (Element.AreTransportControlsEnabled)
            //{
            //    mediaController = new MediaController(Context);
            //    mediaController.Visibility = Android.Views.ViewStates.Visible;
            //    mediaController.SetMediaPlayer(videoView);
            //    videoView.SetMediaController(mediaController);
            //}
            //else
            //{
            //    videoView.SetMediaController(null);

            //    if (mediaController != null)
            //    {
            //        mediaController.SetMediaPlayer(null);
            //        mediaController = null;
            //    }
            //}
        }

        void SetSource()
        {
            isPrepared = false;
            bool hasSetSource = false;
            DefaultHttpDataSourceFactory httpDataSourceFactory = new DefaultHttpDataSourceFactory("1");
            DefaultSsChunkSource.Factory ssChunkFactory = new DefaultSsChunkSource.Factory(httpDataSourceFactory);
            Handler emptyHandler = new Handler();
            IMediaSource videoSource = null;
            if (Element.Source is HLSVideoSource)
            {
                string uri = (Element.Source as HLSVideoSource).Uri;

                if (!string.IsNullOrWhiteSpace(uri))
                {
                    //videoView.SetVideoURI(Android.Net.Uri.Parse(uri));
                    videoSource = new HlsMediaSource.Factory(httpDataSourceFactory)
                        .CreateMediaSource(Android.Net.Uri.Parse(uri));
                    hasSetSource = true;
                }
            }
            else if (Element.Source is UriVideoSource)
            {
                string uri = (Element.Source as UriVideoSource).Uri;

                if (!string.IsNullOrWhiteSpace(uri))
                {
                    //videoView.SetVideoURI(Android.Net.Uri.Parse(uri));
                    var dataSourceFactory = new DefaultDataSourceFactory(Context,Util.GetUserAgent(Context, "Multimedia"));
                    videoSource = new ExtractorMediaSource.Factory(dataSourceFactory).CreateMediaSource(Android.Net.Uri.Parse(uri));
                    hasSetSource = true;
                }
            }
            else if (Element.Source is FileVideoSource)
            {
                string filename = (Element.Source as FileVideoSource).File;

                if (!string.IsNullOrWhiteSpace(filename))
                {
                    DataSpec dataSpec = new DataSpec(Android.Net.Uri.FromFile(new Java.IO.File(filename)));
                    FileDataSource fileDataSource = new FileDataSource();
                    try
                    {
                        fileDataSource.Open(dataSpec);
                    }
                    catch (FileDataSource.FileDataSourceException e)
                    {
                        e.PrintStackTrace();
                    }
                    // videoView.SetVideoPath(filename);
                    IDataSourceFactory dataSourceFactory = new DefaultDataSourceFactory(this.Context,"CloudMusic");
                    videoSource = new ExtractorMediaSource.Factory(dataSourceFactory).CreateMediaSource(fileDataSource.Uri);
                     hasSetSource = true;
                }
            }
            else if (Element.Source is ResourceVideoSource)
            {
                string package = Context.PackageName;
                string path = (Element.Source as ResourceVideoSource).Path;

                if (!string.IsNullOrWhiteSpace(path))
                {
                    string filename = Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
                    string uri = "android.resource://" + package + "/raw/" + filename;
                    //videoView.SetVideoURI(Android.Net.Uri.Parse(uri));
                    videoSource = new SsMediaSource.Factory(new DefaultSsChunkSource.Factory(httpDataSourceFactory), BuildDataSourceFactory(new DefaultBandwidthMeter()))
                        .SetManifestParser(new FilteringManifestParser(new SsManifestParser(), null))
                        .CreateMediaSource(Android.Net.Uri.Parse(uri));
                    hasSetSource = true;
                }
            }
            if (videoSource != null)
                ExoPlayer.Prepare(videoSource);
            if (hasSetSource && Element.AutoPlay)
            {
                ExoPlayer.PlayWhenReady = true;
                // videoView.Start();
            }
        }

        // Event handler to update status
       void OnUpdateStatus(object sender, EventArgs args)
        {
            VideoStatus status = VideoStatus.NotReady;

            if (isPrepared)
            {
                switch (ExoPlayer.PlaybackState)
                {

                    case Player.StateIdle:
                        status = VideoStatus.Paused;
                        break;
                    case Player.StateEnded:
                        status = VideoStatus.Ended;
                        break;
                    case Player.StateReady:
                        status = VideoStatus.Playing;
                        break;

                }
            }

            ((IVideoPlayerController)Element).Status = status;

            // Set Position property
           // TimeSpan timeSpan = TimeSpan.FromMilliseconds(videoView.CurrentPosition);
           // ((IElementController)Element).SetValueFromRenderer(VideoPlayer.PositionProperty, timeSpan);
        }

        private void play()
        {
            Android.Net.Uri sourceUri = Android.Net.Uri.Parse(Element.Source.ToString());
            DefaultHttpDataSourceFactory httpDataSourceFactory = new DefaultHttpDataSourceFactory("1");
            DefaultSsChunkSource.Factory ssChunkFactory = new DefaultSsChunkSource.Factory(httpDataSourceFactory);
            Handler emptyHandler = new Handler();
            //SsMediaSource ssMediaSource = new SsMediaSource(sourceUri, httpDataSourceFactory, ssChunkFactory, emptyHandler,null);
            //ExoPlayer.Prepare(ssMediaSource);
        }

        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            // videoView.Start();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
           // videoView.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
           // videoView.StopPlayback();
            ExoPlayer.Stop();
        }
        public IDataSourceFactory BuildDataSourceFactory(ITransferListener listener)
        {
            DefaultDataSourceFactory upstreamFactory = new DefaultDataSourceFactory(Context, listener, BuildHttpDataSourceFactory(listener));

            return BuildReadOnlyCacheDataSource(upstreamFactory, null);
        }
        public IHttpDataSourceFactory BuildHttpDataSourceFactory(ITransferListener listener)
        {
            return new DefaultHttpDataSourceFactory(userAgent, listener);
        }
        private static CacheDataSourceFactory BuildReadOnlyCacheDataSource(
           DefaultDataSourceFactory upstreamFactory, ICache cache)
        {
            return new CacheDataSourceFactory(
                cache,
                upstreamFactory,
                new FileDataSourceFactory(),
                /* cacheWriteDataSinkFactory= */ null,
                CacheDataSource.FlagIgnoreCacheOnError,
                /* eventListener= */ null);
        }


    }
}