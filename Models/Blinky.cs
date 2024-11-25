using System;

namespace PacManGame.Models
{
    public class Blinky : Ghost
    {
        private int randomMoveSteps = 0; //Counts how many steps Blinky moves randomly

        public Blinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Blinky"; //Name of the ghost
            this.currentDirection = (Direction)random.Next(4);
        }

        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                //In vulnerable mode, Blinky moves randomly
                MoveRandom(gamefield);
            }
            else
            {
                if (randomMoveSteps > 0)
                {
                    //Continue random movement for a set number of steps
                    MoveRandom(gamefield);
                    randomMoveSteps--;
                }
                else
                {
                    //In normal mode, Blinky follows Pac-Man
                    if (!FollowPacMan(pacman, gamefield))
                    {
                        //If FollowPacMan is blocked, enter random mode for a specific number of steps
                        randomMoveSteps = 5; //Number of random steps before chasing Pac-Man again
                    }
                }
            }
        }
    }
}
