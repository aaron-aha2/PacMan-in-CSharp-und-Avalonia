using System;
using System.Collections.Generic;


namespace PacManGame.Models
{

    public class Inky : Ghost
    {
        private List<Ghost> ghosts; // Zugriff auf alle Geister
        private int inkyX;
        private int inkyY;

        public Inky(int inkyX, int inkyY)
        {
            this.inkyX = inkyX;
            this.inkyY = inkyY;
        }

        public Inky(int startX, int startY, List<Ghost> ghosts)
        {
            X = startX;
            Y = startY;
            Name = "Inky"; // Name für Identifikation in der Liste
            this.ghosts = ghosts;
        }

        // Bewegt sich zu einem Punkt, der relativ zur Position von Pac-Man und Blinky ist
        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            // Blinky in der Liste suchen
            var blinky = ghosts.Find(g => g.Name == "Blinky");
            if (blinky == null)
                return; // Wenn kein Blinky gefunden wird, bricht die Bewegung ab

            // Abstand zu Pac-Man berechnen
            int targetX = pacman.X;
            int targetY = pacman.Y;

            // Vorhersagepunkt für Pac-Man berechnen (2 Felder voraus)
            switch (pacman.CurrentDirection)
            {
                case Direction.Up:
                    targetY -= 2;
                    break;
                case Direction.Down:
                    targetY += 2;
                    break;
                case Direction.Left:
                    targetX -= 2;
                    break;
                case Direction.Right:
                    targetX += 2;
                    break;
            }

            // Zielpunkt für Inky basierend auf der Position von Blinky
            int inkyTargetX = targetX + (targetX - blinky.X);
            int inkyTargetY = targetY + (targetY - blinky.Y);

            // Bewegung in Richtung des berechneten Punktes
            int dx = inkyTargetX - X;
            int dy = inkyTargetY - Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                X += dx > 0 ? Speed : -Speed;
            }
            else
            {
                Y += dy > 0 ? Speed : -Speed;
            }

            // Überprüfen, ob Inky in eine Wand läuft und Bewegung zurücknehmen
            if (X < 0 || X >= gamefield.GameFieldData.GetLength(1) || gamefield.GameFieldData[Y, X] == 1)
            {
                X -= dx > 0 ? Speed : -Speed;
            }
            if (Y < 0 || Y >= gamefield.GameFieldData.GetLength(0) || gamefield.GameFieldData[Y, X] == 1)
            {
                Y -= dy > 0 ? Speed : -Speed;
            }
        }
    }
}