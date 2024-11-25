using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;
using Avalonia.Media;
using Avalonia.Input;
using PacManGame.Models;
using System;
using System.Collections.Generic;
using Avalonia.Controls.Documents;

namespace PacManGame.Views
{
    public partial class MainWindow : Window
    {
        private Pacman pacMan;
        private int score = 0; //TODO: Move to Pacman class
        private int lives = 3;  //TODO: Move to Pacman class
        private Gamefield gamefield;
        private DispatcherTimer gameTimer;
        private List<Ghost> ghosts;
        public int currentLevel = 1;
        private bool allFoodCollected = false;

        public MainWindow(int level = 1, int ghostCount = 1, bool isWhiteBackground = false)
        {
            InitializeComponent();

            UpdateScore();
            UpdateLives();
            UpdateLevel();

            //Background colour
            GameCanvas.Background = isWhiteBackground ? Brushes.White : Brushes.Black;

            //Text colour
            ScoreCounter.Foreground = new SolidColorBrush(Color.Parse("#1919A6"));
            LifeCounter.Foreground = new SolidColorBrush(Color.Parse("#1919A6"));
            LevelCounter.Foreground = new SolidColorBrush(Color.Parse("#1919A6"));

            //Inizialize Pacman and Gamefield
            pacMan = new Pacman { X = 13, Y = 17 };
            gamefield = new Gamefield(level, ghostCount, isWhiteBackground);

            //Pacman movement
            KeyDown += OnKeyDown;

            //Initialize Ghosts and add them to the list
            ghosts = new List<Ghost>();
            InitializeGhosts(ghostCount);

            //Initialize and start game Timer
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            gameTimer.Tick += OnGameTick;
            gameTimer.Start();

            ExitButton.Click += OnExitButtonClick;
            ResetButton.Click += OnRestartButtonClick;

            DrawGame();
        }

        //Eventhandler for the whole game logic
        private void OnGameTick(object? sender, EventArgs e)
        {   
            pacMan.Move(gamefield);
            CollectFood(); //TODO: Move to Pacman class

            //Move ghosts and check for collisions
            foreach (var ghost in ghosts)
            {
                ghost.Move(pacMan, gamefield);

                if (pacMan.X == ghost.X && pacMan.Y == ghost.Y)
                {
                    if (ghost.IsVulnerable)
                    {
                        EatGhost(ghost); //TODO: Move to Pacman class
                        return;
                    }
                    else
                    {
                        LoseLife(); //TODO: Move to Pacman class
                        return;
                    }
                }

                if(ghost.Y==pacMan.Y && ghost.X==pacMan.X+1 &&  ghost.currentDirection== Models.Direction.Right && pacMan.CurrentDirection==Models.Direction.Left){
                    if(ghost.IsVulnerable){
                        EatGhost(ghost);
                    }
                    else{
                        LoseLife();
                    }
                }
                if(ghost.Y==pacMan.Y && ghost.X==pacMan.X-1 &&  ghost.currentDirection== Models.Direction.Left && pacMan.CurrentDirection==Models.Direction.Right){
                    if(ghost.IsVulnerable){
                        EatGhost(ghost);
                    }
                    else{
                        LoseLife();
                    }
                }
                if(ghost.Y==pacMan.Y+1 && ghost.X==pacMan.X &&  ghost.currentDirection== Models.Direction.Up && pacMan.CurrentDirection==Models.Direction.Down){
                    if(ghost.IsVulnerable){
                        EatGhost(ghost);
                    }
                    else{
                        LoseLife();
                    }
                }
                if(ghost.Y==pacMan.Y-1 && ghost.X==pacMan.X &&  ghost.currentDirection== Models.Direction.Down && pacMan.CurrentDirection==Models.Direction.Up){
                    if(ghost.IsVulnerable){
                        EatGhost(ghost);
                    }
                    else{
                        LoseLife();
                    }
                }
            }

            CheckAllFoodCollected();

            if (allFoodCollected == true)
            {
                currentLevel++;
                if (currentLevel > 2)
                {   
                    //Player has won: No more levels left
                    var winWindow = new WinWindow(score);
                    winWindow.Show();
                    this.Close();
                }
                else
                {
                    //Player has won: Next level
                    gamefield.LoadLevel(currentLevel);

                    pacMan = new Pacman { X = 13, Y = 17 };

                    //Keep old score, update level and draw new game
                    UpdateScore();
                    UpdateLevel();
                    DrawGame();
                    allFoodCollected = false;
                }
            }
            //Redraw game after every tick
            DrawGame();
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
                        
                        //Centers food in middle of cell:
                        Canvas.SetLeft(food, x * 20 + (20 - food.Width) /2);
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

            // Geometrie
            var pacManGeometry = new PathGeometry();

            //Creating pacManFigure (Circle)
            var pacManFigure = new PathFigure
            {
                StartPoint = new Avalonia.Point(10, 10)
            };

            //Creating the "mouth" (triangle)
            pacManFigure.Segments?.Add(new LineSegment { Point = new Avalonia.Point(10, 20) }); //Tip of the triangle
            pacManFigure.Segments?.Add(new LineSegment { Point = new Avalonia.Point(20, 14) }); //Right side of the triangle

            // Kreisförmigen Rest zeichnen
            pacManFigure.Segments?.Add(new ArcSegment
            {
                Point = new Avalonia.Point(10, 10),
                Size = new Avalonia.Size(9, 9), //Radius
                SweepDirection = SweepDirection.Clockwise,
                IsLargeArc = true
            });

            //Close circle
            pacManFigure.IsClosed = true;
            pacManGeometry?.Figures?.Add(pacManFigure);

            pacManPath.Data = pacManGeometry;


            //Place Pacman in the middle of the cell
            Canvas.SetLeft(pacManPath, pacMan.X * 20 - 2);
            Canvas.SetTop(pacManPath, pacMan.Y * 20 - 9);

            GameCanvas.Children.Add(pacManPath);
        }

        private void DrawGhosts()
        {
            foreach (var ghost in ghosts)
            {
                var ghostEllipse = new Ellipse
                {
                    Width = 18,
                    Height = 18,
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

        private void InitializeGhosts(int ghostCount)
        {
            for (int i = 1; i <= ghostCount; i++)
            {
                if (i == 1)
                {
                    ghosts.Add(new Pinky(3 + i * 2, 3 + i * 2));
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
                    ghosts.Add(new Clyde(5, 5));
                }
            }
        }

        public void MakeGhostsVulnerable()
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

        private void CheckAllFoodCollected()
        {
            allFoodCollected = true;
            for (int y = 0; y < gamefield.GameFieldData.GetLength(0); y++)
            {
                for (int x = 0; x < gamefield.GameFieldData.GetLength(1); x++)
                {
                    if (gamefield.GameFieldData[y, x] == 2 || gamefield.GameFieldData[y, x] == 3)
                    {      
                        allFoodCollected = false;
                        return;
                    }
                }
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();

            var gameOverWindow = new GameOverWindow(score);
            gameOverWindow.Show();

            this.Close();
        }

        //Eventhandler for Pacman movement
        private void OnKeyDown(object? sender, KeyEventArgs e)
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
                pacMan.X = 13;
                pacMan.Y = 17;
            }
        }

        public void EatGhost(Ghost ghost)
        {
            
            score += 100;
            UpdateScore();
            
            //Set Ghost Position to Ghost Spawn
            ghost.X = 14;
            ghost.Y = 14;
            
            DrawGame();  
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();
            DrawGamefield();
            DrawPacMan();
            DrawGhosts();
        }


        // -- Labels and Buttons -- //
        private void UpdateScore()
        {
            ScoreCounter.Text = $"Score: {score}";
        }

        private void UpdateLives()
        {
            LifeCounter.Text = $"Lives: {lives}";
        }

        private void UpdateLevel()
        {
            LevelCounter.Text = $"Level: {currentLevel}";
        }

        private void OnExitButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Close();
        }
        
        private void OnRestartButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuWindow mwindow = new MenuWindow();
            mwindow.Show();
            Close();
        }
    }
}
