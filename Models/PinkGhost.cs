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
                int dx = pacman.X - X;
                int dy = pacman.Y - Y;

            // Vorhersage der Position von Pac-Man
            // Pinky bewegt sich zu der Position 4 Felder vor Pac-Man, basierend auf der Bewegungsrichtung von Pac-Man
                switch (pacman.CurrentDirection)
                {
                    case Direction.Up:
                        dx = pacman.X;
                        dy = pacman.Y - 4;  // Bewege Pinky 4 Felder vor Pac-Man in Richtung oben
                        break;
                    case Direction.Down:
                        dx = pacman.X;
                        dy = pacman.Y + 4;  // Bewege Pinky 4 Felder vor Pac-Man in Richtung unten
                        break;
                    case Direction.Left:
                        dx = pacman.X - 4;  // Bewege Pinky 4 Felder vor Pac-Man nach links
                        dy = pacman.Y;
                        break;
                    case Direction.Right:
                        dx = pacman.X + 4;  // Bewege Pinky 4 Felder vor Pac-Man nach rechts
                        dy = pacman.Y;
                        break;
                }

            // Bewege Pinky in Richtung dieser Position
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    X += dx > X ? Speed : -Speed;
                }
                else
                {
                    Y += dy > Y ? Speed : -Speed;
                }

            // Überprüfen, ob Pinky in eine Wand läuft
                if (X < 0 || X >= gamefield.GameFieldData.GetLength(1) || gamefield.GameFieldData[Y, X] == 1)
                {
                    X -= dx > X ? Speed : -Speed;
                }
                if (Y < 0 || Y >= gamefield.GameFieldData.GetLength(0) || gamefield.GameFieldData[Y, X] == 1)
                {
                    Y -= dy > Y ? Speed : -Speed;
                }
        }
    }
}