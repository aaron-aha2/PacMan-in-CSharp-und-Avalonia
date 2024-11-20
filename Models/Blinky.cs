namespace PacManGame.Models
{
    public class Blinky : Ghost
    {
        public Blinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Blinky"; //Name des Geistes
        }

        //Override der Move-Methode für Blinky
        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                //Im verwundbaren Modus bewegt sich Blinky zufällig
                MoveRandom(gamefield);
            }
            else
            {
                //Im normalen Modus folgt Blinky Pac-Man
                FollowPacMan(pacman, gamefield);
            }
        }

        //Bewegt Blinky, indem er Pac-Man verfolgt
        private void FollowPacMan(Pacman pacman, Gamefield gamefield)
        {
            //Versuche, in die Richtung von Pac-Man zu gehen
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
                //Wenn keine der direkten Richtungen zu Pac-Man möglich ist, versuche zufällige Bewegung
                //Hier wird überprüft, ob jede Richtung begehbar ist
                MoveRandom(gamefield);
            }
            
        }
    }
}
