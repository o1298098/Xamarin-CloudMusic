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
                    visualizer.AudioVisualizerType = CustomForms.AudioVisualizer.AudioVisualizerType.Hifi;
                    break;
                case "wave":
                    visualizer.AudioVisualizerType = CustomForms.AudioVisualizer.AudioVisualizerType.Wave;
                    break;
                case "bar":
                    visualizer.AudioVisualizerType = CustomForms.AudioVisualizer.AudioVisualizerType.Bar;
                    break;
                case "Blob":
                    visualizer.AudioVisualizerType = CustomForms.AudioVisualizer.AudioVisualizerType.Blob;
                    break;
                case "Blast":
                    visualizer.AudioVisualizerType = CustomForms.AudioVisualizer.AudioVisualizerType.Blast;
                    break;
                case "Circleline":
                    visualizer.AudioVisualizerType = CustomForms.AudioVisualizer.AudioVisualizerType.CircleLine;
                    break;
                case "Chibde":
                    visualizer.AudioVisualizerType = CustomForms.AudioVisualizer.AudioVisualizerType.Chibde;
                    break; 
            }
        }
    }
}
