using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;
using Avalonia.Media;
using Avalonia.Input;
using PacManGame.Models;
using System;

namespace PacManGame.Views
{
    public partial class MainWindow : Window
    {
        private Pacman pacMan;
        private Gamefield gamefield;
        private DispatcherTimer gameTimer;

        public MainWindow(int ghostCount, bool isWhiteBackground)
        {
            InitializeComponent();

            // Pac-Man- und Spielfeld-Objekte initialisieren
            pacMan = new Pacman { X = 1, Y = 1 }; // Pac-Man startet im Spielfeld
            gamefield = new Gamefield(ghostCount, isWhiteBackground); // Spielfeld erstellen
            
            // Spiel-Timer konfigurieren
            gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200) // Tick-Intervall auf 200ms setzen
            };
            gameTimer.Tick += OnGameTick;
            gameTimer.Start();

            // Tastenereignis für die Steuerung registrieren
            KeyDown += OnKeyDown;
        }

        // Ereignis-Handler für Tastensteuerung
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    pacMan.CurrentDirection = Direction.Up;
                    break;
                case Key.Down:
                    pacMan.CurrentDirection = Direction.Down;
                    break;
                case Key.Left:
                    pacMan.CurrentDirection = Direction.Left;
                    break;
                case Key.Right:
                    pacMan.CurrentDirection = Direction.Right;
                    break;
            }
        }

        // Tick-Handler des Spiel-Timers
        private void OnGameTick(object sender, EventArgs e)
        {
            pacMan.Move(gamefield);
            DrawGame();
        }

        // Spielfeld und Pac-Man zeichnen
        private void DrawGame()
        {
            GameCanvas.Children.Clear();

            // Spielfeld zeichnen
            for (int y = 0; y < gamefield.GameFieldData.GetLength(0); y++)
            {
                for (int x = 0; x < gamefield.GameFieldData.GetLength(1); x++)
                {
                    if (gamefield.GameFieldData[y, x] == 1) // Wenn Wand
                    {
                        var wallRectangle = new Rectangle
                        {
                            Width = 20,
                            Height = 20,
                            Fill = Brushes.Gray
                        };
                        Canvas.SetLeft(wallRectangle, x * 20);
                        Canvas.SetTop(wallRectangle, y * 20);
                        GameCanvas.Children.Add(wallRectangle);
                    }
                    else if (gamefield.GameFieldData[y, x] == 2) // Wenn Punkt
                    {
                        var pointEllipse = new Ellipse
                        {
                            Width = 10,
                            Height = 10,
                            Fill = Brushes.White
                        };
                        Canvas.SetLeft(pointEllipse, x * 20 + 5);
                        Canvas.SetTop(pointEllipse, y * 20 + 5);
                        GameCanvas.Children.Add(pointEllipse);
                    }
                }
            }

            // Pac-Man zeichnen
            var pacManEllipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Yellow
            };
            Canvas.SetLeft(pacManEllipse, pacMan.X * 20);
            Canvas.SetTop(pacManEllipse, pacMan.Y * 20);

            GameCanvas.Children.Add(pacManEllipse);
        }
    }
}
