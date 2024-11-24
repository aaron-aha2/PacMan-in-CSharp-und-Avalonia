using System;
using Avalonia.Controls.Documents;

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
        public bool exitFlag{ get; set; }
        public abstract void Move(Pacman pacman, Gamefield gamefield);
        public Random random = new Random();
        public Direction currentDirection;
        public void MoveRandom(Gamefield gamefield)
    {
        // Versuche, in die aktuelle Richtung zu bewegen
        if (!MoveInCurrentDirection(gamefield))
        {
            // Wenn die Bewegung nicht möglich ist, wähle eine neue zufällige Richtung
            currentDirection = (Direction)random.Next(4);
            MoveInCurrentDirection(gamefield);
        }
    }
    private bool MoveInCurrentDirection(Gamefield gamefield)
    {
        switch (currentDirection)
        {
            case Direction.Up:
                if (CanMoveUp(gamefield))
                {
                    MoveUp(gamefield);
                    return true;
                }
                break;
            case Direction.Down:
                if (CanMoveDown(gamefield))
                {
                    MoveDown(gamefield);
                    return true;
                }
                break;
            case Direction.Left:
                if (CanMoveLeft(gamefield))
                {
                    MoveLeft(gamefield);
                    return true;
                }
                break;
            case Direction.Right:
                if (CanMoveRight(gamefield))
                {
                    MoveRight(gamefield);
                    return true;
                }
                break;
        }
        return false; // Bewegung nicht möglich
    }
        public bool CanMoveUp(Gamefield gamefield) => Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1;
        
        // Überprüft, ob der Geist sich nach unten bewegen kann (keine Wand)
        public bool CanMoveDown(Gamefield gamefield) => Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1 && !(X==11&&Y==13);

        // Überprüft, ob der Geist sich nach links bewegen kann (keine Wand)
        public bool CanMoveLeft(Gamefield gamefield) => X > 0 && gamefield.GameFieldData[Y, X - 1] != 1;

        // Überprüft, ob der Geist sich nach rechts bewegen kann (keine Wand)
        public bool CanMoveRight(Gamefield gamefield) => X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1;
        

        public void MoveUp(Gamefield gamefield)
        {
            
            if(Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1) // Keine Wand
            {
                Y--;
                currentDirection = Direction.Up;

            }
            /*if(X==14 && Y==14){
                exitFlag = false;
            }*/
        }

        // Bewegt den Geist nach unten, prüft dabei Wände
        public void MoveDown(Gamefield gamefield)
        {
            if(Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1 && !(X==11&&Y==13)) // Keine Wand
            {
                Y++;
                currentDirection = Direction.Down;

            }
            
        }

        // Bewegt den Geist nach links, prüft dabei Wände
        public void MoveLeft(Gamefield gamefield)
        {
            if(X > 0 && gamefield.GameFieldData[Y, X - 1] != 1) // Keine Wand
            {
                X--;                
                currentDirection = Direction.Left;

            }
        }

        // Bewegt den Geist nach rechts, prüft dabei Wände
        public void MoveRight(Gamefield gamefield)
        {
            if(X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1) // Keine Wand
            {
                X++;
                currentDirection = Direction.Right;

            }
        }

    }
}
