using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace PacManGame.Views
{
    public partial class GameOverWindow : Window
    {
        public GameOverWindow(int score)
        {
            InitializeComponent();
            ScoreText.Text = $"Your Score: {score}";
        }

        private void OnExitButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Close(); // Schließt das Fenster
        }
        private void OnRestartButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuWindow mwindow = new MenuWindow();
            mwindow.Show();
            Close(); // Schließt das Fenster
        }
    }
}
