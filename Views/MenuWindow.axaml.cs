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
            //Anzahl der Geister abrufen
            var selectedGhostCountItem = GhostCountComboBox.SelectedItem as ComboBoxItem;
            int ghostCount = int.Parse(selectedGhostCountItem?.Content.ToString() ?? "1");

            //Hintergrundfarbe bestimmen
            bool isWhiteBackground = WhiteBackgroundRadio.IsChecked ?? true;

            //Hauptspiel-Fenster mit den Einstellungen öffnen
            MainWindow mwindow = new MainWindow(ghostCount, isWhiteBackground);
            mwindow.Show();
            this.Close(); // Menüfenster schließen
        }
    }
}
