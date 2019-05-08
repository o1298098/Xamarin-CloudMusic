using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.CustomForms
{
    public class BlurView:StackLayout
    {
        public static readonly BindableProperty BlurColorProperty =BindableProperty.Create("BlurColor", typeof(Color), typeof(BlurView), Color.Transparent);
        public static readonly BindableProperty RadiusProperty =BindableProperty.Create("Radius", typeof(float), typeof(BlurView), 10f);
        public static readonly BindableProperty HasFixedTransformationMatrixProperty =BindableProperty.Create("HasFixedTransformationMatrix", typeof(bool), typeof(BlurView), true);
        public Color BlurColor
        {
            get { return (Color)GetValue(BlurColorProperty); }
            set { SetValue(BlurColorProperty, value); }
        }
        public float Radius
        {
            get { return (float)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        public bool HasFixedTransformationMatrix
        {
            get { return (bool)GetValue(HasFixedTransformationMatrixProperty); }
            set { SetValue(HasFixedTransformationMatrixProperty, value); }
        }
    }
}
