using System;
using System.Diagnostics;

namespace PacManGame.Models
{
    public class Pinky : Ghost
    {
        public Pinky(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Pinky";
            this.currentDirection = (Direction)random.Next(4);
        }

        public override void Move(Pacman pacman, Gamefield gamefield)//Pinky doesn`t attack Pacman actively, if he is not 4 steps ahead. He intercepts Pacman
        {
            Debug.Assert(pacman != null, "Pacman darf nicht null sein.");
            Debug.Assert(gamefield != null, "Gamefield darf nicht null sein.");   
            SpawnStart(gamefield);
            if (IsVulnerable)
            {
                MoveRandom(gamefield);
                return;
            }
            else if (IsNearPacman(pacman))
                {
                FollowPacMan(pacman, gamefield);
                return;
                }

            //Calculate the distance to Pacman
            int targetX = pacman.X;
            int targetY = pacman.Y;

            switch (pacman.CurrentDirection)
            {
                case Direction.Up:
                    targetY = pacman.Y - 4;
                    break;
                case Direction.Down:
                    targetY = pacman.Y + 4;
                    break;
                case Direction.Left:
                    targetX = pacman.X - 4;
                    break;
                case Direction.Right:
                    targetX = pacman.X + 4;
                    break;
            }

            if (Math.Abs(targetX - X) > Math.Abs(targetY - Y))
            {
                if (targetX > X && CanMoveRight(gamefield))
                {
                    MoveRight(gamefield);
                }
                else if (targetX < X && CanMoveLeft(gamefield))
                {
                    MoveLeft(gamefield);
                }
            }
            else
            {
                if(targetY > Y && CanMoveDown(gamefield))
                {
                    MoveDown(gamefield);
                }
                else if (targetY < Y && CanMoveUp(gamefield))
                {
                    MoveUp(gamefield);
                }
            }
        }

        
    }
}
