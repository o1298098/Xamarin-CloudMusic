using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.CustomForms
{
   public class CustomTabbedPage:TabbedPage
    {
        public event EventHandler SearchClicked;
        public event EventHandler MenuClicked;
        public static void OnSearchClick(object sender, EventArgs e)
        {
            var customview = (CustomTabbedPage)sender;
            customview.SearchClicked?.Invoke(customview, e);
        }
        public static void OnMenuClicked(object sender, EventArgs e)
        {
            var customview = (CustomTabbedPage)sender;
            customview.MenuClicked?.Invoke(customview, e);
        }
    }
}
