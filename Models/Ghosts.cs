using System;
using System.Diagnostics;

namespace PacManGame.Models
{
    public abstract class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; } = 1;
        public bool IsVulnerable { get; set; } = false;
        public bool IsDead { get; set; } = false;
        public string? Name { get; set; }
        public abstract void Move(Pacman pacman, Gamefield gamefield);
        public Random random = new Random();
        public Direction currentDirection;
        protected int randomMoveSteps;
        protected void MoveRandom(Gamefield gamefield)
        {

            if (!MoveInCurrentDirection(gamefield))
            {
                currentDirection = (Direction)random.Next(4);
                MoveInCurrentDirection(gamefield);
            }
        }
        protected void SpawnStart(Gamefield gamefield){//if a Ghost spawns he walks to the playground
            if(X==13 && Y==13)
            {
                while(Y != 10){
                    Y--;
                }
                
            }

        }
        private bool MoveInCurrentDirection(Gamefield gamefield)
        {
            //Assert to ensure the gamefield is valid
            Debug.Assert(gamefield != null, "Gamefield must not be null.");
            Debug.Assert(X >= 0 && X < gamefield.GameFieldData.GetLength(1), "X is out of gamefield");
            Debug.Assert(Y >= 0 && Y < gamefield.GameFieldData.GetLength(0), "Y is out of gamefield");

            //Simplified movement logic using switch expressions
            return currentDirection switch
            {
                Direction.Up => MoveUp(gamefield),
                Direction.Down => MoveDown(gamefield),
                Direction.Left => MoveLeft(gamefield),
                Direction.Right => MoveRight(gamefield),
                _ => false //fallback if the direction is invalid
            };
        }

        protected bool CanMoveUp(Gamefield gamefield) => Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1;//checks walls to not get into
        protected bool CanMoveDown(Gamefield gamefield) => Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1 && !(X==13 && Y==11);
        protected bool CanMoveLeft(Gamefield gamefield) => X > 0 && gamefield.GameFieldData[Y, X - 1] != 1;
        protected bool CanMoveRight(Gamefield gamefield) => X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1;

       protected bool MoveUp(Gamefield gamefield)
        {
            Debug.Assert(gamefield != null, "Gamefield must not be null");
            if (Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1) 
            {
                Y--;
                currentDirection = Direction.Up;
                Debug.Assert(Y >= 0, "Y can't be null"); 
                return true;
            }
            return false; 
        }

        protected bool MoveDown(Gamefield gamefield)
        {
            if (Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1 && !(X == 11 && Y == 13)) 
            {
                Y++;
                currentDirection = Direction.Down;
                return true; 
            }
            return false;
        }

        protected bool MoveLeft(Gamefield gamefield)
        {
            if (X > 0 && gamefield.GameFieldData[Y, X - 1] != 1) 
            {
                X--;
                currentDirection = Direction.Left;
                return true; 
            }
            return false; 
        }

        protected bool MoveRight(Gamefield gamefield)
        {
            if (X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1) 
            {
                X++;
                currentDirection = Direction.Right;
                return true; 
            }
            return false;
        }

        protected bool FollowPacMan(Pacman pacman, Gamefield gamefield)
        {
            //Try to go to pacman
            if (X < pacman.X && CanMoveRight(gamefield))
            {
                MoveRight(gamefield);
                return true;
            }
            else if (X > pacman.X && CanMoveLeft(gamefield))
            {
                MoveLeft(gamefield);
                return true;
            }
            else if (Y < pacman.Y && CanMoveDown(gamefield))
            {
                MoveDown(gamefield);
                return true;
            }
            else if (Y > pacman.Y && CanMoveUp(gamefield))
            {
                MoveUp(gamefield);
                return true;
            }

            //if there is no possible way,then return false
            return false;
        }

        protected virtual bool IsNearPacman(Pacman pacman)//checks that a Ghost are near Pacman
        {
            int distance = Math.Abs(X - pacman.X) + Math.Abs(Y - pacman.Y);
            return distance <= 4; 
        }
    }
}

 