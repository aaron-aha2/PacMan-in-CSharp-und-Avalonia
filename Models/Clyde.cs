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

        public override void Move(Pacman pacman, Gamefield gamefield)// Clyde attack Pacman until he is in a range of four steps. Then he move random. 
        {
            if (pacman == null || gamefield == null){
                throw new ArgumentNullException("Pacman or Gamefield is null.");
            }
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
        
    }
}
