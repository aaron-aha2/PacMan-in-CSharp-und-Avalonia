using System;

namespace PacManGame.Models
{
    public class Clyde : Ghost
    {
        private const int ChaseDistance = 4; //Abstand, bei dem Clyde in den Zufallsmodus wechselt

        public Clyde(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Clyde"; //Identifikation
            this.currentDirection = (Direction)random.Next(4);
        }

        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            //Berechne den Abstand zu Pac-Man
            int dx = pacman.X - X;
            int dy = pacman.Y - Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < ChaseDistance)
            {
                //Wenn er in der Nähe von Pac-Man ist, bewege dich zufällig
                MoveRandom(gamefield);
            }
            else
            {
                //Wenn Clyde weiter entfernt ist, verfolgt er Pac-Man
                MoveTowards(pacman.X, pacman.Y, gamefield);
            }
        }

        //Bewegt Clyde in Richtung eines Zielpunkts (Pac-Man), ohne in Wände zu laufen
        private void MoveTowards(int targetX, int targetY, Gamefield gamefield)
        {
            int dx = targetX - X;
            int dy = targetY - Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                //Bevorzugte Bewegung in X-Richtung
                if (dx > 0 && CanMoveRight(gamefield))
                {
                    MoveRight(gamefield);
                }
                else if (dx < 0 && CanMoveLeft(gamefield))
                {
                    MoveLeft(gamefield);
                }
                else
                {
                    //Wenn X blockiert, bewege dich in Y-Richtung
                    MoveVertical(dy, gamefield);
                }
            }
            else
            {
                //Bevorzugte Bewegung in Y-Richtung
                if (dy > 0 && CanMoveDown(gamefield))
                {
                    MoveDown(gamefield);
                }
                else if (dy < 0 && CanMoveUp(gamefield))
                {
                    MoveUp(gamefield);
                }
                else
                {
                    //Wenn Y blockiert, bewege dich in X-Richtung
                    MoveHorizontal(dx, gamefield);
                }
            }
        }

        //Versuch einer vertikalen Bewegung
        private void MoveVertical(int dy, Gamefield gamefield)
        {
            if (dy > 0 && CanMoveDown(gamefield))
            {
                MoveDown(gamefield);
            }
            else if (dy < 0 && CanMoveUp(gamefield))
            {
                MoveUp(gamefield);
            }
        }

        //Versuch einer horizontalen Bewegung
        private void MoveHorizontal(int dx, Gamefield gamefield)
        {
            if (dx > 0 && CanMoveRight(gamefield))
            {
                MoveRight(gamefield);
            }
            else if (dx < 0 && CanMoveLeft(gamefield))
            {
                MoveLeft(gamefield);
            }
        }
    }
}
