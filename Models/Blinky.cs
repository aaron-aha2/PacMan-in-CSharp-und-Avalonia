using System;

namespace PacManGame.Models
{
    public class Blinky : Ghost
    {
        private int randomMoveSteps = 0; // Zählt, wie viele Schritte er im Random-Modus läuft

        public Blinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Blinky"; // Name des Geistes
            this.currentDirection = (Direction)random.Next(4);
        }

        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                // Im verwundbaren Modus bewegt sich Blinky zufällig
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
                    // Im normalen Modus folgt Blinky Pac-Man
                    if (!FollowPacMan(pacman, gamefield))
                    {
                        // Wenn FollowPacMan blockiert ist, starte den zufälligen Modus für eine bestimmte Anzahl Schritte
                        randomMoveSteps = 5; // Anzahl der Schritte im Zufallsmodus, bevor erneut verfolgt wird
                    }
                }
            }
        }

    
        
    }
}
