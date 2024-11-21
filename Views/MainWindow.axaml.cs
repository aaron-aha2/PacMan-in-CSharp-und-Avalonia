using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;
using Avalonia.Media;
using Avalonia.Input;
using PacManGame.Models;
using System;
using System.Collections.Generic;

namespace PacManGame.Views
{
    public partial class MainWindow : Window
    {
        private Pacman pacMan;
        private int score = 0;
        private int lives = 3;
        private Gamefield gamefield;
        private DispatcherTimer gameTimer;
        private List<Ghost> ghosts;
        private bool isWhiteBackground;

        public MainWindow(int ghostCount = 1, bool isWhiteBackground = false)
        {
            InitializeComponent();

            // Score und Lives initialisieren
            UpdateScore();
            UpdateLives();

            // Hintergrundfarbe
            this.isWhiteBackground = isWhiteBackground;
            GameCanvas.Background = isWhiteBackground ? Brushes.White : Brushes.Black;

            // Textfarben anpassen
            ScoreCounter.Foreground = new SolidColorBrush(Color.Parse("#1919A6"));
            LifeCounter.Foreground = new SolidColorBrush(Color.Parse("#1919A6"));

            // Pac-Man und Spielfeld initialisieren
            pacMan = new Pacman { X = 1, Y = 1 };
            gamefield = new Gamefield(ghostCount, isWhiteBackground);

            // Geister-Liste initialisieren und Geister hinzufügen
            ghosts = new List<Ghost>();
            InitializeGhosts(ghostCount);

            // Spiel-Timer konfigurieren
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            gameTimer.Tick += OnGameTick;
            gameTimer.Start();

            // Steuerung für Pac-Man
            KeyDown += OnKeyDown;
        }

        private void InitializeGhosts(int ghostCount)
        {
            for (int i = 1; i <= ghostCount; i++) // Beginnt bei 1, da die Fälle ab 1 definiert waren
            {
                if (i == 1)
                {
                    ghosts.Add(new Blinky(3 + i * 2, 3 + i * 2));
                }
                else if (i == 2)
                {
                    ghosts.Add(new Pinky(6 + i * 3, 6 + i * 3));
                }
                else if (i == 3)
                {
                    ghosts.Add(new Inky(4 + i * 3, 4 + i * 3, ghosts));
                }
                else
                {
                    ghosts.Add(new Clyde(5, 5)); // Standardmäßiger Clyde für alle anderen
                }
            }
        }


        private void UpdateScore()
        {
            ScoreCounter.Text = $"Score: {score}";
        }

        private void UpdateLives()
        {
            LifeCounter.Text = $"Lives: {lives}";
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Up:
                    pacMan.QueuedDirection = (Models.Direction)Direction.Up;
                    break;
                case Key.Down:
                    pacMan.QueuedDirection = (Models.Direction)Direction.Down;
                    break;
                case Key.Left:
                    pacMan.QueuedDirection = (Models.Direction)Direction.Left;
                    break;
                case Key.Right:
                    pacMan.QueuedDirection = (Models.Direction)Direction.Right;
                    break;
            }
        }

        private void OnGameTick(object sender, EventArgs e)
        {
            // Pac-Man bewegen
            pacMan.Move(gamefield);

            // Punkte einsammeln
            CollectFood();

            // Geister bewegen und Kollision prüfen
            foreach (var ghost in ghosts)
            {
                ghost.Move(pacMan, gamefield);

                if (pacMan.X == ghost.X && pacMan.Y == ghost.Y)
                {
                    if (ghost.IsVulnerable)
                    {
                        EatGhost(ghost);
                    }
                    else
                    {
                        LoseLife();
                    }
                }
            }
            checkWinCase();

            // Spiel neu zeichnen
            DrawGame();
        }

        private void CollectFood()
        {
            if (gamefield.GameFieldData[pacMan.Y, pacMan.X] == 2)
            {
                score += 10;
                gamefield.GameFieldData[pacMan.Y, pacMan.X] = 0;
                UpdateScore();
            }
            else if (gamefield.GameFieldData[pacMan.Y, pacMan.X] == 3)
            {
                MakeGhostsVulnerable();
                gamefield.GameFieldData[pacMan.Y, pacMan.X] = 0;
            }
        }

        private void LoseLife()
        {
            lives--;
            UpdateLives();
            if (lives == 0)
            {
                GameOver();
            }
            else
            {
                pacMan.X = 1;
                pacMan.Y = 1;
            }
        }

        private void MakeGhostsVulnerable()
        {
            foreach (var ghost in ghosts)
            {
                ghost.IsVulnerable = true;
            }

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            timer.Tick += (s, e) =>
            {
                foreach (var ghost in ghosts)
                {
                    ghost.IsVulnerable = false;
                }
                timer.Stop();
            };
            timer.Start();
        }

        private void EatGhost(Ghost ghost)
        {
            score += 100;
            UpdateScore();
            ghost.X = 14; // Set Ghost Position to Ghost Spawn
            ghost.Y = 14;
        }

        private void GameOver()
        {
            // Timer stoppen
            gameTimer.Stop();
            // Neues GameOver-Fenster anzeigen
            var gameOverWindow = new GameOverWindow(score);
            gameOverWindow.Show();

            // Aktuelles Fenster schließen
            this.Close();
        }


        private void DrawGame()
        {
            GameCanvas.Children.Clear();

            DrawGamefield();

            DrawPacMan();

            DrawGhosts();
        }

        private void DrawGamefield()
        {
            for (int y = 0; y < gamefield.GameFieldData.GetLength(0); y++)
            {
                for (int x = 0; x < gamefield.GameFieldData.GetLength(1); x++)
                {
                    if (gamefield.GameFieldData[y, x] == 1) //Wall
                    {
                        var wall = new Rectangle
                        {
                            Width = 20,
                            Height = 20,
                            Fill = new SolidColorBrush(Color.Parse("#1919A6")),
                        };
                        Canvas.SetLeft(wall, x * 20);
                        Canvas.SetTop(wall, y * 20);
                        GameCanvas.Children.Add(wall);
                    }
                    else if (gamefield.GameFieldData[y, x] == 2) //Food
                    {
                        var food = new Ellipse
                        {
                            Width = 5,
                            Height = 5,
                            Fill = new SolidColorBrush(Color.Parse("#ffbe47")),
                        };
                        Canvas.SetLeft(food, x * 20 + (20 - food.Width) /2); //Centers food in middle of cell
                        Canvas.SetTop(food, y * 20 + (20 - food.Height) / 2);
                        GameCanvas.Children.Add(food);
                    }
                    else if (gamefield.GameFieldData[y, x] == 3) //Superfood
                    {
                        var superFood = new Ellipse
                        {
                            Width = 10,
                            Height = 10,
                            Fill = Brushes.Gold
                        };
                        Canvas.SetLeft(superFood, x * 20 + (20 - superFood.Width) / 2);
                        Canvas.SetTop(superFood, y * 20 + (20 - superFood.Height) / 2);
                        GameCanvas.Children.Add(superFood);
                    }
                    else if (gamefield.GameFieldData[y, x] == 4) //Ghost exit
                    {
                        var ghostExit = new Rectangle
                        {
                            Width = 20,
                            Height = 5,
                            Fill = new SolidColorBrush(Color.Parse("#DEA185")),
                        };
                        Canvas.SetLeft(ghostExit, x * 20);
                        Canvas.SetTop(ghostExit, y * 20);
                        GameCanvas.Children.Add(ghostExit);
                    }
                }
            }
        }

        private void DrawPacMan()
        {
            var pacManPath = new Path
            {
                Fill = new SolidColorBrush(Color.Parse("#fdff00"))
            };

            // Definieren Sie die Geometrie
            var pacManGeometry = new PathGeometry();

            // Startpunkt in der Mitte von Pac-Man
            var pacManFigure = new PathFigure
            {
                StartPoint = new Avalonia.Point(10, 10)
            };

            // Erstellen des "Mundes" (Dreieck)
            pacManFigure.Segments.Add(new LineSegment { Point = new Avalonia.Point(10, 20) }); // Zur Mundspitze
            pacManFigure.Segments.Add(new LineSegment { Point = new Avalonia.Point(20, 14) }); // Zurück an die untere Spitze

            // Kreisförmigen Rest zeichnen
            pacManFigure.Segments.Add(new ArcSegment
            {
                Point = new Avalonia.Point(10, 10), // Zurück zum Startpunkt
                Size = new Avalonia.Size(10, 10), // Größe des Kreises
                SweepDirection = SweepDirection.Clockwise,
                IsLargeArc = true
            });

            // Hinzufügen der Geometrie
            pacManFigure.IsClosed = true;
            pacManGeometry.Figures.Add(pacManFigure);

            // Hinzufügen der Geometrie zum Path
            pacManPath.Data = pacManGeometry;

            // Platzieren von Pac-Man
            Canvas.SetLeft(pacManPath, pacMan.X * 20 );
            Canvas.SetTop(pacManPath, pacMan.Y * 20 - 9);

            // **Hinzufügen von Pac-Man zum Canvas**
            GameCanvas.Children.Add(pacManPath);
        }


        private void DrawGhosts()
        {
            foreach (var ghost in ghosts)
            {
                var ghostEllipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = ghost.IsVulnerable ? Brushes.Blue : Brushes.Red
                };  
                if (ghost is Blinky)
                {
                    ghostEllipse.Fill = ghost.IsVulnerable ? Brushes.Blue : new SolidColorBrush(Color.Parse("#d03e19"));
                }
                else if (ghost is Pinky)
                {
                    ghostEllipse.Fill = ghost.IsVulnerable ? Brushes.Blue : new SolidColorBrush(Color.Parse("#ea82e5"));
                }
                else if (ghost is Inky)
                {
                    ghostEllipse.Fill = ghost.IsVulnerable ? Brushes.Blue : new SolidColorBrush(Color.Parse("#46bfee"));
                }
                else if (ghost is Clyde)
                {
                    ghostEllipse.Fill = ghost.IsVulnerable ? Brushes.Blue : new SolidColorBrush(Color.Parse("#db851c"));
                }
                

                Canvas.SetLeft(ghostEllipse, ghost.X * 20);
                Canvas.SetTop(ghostEllipse, ghost.Y * 20);
                GameCanvas.Children.Add(ghostEllipse);
            }
        }
        private void checkWinCase()
        {
            // Annahme: Der Gewinn tritt ein, wenn kein Futter (2) oder Super-Futter (3) mehr übrig ist.
            for (int y = 0; y < gamefield.GameFieldData.GetLength(0); y++)
            {
                for (int x = 0; x < gamefield.GameFieldData.GetLength(1); x++)
                {
                    // Wenn noch Futter (2) oder Super-Futter (3) übrig ist, kein Gewinn
                    if (gamefield.GameFieldData[y, x] == 2 || gamefield.GameFieldData[y, x] == 3)
                    {
                        return; // Es gibt noch Punkte zu sammeln, Gewinnbedingung ist nicht erfüllt
                    }
                }
            }

            // Alle Punkte sind aufgebraucht, Spieler hat gewonnen
            gameTimer.Stop(); // Timer anhalten

            // Neues Gewinn-Fenster anzeigen
            var winWindow = new WinWindow(score);
            winWindow.Show();

            // Aktuelles Fenster schließen
            this.Close();
        }

    }
}
