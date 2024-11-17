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

        // 3. Portalbewegung
        if (X == 0 && Y == 14) // Linkes Portal
        {
            X = 25; // Gehe zum rechten Rand
        }
        else if (X == 26 && Y == 14) // Rechtes Portal
        {
            X = 1; // Gehe zum linken Rand
        }
    }
       
    }
}
    

