using System;

namespace PacManGame.Models
{
    public class Pinky : Ghost
    {
        //Konstruktor, um die Startposition von Pinky zu setzen
        public Pinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Pinky";  //Name des Geistes
            this.currentDirection = (Direction)random.Next(4);
        }

        //Überschreibt die Move-Methode von Ghost, um Pinkys spezifische Bewegungslogik zu implementieren
        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                //Wenn Pinky verwundbar ist, bewegt er sich zufällig
                MoveRandom(gamefield);
                return;
            }

            //Normale Bewegungslogik: Bewege Pinky 4 Felder vor Pac-Man
            int targetX = pacman.X;
            int targetY = pacman.Y;

            //Berechne die Zielposition, die 4 Felder vor Pac-Man liegt, basierend auf dessen Bewegungsrichtung
            switch (pacman.CurrentDirection)
            {
                case Direction.Up:
                    targetY = pacman.Y - 4;
                    break;
                case Direction.Down:
                    targetY = pacman.Y + 4;
                    break;
                case Direction.Left:
                    targetX = pacman.X - 4;
                    break;
                case Direction.Right:
                    targetX = pacman.X + 4;
                    break;
            }

            //Bewege Pinky in Richtung der Zielposition
            if (Math.Abs(targetX - X) > Math.Abs(targetY - Y))
            {
                //Bewege Pinky in die X-Richtung
                if (targetX > X && CanMoveRight(gamefield))
                {
                    MoveRight(gamefield);
                }
                else if (targetX < X && CanMoveLeft(gamefield))
                {
                    MoveLeft(gamefield);
                }
            }
            else
            {
                //Bewege Pinky in die Y-Richtung
                if (targetY > Y && CanMoveDown(gamefield))
                {
                    MoveDown(gamefield);
                }
                else if (targetY < Y && CanMoveUp(gamefield))
                {
                    MoveUp(gamefield);
                }
            }
        }
    }
}

