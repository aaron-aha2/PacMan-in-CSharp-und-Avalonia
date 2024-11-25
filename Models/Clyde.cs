using System;

namespace PacManGame.Models
{
    public class Clyde : Ghost
    {
         //Distance at which Clyde switches to random mode

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
                // Clyde bewegt sich zufällig, wenn er verwundbar ist
                MoveRandom(gamefield);
            }
            else
            {
                if (randomMoveSteps > 0)
                {
                    // Solange zufällige Bewegung läuft
                    MoveRandom(gamefield);
                    randomMoveSteps--;
                }
                else
                {
                    // Wenn Clyde nah an Pac-Man ist, wechselt er in den Zufallsmodus
                    if (IsNearPacman(pacman))
                    {
                        randomMoveSteps = 5; // Anzahl der Schritte im Zufallsmodus
                    }
                    else
                    {
                        // Verfolge Pac-Man, wenn Clyde weit entfernt ist
                        FollowPacMan(pacman, gamefield);
                    }
                }
            }
        }

        // Überprüft, ob Clyde nahe genug an Pac-Man ist
        private bool IsNearPacman(Pacman pacman)
        {
            int distance = Math.Abs(X - pacman.X) + Math.Abs(Y - pacman.Y);
            return distance <= 4; // Zum Beispiel: Nähe definiert als <= 4 Felder
        }

        
    }
}
