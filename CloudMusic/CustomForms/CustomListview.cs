using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.CustomForms
{
    public class CustomListview : ListView
    {
        public CustomListview() { }

        public CustomListview(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy) { }
        public delegate void ScrollTouched(object sender);
        public delegate void ScrollEvent(object sender, ScrollEventArgs args);
        public delegate void SwipeEvent(object sender, Swipe swipe, double YPosition, double LastTop);
        public ScrollEvent OnScroll;
        public SwipeEvent OnTopSwipe;
        public ScrollTouched OnScrollTouched;
        public event EventHandler<ScrollStateChangedEventArgs> ScrollStateChanged;
        public event EventHandler<ScrollChangedEventArgs> ScrollChanged;
        public enum Swipe
        {
            DownSwipe,
            UpSwipe,
            None
        }
        public new static readonly BindableProperty ItemsSourceProperty =
          BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(CustomListview), null,
              propertyChanged: CustomListview.ItemsSourceChanged);

        public static readonly BindableProperty TopSwipeProperty =
            BindableProperty.Create("TopSwipe", typeof(Swipe), typeof(CustomListview), Swipe.None);

        public static readonly BindableProperty ScrollToPositionProperty =
            BindableProperty.Create("ScrollToPosition", typeof(Int32), typeof(CustomListview), -1, BindingMode.TwoWay);

        public static readonly BindableProperty HandleScrollProperty =
            BindableProperty.Create("HandleScroll", typeof(bool), typeof(CustomListview), false);

        public bool HandleScroll
        {
            get { return (bool)GetValue(HandleScrollProperty); }
            set { SetValue(HandleScrollProperty, value); }
        }

        public Int32 ScrollToPosition
        {
            get { return (Int32)GetValue(ScrollToPositionProperty); }
            set
            {
                if (value == ScrollToPosition)
                {
                    this.OnPropertyChanged();
                }
                else
                {
                    SetValue(ScrollToPositionProperty, value);
                }
            }
        }

        public Swipe TopSwipe
        {
            get { return (Swipe)GetValue(TopSwipeProperty); }
            set { SetValue(TopSwipeProperty, value); }
        }
        public void FireTopSwipe(Swipe swipe, double YPosition, double LastTop)
        {
            var handler = OnTopSwipe;
            if (handler != null)
            {
                handler(this, swipe, YPosition, LastTop);
            }
        }

        public void FireScrollEvent(ScrollEventArgs args)
        {
            var handler = OnScroll;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void FireScrollTouched()
        {
            var handler = OnScrollTouched;
            if (handler != null)
            {
                handler(this);
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = ((CustomListview)bindable);
            obj.ItemsSource = newValue as IEnumerable;
            var obs = newValue as INotifyCollectionChanged;
            if (obs != null)
            {
                var src = obj.ItemsSource;
                obj.ItemsSource = null;
                obj.ItemsSource = null;
                obj.ItemsSource = src;
                obj.ItemsSource = obj.ItemsSource;
            }
        }

        private void Update(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Update();
        }

        private void Update()
        {
            var src = this.ItemsSource;
            this.ItemsSource = null;
            this.ItemsSource = null;
            this.ItemsSource = src;
            this.ItemsSource = this.ItemsSource;
        }
        public static void OnScrollStateChanged(object sender, ScrollStateChangedEventArgs e)
        {
            var customListview = (CustomListview)sender;
            customListview.ScrollStateChanged?.Invoke(customListview, e);
        }
        public static void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var customListview = (CustomListview)sender;
            customListview.ScrollChanged?.Invoke(customListview, e);
        }

        public event EventHandler<ItemTappedIOSEventArgs> ItemTappedIOS;
        public static void OnItemTappedIOS(object sender, ItemTappedIOSEventArgs e)
        {
            var customListview = (CustomListview)sender;
            customListview.ItemTappedIOS?.Invoke(customListview, e);
        }
    }
    public class ItemTappedIOSEventArgs : EventArgs
    {
        public ItemTappedIOSEventArgs(int index)
        {
            SelectedIndex = index;
        }
        public int SelectedIndex { get; set; }
    }
    public class ScrollChangedEventArgs : EventArgs
    {
        public ScrollChangedEventArgs(int oldx,int oldy,int newx,int newy)
        {
            this.OldScrollX = oldx;
            this.OldScrollY = oldy;
            this.NewScrollX = newx;
            this.NewScrollY = newy;
        }

        public int OldScrollX { get; set; }
        public int OldScrollY { get; set; }
        public int NewScrollX { get; set; }
        public int NewScrollY { get; set; }
    }

    public class ScrollStateChangedEventArgs : EventArgs
    {
        public ScrollStateChangedEventArgs(ScrollState scrollState)
        {
            this.CurScrollState = scrollState;
        }

        public enum ScrollState
        {
            Idle = 0,
            Running = 1
        }

        public ScrollState CurScrollState { get; }
    }
    public class ScrollEventArgs
    {
        public ScrollEventArgs()
        {
            this.OS = Device.RuntimePlatform;
            this.AndroidFirstPostionVisible = 0;
        }

        /// <summary>
        /// string OS = Device.[constant]
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// Android First RowIndex Visible
        /// </summary>
        public Int32 AndroidFirstPostionVisible { get; set; }

        /// <summary>
        /// Length: 1:  [0]: section [1]: row
        /// </summary>
        public Int32[] iOSFirstPositionVisible { get; set; }

        /// <summary>
        /// Length: 1:  [0]: section [1]: row
        /// </summary>
        public Int32[] iOSLastPositionVisible { get; set; }

        public bool iOSFirstRowVisible { get; set; }

        public bool iOSLastRowVisible { get; set; }

        public ViewCell iOSFirstCellVisible { get; set; }

        public ViewCell iOSLastCellVisible { get; set; }

        /// <summary>
        /// Y position
        /// </summary>
        public double YPosition { get; set; }
    }
}
