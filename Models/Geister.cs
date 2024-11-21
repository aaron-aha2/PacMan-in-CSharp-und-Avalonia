using System;

namespace PacManGame.Models
{
    //Abstrakte Basisklasse für Geister
    public abstract class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; } = 1;
        public bool IsVulnerable { get; set; } = false; // Standardmäßig nicht verwundbar
        public bool IsDead { get; set; } = false; // Standardmäßig nicht tot
        public string? Name { get; set; }
        public abstract void Move(Pacman pacman, Gamefield gamefield);
        public Random random = new Random();
        public void MoveRandom(Gamefield gamefield)
        {
            // Wähle zufällig eine Richtung (links, rechts, oben, unten)
            int direction = random.Next(4); // 0 = oben, 1 = unten, 2 = links, 3 = rechts

            switch (direction)
            {
                case 0: // Move Up
                    if (CanMoveUp(gamefield)) MoveUp(gamefield);
                    break;
                case 1: // Move Down
                    if (CanMoveDown(gamefield)) MoveDown(gamefield);
                    break;
                case 2: // Move Left
                    if (CanMoveLeft(gamefield)) MoveLeft(gamefield);
                    break;
                case 3: // Move Right
                    if (CanMoveRight(gamefield)) MoveRight(gamefield);
                    break;
            }
        }
        public bool CanMoveUp(Gamefield gamefield) => Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1;
        
        // Überprüft, ob der Geist sich nach unten bewegen kann (keine Wand)
        public bool CanMoveDown(Gamefield gamefield) => Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1;

        // Überprüft, ob der Geist sich nach links bewegen kann (keine Wand)
        public bool CanMoveLeft(Gamefield gamefield) => X > 0 && gamefield.GameFieldData[Y, X - 1] != 1;

        // Überprüft, ob der Geist sich nach rechts bewegen kann (keine Wand)
        public bool CanMoveRight(Gamefield gamefield) => X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1;
        

        public void MoveUp(Gamefield gamefield)
        {
            if (Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1) // Keine Wand
            {
                Y--;
            }
        }

        // Bewegt den Geist nach unten, prüft dabei Wände
        public void MoveDown(Gamefield gamefield)
        {
            if (Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1) // Keine Wand
            {
                Y++;
            }
        }

        // Bewegt den Geist nach links, prüft dabei Wände
        public void MoveLeft(Gamefield gamefield)
        {
            if (X > 0 && gamefield.GameFieldData[Y, X - 1] != 1) // Keine Wand
            {
                X--;
            }
        }

        // Bewegt den Geist nach rechts, prüft dabei Wände
        public void MoveRight(Gamefield gamefield)
        {
            if (X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1) // Keine Wand
            {
                X++;
            }
        }

    }
}
