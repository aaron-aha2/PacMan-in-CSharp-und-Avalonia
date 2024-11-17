using System;
using System.Collections.Generic;

namespace PacManGame.Models
{
    public class Clyde : Ghost
    {
        private const int ChaseDistance = 4; // Der Abstand, bei dem Clyde den direkten Verfolgemodus verlässt
        

        public Clyde(int startX, int startY, List<Ghost> ghosts)
        {
            X = startX;
            Y = startY;
            Name = "Clyde"; // Identifikation
            Speed = 1; // Standardgeschwindigkeit
            
        }

        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            // Berechne den Abstand zu Pac-Man
            int dx = pacman.X - X;
            int dy = pacman.Y - Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < ChaseDistance)
            {
                // Wenn er in der Nähe von Pac-Man ist, gehe in eine zufällige Richtung
                Random random = new Random();
                int randomDirection = random.Next(4); // Zufällige Richtung auswählen

                switch (randomDirection)
                {
                    case 0: // Nach oben bewegen
                        MoveUp(gamefield);
                        break;
                    case 1: // Nach unten bewegen
                        MoveDown(gamefield);
                        break;
                    case 2: // Nach links bewegen
                        MoveLeft(gamefield);
                        break;
                    case 3: // Nach rechts bewegen
                        MoveRight(gamefield);
                        break;
                }
            }
            else
            {
                // Wenn Clyde weiter entfernt ist, verfolgt er Pac-Man
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    // Bewege in horizontaler Richtung
                    if (dx > 0)
                        MoveLeft(gamefield);
                    else
                        MoveRight(gamefield);
                }
                else
                {
                    // Bewege in vertikaler Richtung
                    if (dy > 0)
                        MoveUp(gamefield);
                    else
                        MoveDown(gamefield);
                }
            }
        }

        // Bewegt den Geist nach oben, prüft dabei Wände
        private void MoveUp(Gamefield gamefield)
        {
            if (Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1) // Keine Wand
            {
                Y--;
            }
        }

        // Bewegt den Geist nach unten, prüft dabei Wände
        private void MoveDown(Gamefield gamefield)
        {
            if (Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1) // Keine Wand
            {
                Y++;
            }
        }

        // Bewegt den Geist nach links, prüft dabei Wände
        private void MoveLeft(Gamefield gamefield)
        {
            if (X > 0 && gamefield.GameFieldData[Y, X - 1] != 1) // Keine Wand
            {
                X--;
            }
        }

        // Bewegt den Geist nach rechts, prüft dabei Wände
        private void MoveRight(Gamefield gamefield)
        {
            if (X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1) // Keine Wand
            {
                X++;
            }
        }
    }
}
