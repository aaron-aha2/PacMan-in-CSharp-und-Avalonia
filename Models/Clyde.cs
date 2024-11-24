using System;

namespace PacManGame.Models
{
    public class Clyde : Ghost
    {
        private const int ChaseDistance = 4; //Distance at which Clyde switches to random mode

        public Clyde(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Name = "Clyde";
            this.currentDirection = (Direction)random.Next(4);
        }

        public override void Move(Pacman pacman, Gamefield gamefield)
        {
            //Calculate distance to Pacman
            int dx = pacman.X - X;
            int dy = pacman.Y - Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < ChaseDistance)
            {
                //When close to Pacman: move randomly
                MoveRandom(gamefield);
            }
            else
            {
                //When further away from Pacman: follow Pacman
                MoveTowards(pacman.X, pacman.Y, gamefield);
            }
        }

        //Move Clyde towards target point (Pacman) without running into walls
        private void MoveTowards(int targetX, int targetY, Gamefield gamefield)
        {
            int dx = targetX - X;
            int dy = targetY - Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                //Move in X direction
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
                    //If X is blocked: move in Y direction
                    MoveVertical(dy, gamefield);
                }
            }
            else
            {
                //Move in Y direction
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
                    //If Y is blocked: move in X direction
                    MoveHorizontal(dx, gamefield);
                }
            }
        }

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
