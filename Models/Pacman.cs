using System.Diagnostics;

namespace PacManGame.Models
{
    public enum Direction { Up, Down, Left, Right }

    public class Pacman
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction CurrentDirection { get; set; }
        public int Speed { get; set; } = 1;
        public Direction? QueuedDirection { get; set; }

        private (int newX, int newY) CalculateNewPosition(Direction direction, int speed)//looks in which direction Pacman walks
        {
            return direction switch
            {
                Direction.Up => (X, Y - speed),
                Direction.Down => (X, Y + speed),
                Direction.Left => (X - speed, Y),
                Direction.Right => (X + speed, Y),
                _ => (X, Y)
            };
        }

        public void Move(Gamefield gamefield)
        {
            Debug.Assert(gamefield != null, "Gamefield cannot be null.");
            int newX, newY;

            if (QueuedDirection.HasValue)
            {
                (newX, newY) = CalculateNewPosition(QueuedDirection.Value, Speed);//if there is a wall, he can`t move in this direction

                if (newX >= 0 && newX < gamefield.GameFieldData.GetLength(1) &&
                    newY >= 0 && newY < gamefield.GameFieldData.GetLength(0) &&
                    gamefield.GameFieldData[newY, newX] != 1)
                {
                    CurrentDirection = QueuedDirection.Value;
                    QueuedDirection = null;
                }
            }

            (newX, newY) = CalculateNewPosition(CurrentDirection, Speed);

            if (newX >= 0 && newX < gamefield.GameFieldData.GetLength(1) &&
                newY >= 0 && newY < gamefield.GameFieldData.GetLength(0) &&
                gamefield.GameFieldData[newY, newX] != 1)
            {
                X = newX;
                Y = newY;
            }

            if (gamefield.GameFieldData[Y, X] == 5 && X == 0 && Y == 14)//It says, that in this koordinate is a Teleportation
            {
                X = 25;
            }
            else if (gamefield.GameFieldData[Y, X] == 5 && X == 26 && Y == 14)
            {
                X = 0;
            }
            else if (gamefield.GameFieldData[Y, X] == 6 && X == 0 && Y == 8)
            {
                X = 25;
            }
            else if (gamefield.GameFieldData[Y, X] == 6 && X == 26 && Y == 8)
            {
                X = 0;
            }
            else if (gamefield.GameFieldData[Y, X] == 7 && X == 0 && Y == 18)
            {
                X = 25;
            }
            else if (gamefield.GameFieldData[Y, X] == 7 && X == 26 && Y == 18)
            {
                X = 0;
            }
        }
    }
}
