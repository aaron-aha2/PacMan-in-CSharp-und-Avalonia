using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace PacManGame.Views
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void OnStartButtonClick(object? sender, RoutedEventArgs e)
        {
            //Get number of ghosts
            var selectedGhostCountItem = GhostCountComboBox.SelectedItem as ComboBoxItem;
            int ghostCount = int.Parse(selectedGhostCountItem?.Content?.ToString() ?? "1");

            bool isWhiteBackground = WhiteBackgroundRadio.IsChecked ?? true;

            int level = 1;

            //Open main game window with the settings
            MainWindow mwindow = new MainWindow(level, ghostCount, isWhiteBackground);
            mwindow.Show();
            this.Close();
        }
    }
}
