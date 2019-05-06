using CloudMusic.CustomForms.AudioVisualer;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.CustomForms
{
    public class AudioVisualizerView : View
    {
        public static BindableProperty ColorProperty = BindableProperty.Create("Color", typeof(Color), typeof(AudioVisualizerView), Color.Red);
        public static BindableProperty AnimationSpeedProperty = BindableProperty.Create("AnimationSpeed", typeof(VisualizerAnimationSpeed), typeof(AudioVisualizerView), VisualizerAnimationSpeed.medium);
        public static BindableProperty AudioVisualerTypeProperty = BindableProperty.Create("AudioVisualerType", typeof(AudioVisualerType), typeof(AudioVisualizerView), AudioVisualerType.CircleLine);
        public static BindableProperty DensityProperty = BindableProperty.Create("Density", typeof(float), typeof(AudioVisualizerView), 0.5f);
        public float Density
        {
            get
            {
                return (float)base.GetValue(DensityProperty);
            }
            set
            {
                SetValue(DensityProperty, value);
            }
        }
        public Color Color
        {
            get
            {
                return (Color)base.GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }
        public VisualizerAnimationSpeed AnimationSpeed
        {
            get
            {
                return (VisualizerAnimationSpeed)base.GetValue(AnimationSpeedProperty);
            }
            set
            {
                SetValue(AnimationSpeedProperty, value);
            }
        }
        public AudioVisualerType AudioVisualerType
        {
            get
            {
                return (AudioVisualerType)base.GetValue(AudioVisualerTypeProperty);
            }
            set
            {
                SetValue(AudioVisualerTypeProperty, value);
            }
        }
    }
}
