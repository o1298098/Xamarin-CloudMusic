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
    public partial class UserViewCell : ViewCell
    {
        public UserViewCell()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            // you can also put cachedImage.Source = null; here to prevent showing old images occasionally
            image.Source = null;
            var item = BindingContext as Userprofile;

            if (item == null)
            {
                return;
            }

            image.Source = item.avatarUrl;

            base.OnBindingContextChanged();
            this.ForceUpdateSize();
        }
    }
}