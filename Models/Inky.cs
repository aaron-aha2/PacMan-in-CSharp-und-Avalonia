using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PacManGame.Models
{
    public class Inky : Ghost
    {
        private List<Ghost> ghosts; //Access to all ghosts for targeting

        public Inky(int startX, int startY, List<Ghost> ghosts)
        {
            X = startX;
            Y = startY;
            Name = "Inky";
            this.currentDirection = (Direction)random.Next(4);
            this.ghosts = ghosts;
        }
        protected override bool IsNearPacman(Pacman pacman)
        {
            int distance = Math.Abs(X - pacman.X) + Math.Abs(Y - pacman.Y);
            return distance <= 2;
        }

        //Overrides the move method of Ghost to implement Inky's specific movement logic
        public override void Move(Pacman pacman, Gamefield gamefield)//Inky search a way between Blinky and Pacman. In a range of two steps he attacks Pacman actively.
        {
            Debug.Assert(pacman != null, "Pacman darf nicht null sein.");
            Debug.Assert(gamefield != null, "Gamefield darf nicht null sein.");
            SpawnStart(gamefield);
            if (IsVulnerable)
            {
                //Vulnerable: move randomly
                MoveRandom(gamefield);
                return;
            }
            if (IsNearPacman(pacman))
            {
                FollowPacMan(pacman, gamefield);
                return;
            }

            //Find Blinky in the list of ghosts
            var blinky = ghosts.Find(g => g.Name == "Blinky");
            if (blinky == null)
                return; //Blinky not found

            //Predicted point based on Pacman's direction
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

            //Aiming point for Inky based on Blinky's position
            int inkyTargetX = targetX + (targetX - blinky.X);
            int inkyTargetY = targetY + (targetY - blinky.Y);

            //Movement towards target point
            MoveTowards(inkyTargetX, inkyTargetY, gamefield);
        }

        //Moves Inky towards target point without running into walls
        private void MoveTowards(int targetX, int targetY, Gamefield gamefield)
        {
            int dx = targetX - X;
            int dy = targetY - Y;

            //Move in the direction with the larger distance
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
                    //If horizontal movement is blocked: try vertical
                    MoveVertical(dy, gamefield);
                }
            }
            else
            {
                //Move in vertical direction if longer distance
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
                    //If vertical movement is blocked: try horizontal
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
