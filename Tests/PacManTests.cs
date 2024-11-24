using System;
using PacManGame.Models;

namespace PacManGame.Tests
{
    public class PacManTests
    {
        public static void TestPacmanMoves()
        {
            Console.WriteLine("Test: does Pacman move correctly?");

            var pacman = new Pacman { X = 5, Y = 5, CurrentDirection = (Models.Direction)Direction.Right };
            var gamefield = new Gamefield(1, 0, false);

            pacman.Move(gamefield);
            Console.WriteLine(pacman.X == 6 && pacman.Y == 5 ? "Move Right: OK" : "Move Right: FAILED");

            pacman.CurrentDirection = (Models.Direction)Direction.Left;
            pacman.Move(gamefield);
            Console.WriteLine(pacman.X == 5 && pacman.Y == 5 ? "Move Left: OK" : "Move Left: FAILED");

            pacman.CurrentDirection = (Models.Direction)Direction.Up;
            pacman.Move(gamefield);
            Console.WriteLine(pacman.X == 5 && pacman.Y == 4 ? "Move UP: OK" : "Move UP: FAILED");

            pacman.CurrentDirection = (Models.Direction)Direction.Down;
            pacman.Move(gamefield);
            Console.WriteLine(pacman.X == 5 && pacman.Y == 5 ? "Move Down: OK" : "Move Down: FAILED");
        }

        public static void TestPacmanPortalLEFT()
        {
            Console.WriteLine("Test: does the teleportation at the left side work correctly?");

            //Arrange: Place Pacman at the left portal edge
            var pacman = new Pacman { X = 0, Y = 14, CurrentDirection = (Models.Direction)Direction.Left };
            var gamefield = new Gamefield(1, 0, false);

            //Act
            pacman.Move(gamefield);

            //Assert: Check if Pacman was teleported to the right side
            if (pacman.X == 25 && pacman.Y == 14)
            {
                Console.WriteLine("Test was successful: Pacman got teleported to the right side.");
            }
            else
            {
                Console.WriteLine($"Test wasn't successful: Expected X=25, Y=14, but its X={pacman.X}, Y={pacman.Y}.");
            }
        }
        public static void TestPacmanPortalRIGHT()
        {
            Console.WriteLine("Test: does the teleportation at the right side work correctly?");

            //Arrange: Place Pacman at the right portal edge
            var pacman = new Pacman { X = 25, Y = 14, CurrentDirection = (Models.Direction)Direction.Right };
            var gamefield = new Gamefield(1, 0, false);

            //Act
            pacman.Move(gamefield);

            //Assert: Check if Pacman was teleported to the left side
            if (pacman.X == 1 && pacman.Y == 14)
            {
                Console.WriteLine("Test was successful: Pacman got teleported to the left side.");
            }
            else
            {
                Console.WriteLine($"Test wasn't successful: Expected X=1, Y=14, but its X={pacman.X}, Y={pacman.Y}.");
            }
        }
    }
}
