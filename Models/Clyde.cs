using System;

namespace PacManGame.Models
{
    public class Clyde : Ghost
    {
        public Clyde(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Clyde";
            this.currentDirection = (Direction)random.Next(4);
        }

        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                MoveRandom(gamefield);
            }
            else
            {
                if (randomMoveSteps > 0)
                {
                    MoveRandom(gamefield);
                    randomMoveSteps--;
                }
                else
                {
                    if (IsNearPacman(pacman))
                    {
                        randomMoveSteps = 5;
                    }
                    else
                    {
                        FollowPacMan(pacman, gamefield);
                    }
                }
            }
        }
        private bool IsNearPacman(Pacman pacman)
        {
            int distance = Math.Abs(X - pacman.X) + Math.Abs(Y - pacman.Y);
            return distance <= 4; 
        }
    }
}
