using System;

namespace MiniMusicPlayer.Views
{
    public partial class MusicPlayerView
    {
        public MusicPlayerView()
        {
            InitializeComponent();
        }

        private void ProgressBar_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double MousePosition = e.GetPosition(ProgressBar).X;
            ProgressBar.Value = SetProgressBarValue(MousePosition);
        }

        private int SetProgressBarValue(double MousePosition)
        {
            double ratio = MousePosition / ProgressBar.ActualWidth;
            double ProgressBarValue = ratio * ProgressBar.Maximum;
            return Convert.ToInt32(Math.Round(ProgressBarValue));
        }
    }
}