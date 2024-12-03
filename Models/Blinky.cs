using System;

namespace PacManGame.Models
{
    public class Blinky : Ghost
    {
        

        public Blinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Blinky"; //Name of the ghost
            this.currentDirection = (Direction)random.Next(4);
        }

        public override void Move(Pacman pacman, Gamefield gamefield)// blinky attack Pacman actively. If the way is blocked, he walks random until the way is passable
        {
            SpawnStart(gamefield);
            if (pacman == null || gamefield == null){
                throw new ArgumentNullException("Pacman or Gamefield is null.");
            }
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
                        randomMoveSteps = 10; //Number of random steps before chasing Pac-Man again
                    }
                }
            }
        }
    }  
}
