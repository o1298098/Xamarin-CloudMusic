using Xamarin.Forms;

namespace CloudMusic.Views
{
    public partial class EmptyPage : ContentPage
    {
        public EmptyPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            switch (btn.Text)
            { case "hifi":
                    visualizer.AudioVisualerType = CustomForms.AudioVisualer.AudioVisualerType.Hifi;
                    break;
                case "wave":
                    visualizer.AudioVisualerType = CustomForms.AudioVisualer.AudioVisualerType.Wave;
                    break;
                case "bar":
                    visualizer.AudioVisualerType = CustomForms.AudioVisualer.AudioVisualerType.Bar;
                    break;
                case "Blob":
                    visualizer.AudioVisualerType = CustomForms.AudioVisualer.AudioVisualerType.Blob;
                    break;
                case "Blast":
                    visualizer.AudioVisualerType = CustomForms.AudioVisualer.AudioVisualerType.Blast;
                    break;
                case "Circleline":
                    visualizer.AudioVisualerType = CustomForms.AudioVisualer.AudioVisualerType.CircleLine;
                    break;
                case "Chibde":
                    visualizer.AudioVisualerType = CustomForms.AudioVisualer.AudioVisualerType.Chibde;
                    break; 
            }
        }
    }
}
