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
        public override void Move(Pacman pacman, Gamefield gamefield)//Clyde attack Pacman until he is in a range of four steps. Then he move random.
        {
            SpawnStart(gamefield);
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
                    if (IsNearPacman(pacman) && !(X == 11 && Y == 13) && !(X == 11 && Y == 12) && !(X == 11 && Y == 11) && !(X == 11 && Y == 10) && !(X == 11 && Y == 9))
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
        protected override bool IsNearPacman(Pacman pacman)
        {
            int distance = Math.Abs(X - pacman.X) + Math.Abs(Y - pacman.Y);
            return distance <= 5; 
        }
    }
}
