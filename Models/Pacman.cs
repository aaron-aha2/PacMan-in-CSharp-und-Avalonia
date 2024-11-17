using System.ComponentModel;

namespace PacManGame.Models
{
    // Enum zur Definition der Bewegungsrichtungen für Pac-Man
    public enum Direction { Up, Down, Left, Right }

    // Klasse, die den Pac-Man-Charakter repräsentiert
    public class Pacman
    {
        public int X { get; set; } // X-Koordinate
        public int Y { get; set; } // Y-Koordinate
        public Direction CurrentDirection { get; set; } // Aktuelle Bewegungsrichtung
        public int Speed { get; set; } = 1; // Bewegungsgeschwindigkeit

        // Methode zur Aktualisierung der Position von Pac-Man basierend auf der aktuellen Richtung
        public void Move(Gamefield gamefield)
        {
            int newX = X;
            int newY = Y;
            int prevX = X;
            int prevY = Y;

            // Neue Position basierend auf Richtung berechnen
            switch (CurrentDirection)
            {
                case Direction.Up:
                    newY -= Speed;
                    break;
                case Direction.Down:
                    newY += Speed;
                    break;
                case Direction.Left:
                    newX -= Speed;
                    break;
                case Direction.Right:
                    newX += Speed;
                    break;
            }

            // Prüfe, ob neue Position innerhalb des Spielfelds ist und keine Wand darstellt
            if (newX >= 0 && newX < gamefield.GameFieldData.GetLength(1) &&
                newY >= 0 && newY < gamefield.GameFieldData.GetLength(0) &&
                gamefield.GameFieldData[newY, newX] != 1)
            {
                X = newX;
                Y = newY;
            }
            if(newX == 0 && newY == 14 && prevX!=26)
            {
                X = 25;
                newX = X;
            }
            if(newX == 26 && newY == 14 && prevX!=0)
            {
                X = 1;
                newX = X;
            }
        }
    }
}
