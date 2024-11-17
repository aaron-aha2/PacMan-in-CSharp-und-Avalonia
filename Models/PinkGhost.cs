using System;

namespace PacManGame.Models
{
    public class Pinky : Ghost
    {
        public Pinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        // Pinky bewegt sich vor Pac-Man, anstatt direkt auf ihn zuzulaufen
        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            int targetX = pacman.X;
            int targetY = pacman.Y;

            // Berechne die Zielposition 4 Felder vor Pac-Man, abhängig von dessen Bewegungsrichtung
            switch (pacman.CurrentDirection)
            {
                case Direction.Up:
                    targetY = pacman.Y - 4;  // 4 Felder vor Pac-Man in Richtung oben
                    break;
                case Direction.Down:
                    targetY = pacman.Y + 4;  // 4 Felder vor Pac-Man in Richtung unten
                    break;
                case Direction.Left:
                    targetX = pacman.X - 4;  // 4 Felder vor Pac-Man nach links
                    break;
                case Direction.Right:
                    targetX = pacman.X + 4;  // 4 Felder vor Pac-Man nach rechts
                    break;
            }

            // Bewege Pinky in Richtung der berechneten Zielposition
            if (Math.Abs(targetX - X) > Math.Abs(targetY - Y))
            {
                // Wenn die X-Differenz größer ist, bewege Pinky horizontal
                if (targetX > X && CanMoveTo(X + Speed, Y, gamefield))
                {
                    X += Speed;
                }
                else if (targetX < X && CanMoveTo(X - Speed, Y, gamefield))
                {
                    X -= Speed;
                }
            }
            else
            {
                // Wenn die Y-Differenz größer ist, bewege Pinky vertikal
                if (targetY > Y && CanMoveTo(X, Y + Speed, gamefield))
                {
                    Y += Speed;
                }
                else if (targetY < Y && CanMoveTo(X, Y - Speed, gamefield))
                {
                    Y -= Speed;
                }
            }
        }

        // Hilfsmethode zur Prüfung, ob der Geist an der Zielposition in eine Wand laufen würde
        private bool CanMoveTo(int newX, int newY, Gamefield gamefield)
        {
            // Prüfe, ob die neue Position im gültigen Bereich liegt
            if (newX < 0 || newX >= gamefield.GameFieldData.GetLength(1) || newY < 0 || newY >= gamefield.GameFieldData.GetLength(0))
            {
                return false;
            }

            // Prüfe, ob die neue Position eine Wand ist (Wand = 1)
            if (gamefield.GameFieldData[newY, newX] == 1)
            {
                return false;
            }

            return true;
        }

    }
}
