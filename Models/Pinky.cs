using System;

namespace PacManGame.Models
{
    public class Pinky : Ghost
    {
        public Pinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Pinky";
            this.currentDirection = (Direction)random.Next(4);
        }

        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                MoveRandom(gamefield);
                return;
            }

            //Calculate the distance to Pacman
            int distanceToPacman = Math.Abs(pacman.X - X) + Math.Abs(pacman.Y - Y);

            if (distanceToPacman <= 4)
            {
                FollowPacMan(pacman, gamefield);
                return;
            }

            int targetX = pacman.X;
            int targetY = pacman.Y;

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

            if (Math.Abs(targetX - X) > Math.Abs(targetY - Y))
            {
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
                if(targetY > Y && CanMoveDown(gamefield))
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

