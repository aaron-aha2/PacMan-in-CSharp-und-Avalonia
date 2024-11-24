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
        public Direction? QueuedDirection { get; set; } // Geplante Richtung, falls die aktuelle blockiert ist


        // Methode zur Aktualisierung der Position von Pac-Man basierend auf der aktuellen Richtung
        public void Move(Gamefield gamefield)
        {
            int newX = X;
            int newY = Y;

            // 1. Versuche, die geplante Richtung zu verwenden
            if (QueuedDirection.HasValue)
            {
                switch (QueuedDirection.Value)
                {
                    case Direction.Up: newY = Y - Speed; break;
                    case Direction.Down: newY = Y + Speed; break;
                    case Direction.Left: newX = X - Speed; break;
                    case Direction.Right: newX = X + Speed; break;
                }

                // Prüfen, ob die Bewegung in der geplanten Richtung möglich ist
                if (newX >= 0 && newX < gamefield.GameFieldData.GetLength(1) &&
                    newY >= 0 && newY < gamefield.GameFieldData.GetLength(0) &&
                    gamefield.GameFieldData[newY, newX] != 1)
                {
                    // Geplante Richtung übernehmen
                    CurrentDirection = QueuedDirection.Value;
                    QueuedDirection = null; // Geplante Richtung nur zurücksetzen, wenn erfolgreich
                }
            }

            // 2. Bewegung in der aktuellen Richtung fortsetzen
            newX = X;
            newY = Y;

            switch (CurrentDirection)
            {
                case Direction.Up: newY = Y - Speed; break;
                case Direction.Down: newY = Y + Speed; break;
                case Direction.Left: newX = X - Speed; break;
                case Direction.Right: newX = X + Speed; break;
            }

            // Prüfen, ob die Bewegung in der aktuellen Richtung möglich ist
            if (newX >= 0 && newX < gamefield.GameFieldData.GetLength(1) &&
                newY >= 0 && newY < gamefield.GameFieldData.GetLength(0) &&
                gamefield.GameFieldData[newY, newX] != 1)
            {
                // Bewegung ausführen
                X = newX;
                Y = newY;
            }

            //3. Protalmovement
            if (gamefield.GameFieldData[Y, X] == 5 && X == 0 && Y == 14) //Left portal Level 1
            {
                X = 25; //Move to right edge
            }
            else if (gamefield.GameFieldData[Y, X] == 5 && X == 26 && Y == 14) //Right portal Level 1
            {
                X = 0; //Move to left edge
            }
            else if (gamefield.GameFieldData[Y, X] == 6 && X == 0 && Y == 8) //Left upper portal Level 2
            {
                X = 25; //Move to right edge
            }
            else if (gamefield.GameFieldData[Y, X] == 6 && X == 26 && Y ==8) //Right upper portal Level 2
            {
                X = 0; //Move to left edge
            }
            else if (gamefield.GameFieldData[Y, X] == 7 && X == 0 && Y == 18) //Left lower portal Level 2
            {
                X = 25; //Move to right edge
            }
            else if (gamefield.GameFieldData[Y, X] == 7 && X == 26 && Y == 18) //Right lower portal Level 2
            {
                X = 0; //Move to left edge
            }
            
        }
    }
}
    