using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;
using Avalonia.Media;
using Avalonia.Input;
using System;
using PacManGame.Models;

namespace PacManGame.Views
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            // Anzahl der Geister abrufen
            var selectedGhostCountItem = GhostCountComboBox.SelectedItem as ComboBoxItem;
            int ghostCount = int.Parse(selectedGhostCountItem?.Content.ToString() ?? "1");

            // Hintergrundfarbe bestimmen
            bool isWhiteBackground = WhiteBackgroundRadio.IsChecked ?? true;

            // Das Hauptspiel mit den ausgewählten Optionen starten
            MainWindow mwindow = new MainWindow(ghostCount, isWhiteBackground);
            mwindow.Show();
            this.Close(); // Schließt das Menüfenster
        }
    }
}
