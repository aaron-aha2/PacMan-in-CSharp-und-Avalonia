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
        protected override bool IsNearPacman(Pacman pacman)
        {
            int distance = Math.Abs(X - pacman.X) + Math.Abs(Y - pacman.Y);
            return distance <= 2; // overide the method to change the distance
        }
        public override void Move(Pacman pacman, Gamefield gamefield)// Pinky doesn`t attack Pacman actively, if he is not 4 steps ahead. He intercepts Pacman
        {
            

            if (IsVulnerable)
            {
                MoveRandom(gamefield);
                return;
            }
            else if(IsNearPacman(pacman))
                {
                    FollowPacMan(pacman, gamefield);
                    
                }
      
            int targetX = pacman.X;
            int targetY = pacman.Y;

            switch (pacman.CurrentDirection)// with the direction of Pacman he walks four steps ahead .
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

            if (Math.Abs(targetX - X) > Math.Abs(targetY - Y))// if Pacmans X+4 is higher dann Pinkys and he can move right, he walks Right.
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

