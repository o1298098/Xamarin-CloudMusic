using CloudMusic.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CloudMusic.CustomForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MVCommentViewCell : ViewCell
    {
        public MVCommentViewCell()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            image.Source = null;
            var item = BindingContext as MusicComment.Comment;

            if (item == null)
            {
                return;
            }

            image.Source = item.user.avatarUrl;
            base.OnBindingContextChanged();
        }
    }
}