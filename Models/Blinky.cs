namespace PacManGame.Models
{
    public class Blinky : Ghost
    {
        public Blinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Blinky";
            this.currentDirection = (Direction)random.Next(4);
        }

        //Override for the Move method for Blinky
        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                //Blinky moves randomly when vulnerable
                MoveRandom(gamefield);
            }
            else
            {
                //Blinky follows Pacman when in normal mode
                FollowPacMan(pacman, gamefield);
            }
        }

        //Moves Blinky: follows Pacman
        private void FollowPacMan(Pacman pacman, Gamefield gamefield)
        {
            if (X < pacman.X && CanMoveRight(gamefield))
            {
                MoveRight(gamefield);
            }
            else if (X > pacman.X && CanMoveLeft(gamefield))
            {
                MoveLeft(gamefield);
            }
            else if (Y < pacman.Y && CanMoveDown(gamefield))
            {
                MoveDown(gamefield);
            }
            else if (Y > pacman.Y && CanMoveUp(gamefield))
            {
                MoveUp(gamefield);
            }
            else
            {
                //If none of the direct directions to Pacman are possible: move randomly
                MoveRandom(gamefield);
            }
            
        }
    }
}
