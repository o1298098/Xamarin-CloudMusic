using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using CloudMusic.CustomForms;
using CloudMusic.iOS.renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomListview), typeof(CustomListviewRenderer))]

namespace CloudMusic.iOS.renderers
{
    public class CustomListviewRenderer : ListViewRenderer
    {
        private WrapperSource DataSource;
        private bool _isDisposing = false;
        protected override void Dispose(bool disposing)
        {
            if (disposing && this._isDisposing == false)
            {
                this._isDisposing = true;
                DisposeDataSource();
            }
            base.Dispose(disposing);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            var customListview = Element as CustomListview;
            if (customListview == null)
                return;

            if (this.Control != null && this._isDisposing == false && this.Element != null)
            {
                this.Control.MultipleTouchEnabled = true;
                DataSource = new WrapperSource(Control.Source, Control, 55);
                DataSource._LastYPosition = 0;
                //DataSource._RemoveListViewInset = Source.RemoveInset;
                Control.Source = DataSource;
                DataSource.OnSwipe += OnSwipe;
                DataSource.OnScrollEvent += OnScrollEvent;
            }
            if (Control != null)
            {
                var tvDelegate = new TableViewDelegate();
                Control.Delegate = tvDelegate;
                tvDelegate.OnScrolled += TvDelegate_OnScrolled;
                tvDelegate.OnDecelerationStarted += (s, ev) =>
                {
                    CustomListview.OnScrollStateChanged(customListview,
                        new ScrollStateChangedEventArgs(ScrollStateChangedEventArgs.ScrollState.Running));
                };

                tvDelegate.OnDecelerationEnded += (s, ev) =>
                {
                    CustomListview.OnScrollStateChanged(customListview,
                        new ScrollStateChangedEventArgs(ScrollStateChangedEventArgs.ScrollState.Idle));
                };

                tvDelegate.OnRowSelected += (s, ev) =>
                {
                    var index = s as NSIndexPath;
                    CustomListview.OnItemTappedIOS(customListview, new ItemTappedIOSEventArgs(index.Row));
                };
            }
        }

        private void TvDelegate_OnScrolled(object sender, EventArgs e)
        {
            CustomListview.OnScrollChanged(Source, new CustomForms.ScrollChangedEventArgs(0, 0, 0, (int)(-Control.ContentOffset.Y* Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density)));
            CustomListview.OverScrollUpdate(Source, (float)(-Control.ContentOffset.Y / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density));
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomListview.OverScrollProperty.PropertyName)
                Control.Bounces = ((CustomListview)Element).OverScroll;
        }
        /// <summary>
        /// Problem: Event registration is overwriting existing delegate. Either just use events or your own delegate:
        /// Solution: Create your own delegate and overide the required events
        /// </summary>

        public class TableViewDelegate : UIKit.UITableViewDelegate
        {
            public event EventHandler OnDecelerationEnded;
            public event EventHandler OnDecelerationStarted;
            public event EventHandler OnDidZoom;
            public event EventHandler OnDraggingStarted;
            public event EventHandler OnDraggingEnded;
            public event EventHandler OnScrollAnimationEnded;
            public event EventHandler OnScrolled;
            public event EventHandler OnScrolledToTop;
            public event EventHandler OnRowSelected;
            public event EventHandler OnDidChangeAdjustedContentInse;

            public override void DidChangeAdjustedContentInset(UIScrollView scrollView)
            {
                OnDidChangeAdjustedContentInse?.Invoke(scrollView,null);
            }

            public override void DecelerationEnded(UIKit.UIScrollView scrollView)
            {
                OnDecelerationEnded?.Invoke(scrollView, null);
            }
            public override void DecelerationStarted(UIKit.UIScrollView scrollView)
            {
                OnDecelerationStarted?.Invoke(scrollView, null);
            }
            public override void DidZoom(UIKit.UIScrollView scrollView)
            {
                OnDidZoom?.Invoke(scrollView, null);
            }
            public override void DraggingStarted(UIKit.UIScrollView scrollView)
            {
                OnDraggingStarted?.Invoke(scrollView, null);
            }
            public override void DraggingEnded(UIKit.UIScrollView scrollView, bool willDecelerate)
            {
                OnDraggingEnded?.Invoke(scrollView, null);
            }
            public override void ScrollAnimationEnded(UIKit.UIScrollView scrollView)
            {
                OnScrollAnimationEnded?.Invoke(scrollView, null);
            }
            public override void Scrolled(UIKit.UIScrollView scrollView)
            {
                OnScrolled?.Invoke(scrollView, null);
            }
            public override void ScrolledToTop(UIKit.UIScrollView scrollView)
            {
                OnScrolledToTop?.Invoke(scrollView, null);
            }
            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                OnRowSelected?.Invoke(indexPath, null);
            }
        }
        private Int32 CheckTotal()
        {
            if (this.Control != null && this._isDisposing == false)
            {
                var totalrows = 0;
                for (int i = 0; i < this.Control.NumberOfSections(); i++)
                {
                    totalrows += (int)this.Control.NumberOfRowsInSection(i);
                }
                return totalrows;
            }
            return 0;
        }

        /// <summary>
        /// returns [0]: section [1] row
        /// </summary>
        /// <returns>The row index.</returns>
        /// <param name="row">Row.</param>
        private nint[] GetRowIndex(Int32 row)
        {
            if (this.Control != null && this._isDisposing == false)
            {
                nint[] pos = new nint[] { 0, 0 };
                nint total = 0;
                nint tempRow = row;
                nint NumberSections = this.Control.NumberOfSections();

                for (int i = 0; i < NumberSections; i++)
                {
                    var c = this.Control.NumberOfRowsInSection(i);
                    total += c;
                    if (total > row)
                    {
                        //total = total - c + row;
                        pos = new nint[] { i, total - c + row };
                        pos = new nint[] { i, tempRow };
                        break;
                    }
                    else if (total == row)
                    {
                        if (NumberSections - 1 >= i + 1)
                        {
                            pos = new nint[] { i + 1, 0 };
                            break;
                        }
                    }
                    tempRow = tempRow - c;
                }
                if (pos[0] == 0 && pos[1] == 0)
                {
                    // last Row
                    pos = new nint[] {
                        NumberSections - 1,
                        this.Control.NumberOfRowsInSection (NumberSections - 1) - 1
                    };
                }
                return pos;
            }
            return new nint[] { 0, 0 };
        }

        protected internal CustomListview Source
        {
            get
            {
                return this.Element as CustomListview;
            }
        }



        private void SetDataSource()
        {
            var source = Source;
            if (source != null && this._isDisposing == false)
            {
                if (this.Control != null && source.HandleScroll)
                {
                    this.Control.ReloadData();
                }
            }
        }

        private bool DisposeDataSource()
        {
            if (DataSource != null && this._isDisposing == false)
            {
                DataSource.OnSwipe -= OnSwipe;
                DataSource.OnScrollEvent -= OnScrollEvent;
                DataSource.Dispose();
                DataSource = null;
                this.Control.Source = null;
                return true;
            }
            return false;
        }

        private void OnScrollEvent(object sender, TableSourceEvents e)
        {
            var source = Source;
            if (source != null && this._isDisposing == false)
            {
                source.FireScrollEvent(new ScrollEventArgs
                {
                    iOSFirstCellVisible = e.FirstViewCell,
                    iOSLastCellVisible = e.LastViewCell,
                    iOSFirstPositionVisible = e.FirstRow,
                    iOSLastPositionVisible = e.LastRow,
                    iOSFirstRowVisible = e.IsFirstRowVisible,
                    iOSLastRowVisible = e.IsLastRowVisible,
                    YPosition = e.Y
                });
            }
        }

        private void OnSwipe(bool up, double y, double lastPosition)
        {
            var source = Source;
            if (source != null && this._isDisposing == false)
            {
                source.FireTopSwipe((up) ? CustomListview.Swipe.UpSwipe : CustomListview.Swipe.DownSwipe, y, lastPosition);
            }
        }
    }
}
