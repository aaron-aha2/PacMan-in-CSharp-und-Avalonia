using System;
using System.Collections.Generic;

namespace PacManGame.Models
{
    public class Inky : Ghost
    {
        private List<Ghost> ghosts; //Zugriff auf andere Geister

        //Konstruktor: Startposition und Geisterliste
        public Inky(int startX, int startY, List<Ghost> ghosts)
        {
            X = startX;
            Y = startY;
            Name = "Inky"; //Name zur Identifikation in der Liste
            this.ghosts = ghosts;
        }

        //Überschreibt die Move-Methode von Ghost, um Inkys spezifische Bewegungslogik zu implementieren
        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            if (IsVulnerable)
            {
                //Verwundbarer Modus: Bewegt sich zufällig
                MoveRandom(gamefield);
                return;
            }

            //Blinky in der Geisterliste finden
            var blinky = ghosts.Find(g => g.Name == "Blinky");
            if (blinky == null)
                return; //Bewegung abbrechen, wenn Blinky nicht existiert

            //Vorhersagepunkt basierend auf Pac-Mans Richtung
            int targetX = pacman.X;
            int targetY = pacman.Y;

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

            //Zielpunkt für Inky basierend auf Blinkys Position
            int inkyTargetX = targetX + (targetX - blinky.X);
            int inkyTargetY = targetY + (targetY - blinky.Y);

            //Bewegung in Richtung des berechneten Zielpunkts
            MoveTowards(inkyTargetX, inkyTargetY, gamefield);
        }

        //Bewegt Inky in Richtung des Zielpunkts
        private void MoveTowards(int targetX, int targetY, Gamefield gamefield)
        {
            int dx = targetX - X;
            int dy = targetY - Y;

            //Bevorzugt horizontale Bewegung, falls keine Wand
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (dx > 0 && CanMoveRight(gamefield))
                {
                    MoveRight(gamefield);
                }
                else if (dx < 0 && CanMoveLeft(gamefield))
                {
                    MoveLeft(gamefield);
                }
                else
                {
                    //Wenn die horizontale Bewegung blockiert ist, versuche vertikal
                    MoveVertical(dy, gamefield);
                }
            }
            else
            {
                //Bevorzugt vertikale Bewegung, falls keine Wand
                if (dy > 0 && CanMoveDown(gamefield))
                {
                    MoveDown(gamefield);
                }
                else if (dy < 0 && CanMoveUp(gamefield))
                {
                    MoveUp(gamefield);
                }
                else
                {
                    //Wenn die vertikale Bewegung blockiert ist, versuche horizontal
                    MoveHorizontal(dx, gamefield);
                }
            }
        }

        //Versuch einer vertikalen Bewegung
        private void MoveVertical(int dy, Gamefield gamefield)
        {
            if (dy > 0 && CanMoveDown(gamefield))
            {
                MoveDown(gamefield);
            }
            else if (dy < 0 && CanMoveUp(gamefield))
            {
                MoveUp(gamefield);
            }
        }

        //Versuch einer horizontalen Bewegung
        private void MoveHorizontal(int dx, Gamefield gamefield)
        {
            if (dx > 0 && CanMoveRight(gamefield))
            {
                MoveRight(gamefield);
            }
            else if (dx < 0 && CanMoveLeft(gamefield))
            {
                MoveLeft(gamefield);
            }
        }
    }
}
