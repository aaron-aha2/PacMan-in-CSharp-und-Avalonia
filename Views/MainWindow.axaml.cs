using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;
using Avalonia.Media;
using Avalonia.Input;
using PacManGame.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using PacManGame.Models;

namespace PacManGame.Views
{
    public partial class MainWindow : Window
    {
        private Pacman pacMan;
        private int score = 0;
        private int lives = 3;
        private Gamefield gamefield;
        private DispatcherTimer gameTimer;
        private List <Ghost> ghosts;
        private bool isWhiteBackground;

        public MainWindow(int ghostCount = 1, bool isWhiteBackground = false)
        {
            InitializeComponent();
            UpdateScore();
            UpdateLives();

            //Backround-color
            this.isWhiteBackground = isWhiteBackground;
            GameCanvas.Background = isWhiteBackground ? Brushes.White : Brushes.Black;
            if(isWhiteBackground)
            {
                ScoreCounter.Foreground = Brushes.Black;
                LifeCounter.Foreground = Brushes.Black;
            }
            else
            {
                ScoreCounter.Foreground = Brushes.White;
                LifeCounter.Foreground = Brushes.White;
            }

            //Initializing pacman and gamefield
            pacMan = new Pacman { X = 1, Y = 1 };
            gamefield = new Gamefield(ghostCount, isWhiteBackground);
            
            //Initialize ghost list and add ghosts based on ghostCount
            ghosts = new List<Ghost>();
            for (int i = 0; i < ghostCount; i++)
            {
                if(i==1){
                    int ghostX = 3 + i * 2;
                    int ghostY = 3 + i * 2;
                    ghosts.Add(new RedGhost(ghostX, ghostY));

                }
                //Add ghosts to random positions
                else if(i==2){
                    int pinkyX = 6 + i * 3;
                    int pinkyY = 6 + i * 3;
                    ghosts.Add(new Pinky(pinkyX,pinkyY));
                }
                else if(i==3){
                    int inkyX = 4 + i * 3;
                    int inkyY = 4 + i * 3;
                    ghosts.Add(new Inky(inkyX,inkyY));
                }
            }

            //Initializing the game timer
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(200);
            gameTimer.Tick += OnGameTick;
            gameTimer.Start();

            //Initializing Pacman control
            KeyDown += OnKeyDown;
        }

        public void UpdateScore()
        {
            ScoreCounter.Text = $"Score: {score}";
            // Setze die Textfarbe abhängig vom Hintergrund
            if (isWhiteBackground)
            {
                ScoreCounter.Foreground = Brushes.Black; // Dunkler Text für weißen Hintergrund
            }
            else
            {
                ScoreCounter.Foreground = Brushes.White; // Heller Text für schwarzen Hintergrund
            }
        }

        //TODO: Implement lives
        public void UpdateLives()
        {
            LifeCounter.Text = $"Lives: {lives}";
            if (isWhiteBackground)
            {
                LifeCounter.Foreground = Brushes.Black; // Dunkler Text für weißen Hintergrund
            }
            else
            {
                LifeCounter.Foreground = Brushes.White; // Heller Text für schwarzen Hintergrund
            }
        }

        //Pacman control
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    pacMan.CurrentDirection = (Models.Direction)Direction.Up;
                    break;
                case Key.Down:
                    pacMan.CurrentDirection = (Models.Direction)Direction.Down;
                    break;
                case Key.Left:
                    pacMan.CurrentDirection = (Models.Direction)Direction.Left;
                    break;
                case Key.Right:
                    pacMan.CurrentDirection = (Models.Direction)Direction.Right;
                    break;
            }
        }

private void OnGameTick(object sender, EventArgs e)
{
    pacMan.Move(gamefield);

    // Pac-Man isst normales Futter
    if (gamefield.GameFieldData[pacMan.Y, pacMan.X] == 2)
    {
        score += 10;
        UpdateScore();
        gamefield.GameFieldData[pacMan.Y, pacMan.X] = 0;
    }
    // Pac-Man isst Super-Futter
    else if (gamefield.GameFieldData[pacMan.Y, pacMan.X] == 3)
    {
        MakeGhostsVulnerable();
        gamefield.GameFieldData[pacMan.Y, pacMan.X] = 0;
    }

    // Bewegung und Kollisionserkennung für Geister
    foreach (var ghost in ghosts)
    {
        ghost.Move(pacMan, gamefield);

        // Prüfen, ob Pac-Man einen Geist getroffen hat
        if (pacMan.X == ghost.X && pacMan.Y == ghost.Y)
        {
            if (ghost.IsVulnerable)
            {
                eatGhost(ghost); // Geist fressen
            }
            else
            {
                lives--; // Pac-Man verliert ein Leben
                UpdateLives();
                if(lives == 0)
                {
                    GameCanvas.Children.Clear();
                    gameOverTextBlock.Text = "Game Over! Your score: " + score;                
                    gameTimer.Stop(); 
                }
                pacMan.X = 1;
                
            }
        }
            
    }

    DrawGame();
}


        //Draw gamefield and all objects
        private void DrawGame()
        {
            GameCanvas.Children.Clear();

            //Draw gamefield
            for (int y = 0; y < gamefield.GameFieldData.GetLength(0); y++)
            {
                for (int x = 0; x < gamefield.GameFieldData.GetLength(1); x++)
                {
                    if (gamefield.GameFieldData[y, x] == 1)  //Wall
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
                    else if (gamefield.GameFieldData[y, x] == 2)  //Food
                    {
                        var pointEllipse = new Ellipse
                        {
                            Width = 10,
                            Height = 10,
                            Fill = isWhiteBackground ? Brushes.Black : Brushes.Gray
                        };
                        Canvas.SetLeft(pointEllipse, x * 20 + 5);
                        Canvas.SetTop(pointEllipse, y * 20 + 5);
                        GameCanvas.Children.Add(pointEllipse);
                    }
                    else if (gamefield.GameFieldData[y, x] == 3)  //Super-Food
                    {
                        var pointEllipse = new Ellipse
                        {
                            Width = 15,
                            Height = 15,
                            Fill = isWhiteBackground ? Brushes.Black : Brushes.White
                        };
                        Canvas.SetLeft(pointEllipse, x * 20 + 5);
                        Canvas.SetTop(pointEllipse, y * 20 + 5);
                        GameCanvas.Children.Add(pointEllipse);
                    }
                }
            }
        // Erstellen Sie einen Pacman mit einem "Kuchenstück"-Mund
        var pacManPath = new Path
        {
            Fill = Brushes.Yellow
        };

        // Definieren Sie die Geometrie
        var pacManGeometry = new PathGeometry();

        // Startpunkt in der Mitte von Pac-Man
        var pacManFigure = new PathFigure
        {
            StartPoint = new Avalonia.Point(10, 10) // Mittelpunkt
        };

        // Erstellen des "Mundes" (Dreieck)
        pacManFigure.Segments.Add(new LineSegment { Point = new Avalonia.Point(10, 20) }); // Zur Mundspitze
        pacManFigure.Segments.Add(new LineSegment { Point = new Avalonia.Point(20, 15) }); // Zurück an die untere Spitze

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
        Canvas.SetLeft(pacManPath, pacMan.X * 20 - 1);
        Canvas.SetTop(pacManPath, pacMan.Y * 20 - 10);

        // Hinzufügen zum Canvas
        GameCanvas.Children.Add(pacManPath);
            foreach (var ghost in ghosts)
            {
                var ghostEllipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = ghost.IsVulnerable ? Brushes.Blue : Brushes.Red // Blau, wenn verwundbar
                };

                // Setze Farbe basierend auf Geist-Typ und Verwundbarkeit
                switch (ghost)
                {
                    case RedGhost _:
                        ghostEllipse.Fill = ghost.IsVulnerable ? Brushes.Blue : Brushes.Red;
                        break;
                    case Pinky _:
                        ghostEllipse.Fill = ghost.IsVulnerable ? Brushes.Blue : Brushes.Pink;
                        break;
                    case Inky _:
                        ghostEllipse.Fill = ghost.IsVulnerable ? Brushes.Blue : Brushes.Turquoise;
                        break;
                    default:
                        ghostEllipse.Fill = ghost.IsVulnerable ? Brushes.Blue : Brushes.Gray;
                        break;
                }

                Canvas.SetLeft(ghostEllipse, ghost.X * 20);
                Canvas.SetTop(ghostEllipse, ghost.Y * 20);
                GameCanvas.Children.Add(ghostEllipse);
}
}   
        private void MakeGhostsVulnerable()
        {
            foreach (var ghost in ghosts)
            {
                ghost.IsVulnerable = true; // Geister verwundbar machen
            }

            // Starte einen Timer, um die Verwundbarkeit nach 10 Sekunden zu beenden
            var vulnerableTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(10) // Verwundbar für 10 Sekunden
            };

            vulnerableTimer.Tick += (s, e) =>
            {
                foreach (var ghost in ghosts)
                {
                    ghost.IsVulnerable = false; // Geister wieder normal machen
                }
                vulnerableTimer.Stop(); // Timer beenden
            };

            vulnerableTimer.Start();
        }
        private void eatGhost(Ghost ghost)
        {
            if (ghost.IsVulnerable)
            {
                score += 100;
                UpdateScore();
                ghost.X = 10;
                ghost.Y = 10;
            }
        }
    }

}
