using System.ComponentModel;

namespace PacManGame.Models
{
    public enum Direction { Up, Down, Left, Right }

    public class Pacman
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction CurrentDirection { get; set; }
        public int Speed { get; set; } = 1;
        public Direction? QueuedDirection { get; set; } //Direction that shall be taken next

        public void Move(Gamefield gamefield)
        {
            int newX = X;
            int newY = Y;

            //1. Try to move in the queued direction
            if (QueuedDirection.HasValue)
            {
                switch (QueuedDirection.Value)
                {
                    case Direction.Up: newY = Y - Speed; break;
                    case Direction.Down: newY = Y + Speed; break;
                    case Direction.Left: newX = X - Speed; break;
                    case Direction.Right: newX = X + Speed; break;
                }

                //Chgeck if movement in the queued direction is possible
                if (newX >= 0 && newX < gamefield.GameFieldData.GetLength(1) &&
                    newY >= 0 && newY < gamefield.GameFieldData.GetLength(0) &&
                    gamefield.GameFieldData[newY, newX] != 1)
                {
                    //Execute movement
                    CurrentDirection = QueuedDirection.Value;
                    QueuedDirection = null; //Reset queued direction if movement was successful
                }
            }

            //2. Try to move in the current direction
            newX = X;
            newY = Y;

            switch (CurrentDirection)
            {
                case Direction.Up: newY = Y - Speed; break;
                case Direction.Down: newY = Y + Speed; break;
                case Direction.Left: newX = X - Speed; break;
                case Direction.Right: newX = X + Speed; break;
            }

            //Check if movement in the current direction is possible
            if (newX >= 0 && newX < gamefield.GameFieldData.GetLength(1) &&
                newY >= 0 && newY < gamefield.GameFieldData.GetLength(0) &&
                gamefield.GameFieldData[newY, newX] != 1)
            {
                //Execute movement
                X = newX;
                Y = newY;
            }

            //3. Protalmovement
            if (gamefield.GameFieldData[Y, X] == 5 && X == 0 && Y == 14) //Left portal Level 1
            {
                X = 25; //Move to right edge
            }
            else if (gamefield.GameFieldData[Y, X] == 5 && X == 26 && Y == 14) //Right portal Level 1
            {
                X = 0; //Move to left edge
            }
            else if (gamefield.GameFieldData[Y, X] == 6 && X == 0 && Y == 8) //Left upper portal Level 2
            {
                X = 25; //Move to right edge
            }
            else if (gamefield.GameFieldData[Y, X] == 6 && X == 26 && Y ==8) //Right upper portal Level 2
            {
                X = 0; //Move to left edge
            }
            else if (gamefield.GameFieldData[Y, X] == 7 && X == 0 && Y == 18) //Left lower portal Level 2
            {
                X = 25; //Move to right edge
            }
            else if (gamefield.GameFieldData[Y, X] == 7 && X == 26 && Y == 18) //Right lower portal Level 2
            {
                X = 0; //Move to left edge
            }  
        }
    }
}
    