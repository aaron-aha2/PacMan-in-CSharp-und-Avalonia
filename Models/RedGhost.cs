using System;

namespace PacManGame.Models
{
    public class RedGhost : Ghost
    {
        public RedGhost(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            int dx = pacman.X - X;
            int dy = pacman.Y - Y;

            //Optimierte Bewegungslogik:
            //Berechne den horizontalen und vertikalen Abstand zu Pac-Man
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                //Bewege den Geist zuerst in horizontaler Richtung
                if (dx > 0)
                    MoveRight(gamefield);
                else
                    MoveLeft(gamefield);
            }
            else
            {
                //Bewege den Geist zuerst in vertikaler Richtung
                if (dy > 0)
                    MoveDown(gamefield);
                else
                    MoveUp(gamefield);
            }
        }

        //Bewegt den Geist nach oben, prüft dabei Wände
        private void MoveUp(Gamefield gamefield)
        {
            if (Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1) //Keine Wand
            {
                Y--;
            }
        }

        //Bewegt den Geist nach unten, prüft dabei Wände
        private void MoveDown(Gamefield gamefield)
        {
            if (Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1) //Keine Wand
            {
                Y++;
            }
        }

        //Bewegt den Geist nach links, prüft dabei Wände
        private void MoveLeft(Gamefield gamefield)
        {
            if (X > 0 && gamefield.GameFieldData[Y, X - 1] != 1) //Keine Wand
            {
                X--;
            }
        }

        //Bewegt den Geist nach rechts, prüft dabei Wände
        private void MoveRight(Gamefield gamefield)
        {
            if (X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1) //Keine Wand
            {
                X++;
            }
        }
    }
}
