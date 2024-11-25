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

        //Overrides the move method of Ghost to implement Pinky's specific movement logic
        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                //If vulnerable: move randomly
                MoveRandom(gamefield);
                return;
            }

            //Normal movement logic: Move Pinky 4 tiles ahead of Pac
            int targetX = pacman.X;
            int targetY = pacman.Y;

            //Calculate target position that is 4 tiles ahead of Pacman
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

            //Move Pinky towards target point
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

