using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;

namespace CloudMusic.iOS.renderers
{
   public class TableSourceEvents
    {
        public TableSourceEvents()
        {

        }

        public double Y { get; set; }

        public bool IsFirstRowVisible { get; set; }

        public bool IsLastRowVisible { get; set; }

        public Int32[] FirstRow { get; set; }

        public Int32[] LastRow { get; set; }

        public Xamarin.Forms.ViewCell FirstViewCell { get; set; }

        public Xamarin.Forms.ViewCell LastViewCell { get; set; }
    }


    internal class WrapperSource : UITableViewSource
    {
        private readonly UITableViewSource original;
        protected internal double _LastYPosition;

        protected internal bool _RemoveListViewInset = false;

        WeakReference _TableView { get; set; }

        private bool _IsFirstRowVisible;
        private bool _IsLastRowVisible;
        private bool _UserSwipeUp;
        private bool _IsStartedScrolling = false;
        private nint _MinRowHeight;

        // Scrolling Events
        public delegate void SwipeEvent(bool up, double y, double lastPosition);

        public event SwipeEvent OnSwipe;
        public event EventHandler<TableSourceEvents> OnScrollEvent;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public WrapperSource(UITableViewSource original, UITableView tableView, Int32 minRowHeight = -1)
        {
            this.original = original;
            this._MinRowHeight = minRowHeight;
            if (this._TableView == null)
            {
                this._TableView = new WeakReference(tableView);
            }
        }

        public override nint NumberOfSections(UITableView tableView)
        {

            return this.original.NumberOfSections(tableView);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this.original.RowsInSection(tableview, section);
        }

        public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var h = this.original.GetHeightForRow(tableView, indexPath);
            if (this._MinRowHeight == -1)
            {
                return h;
            }
            else
            {
                if (h < this._MinRowHeight)
                {
                    return this._MinRowHeight;
                }
                return h;
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = this.original.GetCell(tableView, indexPath);
            if (this._RemoveListViewInset)
            {
                cell.SeparatorInset = UIEdgeInsets.Zero;
                cell.PreservesSuperviewLayoutMargins = false;
                cell.LayoutMargins = UIEdgeInsets.Zero;
            }
            return cell;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return this.original.GetHeightForHeader(tableView, section);
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            return this.original.GetViewForHeader(tableView, section);
        }

        //public override string TitleForHeader(UITableView tableView, nint section)
        //{
        //    return this.original.TitleForHeader(tableView, section);
        //}

        public override string[] SectionIndexTitles(UITableView tableView)
        {
            return this.original.SectionIndexTitles(tableView);
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            this.original.RowSelected(tableView, indexPath);
        }

        public override void RowDeselected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            this.original.RowDeselected(tableView, indexPath);
        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, Foundation.NSIndexPath indexPath)
        {
            var visibleCell = tableView.VisibleCells;
            if (visibleCell.Count() > 0)
            {
                var first = visibleCell.First();
                var last = visibleCell.Last();

                var firstCellPath = tableView.IndexPathForCell(first);
                var lastCellPath = tableView.IndexPathForCell(last);
                ViewCell firstViewCell = null;
                ViewCell lastViewCell = null;

                try
                {
                    firstViewCell = first.GetType().GetProperty("ViewCell").GetValue(first) as Xamarin.Forms.ViewCell;
                    lastViewCell = last.GetType().GetProperty("ViewCell").GetValue(last) as Xamarin.Forms.ViewCell;
                }
                catch
                {
                }

                var sectionsAmount = tableView.NumberOfSections();
                var rowsAmount = tableView.NumberOfRowsInSection(indexPath.Section);
                if (indexPath.Section == sectionsAmount - 1 && indexPath.Row == rowsAmount - 1)
                {
                    // This is the last cell in the table
                    this._IsFirstRowVisible = false;
                    this._IsLastRowVisible = true;
                }
                else if (indexPath.Section == 0 && (indexPath.Row == 0))
                {
                    this._IsFirstRowVisible = false;
                    this._IsLastRowVisible = true;
                }
                else if (this._IsStartedScrolling)
                {
                    this._IsFirstRowVisible = false;
                    this._IsLastRowVisible = false;
                }

                var handler = this.OnScrollEvent;
                // fire event
                if (handler != null)
                {
                    var evt = new TableSourceEvents
                    {
                        Y = _LastYPosition,
                        IsFirstRowVisible = this._IsFirstRowVisible,
                        IsLastRowVisible = this._IsLastRowVisible,
                        FirstViewCell = firstViewCell,
                        LastViewCell = lastViewCell
                    };

                    if (firstCellPath != null)
                    {
                        evt.FirstRow = new int[] { firstCellPath.Section, firstCellPath.Row };
                    }
                    else
                    {
                        evt.FirstRow = new int[] { -1, -1 };
                    }

                    if (lastCellPath != null)
                    {
                        evt.LastRow = new int[] { lastCellPath.Section, lastCellPath.Row };
                    }
                    else
                    {
                        evt.LastRow = new int[] { -1, -1 };
                    }
                    handler(this, evt);
                }
            }
        }

        public override void Scrolled(UIScrollView scrollView)
        {
            var handler = this.OnSwipe;
            var y = scrollView.ContentOffset.Y;
            if (scrollView.ContentOffset.Y < 0)
            {
                this._UserSwipeUp = true;
                this._IsStartedScrolling = false;
            }
            else
            {
                this._UserSwipeUp = false;
                this._IsStartedScrolling = true;
            }

            // fire event
            if (handler != null)
            {
                handler(this._UserSwipeUp, y, this._LastYPosition <= 0 ? 0 : this._LastYPosition);
            }

            this._LastYPosition = y;
        }
    }
}
