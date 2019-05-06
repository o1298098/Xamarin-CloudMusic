using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CloudMusic.CustomForms;
using CloudMusic.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Widget.AbsListView;

[assembly: ExportRenderer(typeof(CustomListview), typeof(CustomListviewRenderer))]
namespace CloudMusic.Droid.Renderers
{
    public class CustomListviewRenderer : ListViewRenderer, Android.Widget.AbsListView.IOnScrollListener, Android.Views.View.IOnScrollChangeListener
    {
        public CustomListviewRenderer(Context context) : base(context) { }
        private int mLastFirstVisibleItem;
        private bool processingSwipe = false;
        private Dictionary<Int32, Int32> listViewItemHeights = new Dictionary<Int32, Int32>();
        private Double CellHeight = 0;

        private CustomListview Source => this.Element as CustomListview;
        Android.Widget.ListView lw;
        public static void Init() { }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            //Android.Widget.ListView lw = (Android.Widget.ListView)this.Control;
            //var d = lw.FirstVisiblePosition;
           
        }

        public void OnScrollChange(Android.Views.View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
        {
        }
        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {             
            if (lw == null)
            {
                lw = (Android.Widget.ListView)this.Control;
                lw.Scroll += Lw_Scroll;
                lw.ScrollStateChanged += Lw_ScrollStateChanged;
            }
               
            if (scrollState == 0)
            {
                // stopped
            }

            if (processingSwipe == false)
            {
                processingSwipe = true;

                if (lw.Id == view.Id)
                {
                    var yPosition = GetYPosition();
                    int currentFirstVisibleItem = lw.FirstVisiblePosition;
                    if (currentFirstVisibleItem > mLastFirstVisibleItem)
                    {
                        //Down
                        Source.FireTopSwipe(CustomListview.Swipe.DownSwipe, yPosition, currentFirstVisibleItem);
                    }
                    else if (currentFirstVisibleItem < mLastFirstVisibleItem)
                    {
                        // Up
                        Source.FireTopSwipe(CustomListview.Swipe.UpSwipe, yPosition, currentFirstVisibleItem);
                       
                    }

                    mLastFirstVisibleItem = currentFirstVisibleItem;
                }

                processingSwipe = false;
            }

        }

        private void Lw_ScrollStateChanged(object sender, AbsListView.ScrollStateChangedEventArgs e)
        {
            var currentScrollState = CustomForms.ScrollStateChangedEventArgs.ScrollState.Idle;

            if (e.ScrollState == Android.Widget.ScrollState.Idle)
            {
                currentScrollState = CustomForms.ScrollStateChangedEventArgs.ScrollState.Idle;
            }
            else
            {
                currentScrollState = CustomForms.ScrollStateChangedEventArgs.ScrollState.Running;
            }

            CustomListview.OnScrollStateChanged(Source, new CustomForms.ScrollStateChangedEventArgs(currentScrollState));
        }

        int rrr = 0;
        private void Lw_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
           var ddd=  e.View.GetChildAt(0);
            if(lw.FirstVisiblePosition==0)
                rrr = ddd.Top;
            CustomListview.OnScrollChanged(Source, new CustomForms.ScrollChangedEventArgs(0, 0, 0, rrr));
        }

        private int GetYPosition()
        {

            if (this.Control != null)
            {
                var c = this.Control.GetChildAt(0); //this is the first visible row
                if (c != null)
                {
                    int scrollY = -c.Top;
                    if (listViewItemHeights.ContainsKey(this.Control.FirstVisiblePosition) == false)
                    {
                        CellHeight = c.Height;
                        listViewItemHeights.Add(this.Control.FirstVisiblePosition, c.Height);
                    }
                    for (int i = 0; i < this.Control.FirstVisiblePosition; ++i)
                    {
                        if (listViewItemHeights.ContainsKey(i) && listViewItemHeights[i] != 0)
                            scrollY += listViewItemHeights[i];
                    }
                    return scrollY;
                }
            }
            return 0;
        }

        protected override void OnAttachedToWindow()
        {
            if (this.Control != null)
            {
                this.Control.SetOnScrollChangeListener(this);
                this.Control.SetOnScrollListener(this);
            }
            base.OnAttachedToWindow();
        }

        protected override void OnDetachedFromWindow()
        {
            if (this.Control != null)
            {
                this.Control.SetOnScrollChangeListener(null);
                this.Control.SetOnScrollChangeListener(null);
            }
            base.OnDetachedFromWindow();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
        }
        private int TopElementHeight
        {
            get
            {
                if (this.Control != null)
                {
                    var c = this.Control.GetChildAt(0);
                    return c.Top + c.Height;
                }
                return 0;
            }
        }
    }
}