using System;

namespace PacManGame.Models
{
    //Abstrakte Basisklasse für Geister
    public abstract class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; } = 1;
        public abstract void Move(Pacman pacman, Gamefield gamefield);
    }

    //Roter Geist, der von Ghost erbt
    public class RedGhost : Ghost
    {
        public RedGhost(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        //Bewegt sich in Richtung Pacman
        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            int dx = pacman.X - X;
            int dy = pacman.Y - Y;

            //Bewegung entlang der X- oder Y-Achse je nach Abstand
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                X += dx > 0 ? Speed : -Speed;
            }
            else
            {
                Y += dy > 0 ? Speed : -Speed;
            }

            //Überprüfen, ob die Position eine Wand ist, und anpassen
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
