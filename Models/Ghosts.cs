using System;
using Avalonia.Controls.Documents;

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
        public bool exitFlag{ get; set; }
        public abstract void Move(Pacman pacman, Gamefield gamefield);
        public Random random = new Random();
        public Direction currentDirection;
        public void MoveRandom(Gamefield gamefield)
        {
            //Move in current direction
            if (!MoveInCurrentDirection(gamefield))
            {
                //Else: move in random direction
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
            return false; //Movement in current direction not possible
        }

        public bool CanMoveUp(Gamefield gamefield) => Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1;
        public bool CanMoveDown(Gamefield gamefield) => Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1 && !(X==11&&Y==13);
        public bool CanMoveLeft(Gamefield gamefield) => X > 0 && gamefield.GameFieldData[Y, X - 1] != 1;
        public bool CanMoveRight(Gamefield gamefield) => X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1;

        public void MoveUp(Gamefield gamefield)
        {
            if(Y > 0 && gamefield.GameFieldData[Y - 1, X] != 1) //No wall
            {
                Y--;
                currentDirection = Direction.Up;

            }
            /*if(X==14 && Y==14){
                exitFlag = false;
            }*/
        }

        public void MoveDown(Gamefield gamefield)
        {
            if(Y < gamefield.GameFieldData.GetLength(0) - 1 && gamefield.GameFieldData[Y + 1, X] != 1 && !(X==11&&Y==13)) //No wall
            {
                Y++;
                currentDirection = Direction.Down;
            }
            
        }

        public void MoveLeft(Gamefield gamefield)
        {
            if(X > 0 && gamefield.GameFieldData[Y, X - 1] != 1) //No wall
            {
                X--;                
                currentDirection = Direction.Left;
            }
        }

        public void MoveRight(Gamefield gamefield)
        {
            if(X < gamefield.GameFieldData.GetLength(1) - 1 && gamefield.GameFieldData[Y, X + 1] != 1) //No wall
            {
                X++;
                currentDirection = Direction.Right;
            }
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
    }
}
