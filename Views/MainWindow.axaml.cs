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
        private Gamefield gamefield;
        private DispatcherTimer gameTimer;
        private List<RedGhost> ghosts;  //List of multiple ghosts
        private bool isWhiteBackground;

        public MainWindow(int ghostCount = 1, bool isWhiteBackground = false)
        {
            InitializeComponent();

            //Backround-color
            this.isWhiteBackground = isWhiteBackground;
            Background = isWhiteBackground ? Brushes.White : Brushes.Black;

            //Initializing pacman and gamefield
            pacMan = new Pacman { X = 1, Y = 1 };
            gamefield = new Gamefield(ghostCount, isWhiteBackground);

            //Intializing ghosts and adding them to the list
            ghosts = new List<RedGhost>();
            for (int i = 0; i < ghostCount; i++)
            {
                //Add ghosts to the random positions
                int ghostX = 3 + i * 2;
                int ghostY = 3 + i * 2;
                ghosts.Add(new RedGhost(ghostX, ghostY));
            }

            //Initializing the game timer
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(200);
            gameTimer.Tick += OnGameTick;
            gameTimer.Start();

            //Initializing Pacman control
            KeyDown += OnKeyDown;
        }

        //Pacman control
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

        //Game-Update-Tick
        private void OnGameTick(object sender, EventArgs e)
        {
            pacMan.Move(gamefield);

            //Move all ghosts
            foreach (var ghost in ghosts)
            {
                ghost.Move(pacMan, gamefield);
            }

            DrawGame();
        }

        //Draw Gamefield and all objects
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
                            Fill = isWhiteBackground ? Brushes.Black : Brushes.White
                        };
                        Canvas.SetLeft(pointEllipse, x * 20 + 5);
                        Canvas.SetTop(pointEllipse, y * 20 + 5);
                        GameCanvas.Children.Add(pointEllipse);
                    }
                }
            }

            //Draw Pacman
            var pacManEllipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Yellow
            };
            Canvas.SetLeft(pacManEllipse, pacMan.X * 20);
            Canvas.SetTop(pacManEllipse, pacMan.Y * 20);
            GameCanvas.Children.Add(pacManEllipse);

            //Draw all Ghosts
            foreach (var ghost in ghosts)
            {
                var ghostEllipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(ghostEllipse, ghost.X * 20);
                Canvas.SetTop(ghostEllipse, ghost.Y * 20);
                GameCanvas.Children.Add(ghostEllipse);
            }
        }
    }
}
