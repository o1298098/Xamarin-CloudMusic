using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using CloudMusic.CustomForms;
using CloudMusic.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PasswordEntryCell), typeof(PasswordEntryCellRenderer))]
namespace CloudMusic.iOS.Renderers
{
    
   public class PasswordEntryCellRenderer: EntryCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var entryCell = (PasswordEntryCell)item;
            var cell = base.GetCell(item, reusableCell, tv);
            if (cell != null)
            {
                var textField = (UITextField)cell.ContentView.Subviews[0];
            }
            return cell;
        }
    }
}