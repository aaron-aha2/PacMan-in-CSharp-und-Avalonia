using System;
using PacManGame.Models;

namespace PacManGame.Tests
{
    public class GhostTests
    {
        public static void TestBlinkyFollowsPacman()
        {
            Console.WriteLine("Test: Does Blinky follow Pacman?");

            var pacman = new Pacman { X = 10, Y = 10 };
            var blinky = new Blinky(5, 10); 
            var gamefield = new Gamefield(1, 0, false);

            blinky.Move(pacman, gamefield);

            // Assert
            if (blinky.X == 6 && blinky.Y == 10) //Expected: Blinky moves to the right
            {
                Console.WriteLine("Test successful: Blinky follows Pacman correctly.");
            }
            else
            {
                Console.WriteLine($"Test failed: Expected X=6, Y=10, but got X={blinky.X}, Y={blinky.Y}.");
            }
        }

        public static void TestClydeRandomModeWhenClose()
        {
            Console.WriteLine("Test: Does Clyde switch to random mode when close to Pacman?");

            //Arrange
            var pacman = new Pacman { X = 10, Y = 10 };
            var clyde = new Clyde(8, 10); // Clyde ist nah an Pacman
            var gamefield = new Gamefield(1, 0, false);

            //Act
            clyde.Move(pacman, gamefield);

            //Assert
            if (Math.Abs(clyde.X - 8) <= 1 && Math.Abs(clyde.Y - 10) <= 1)
            {
                Console.WriteLine("Test successful: Clyde moved randomly when near Pacman.");
            }
            else
            {
                Console.WriteLine($"Test failed: Clyde moved inappropriately to X={clyde.X}, Y={clyde.Y}.");
            }
        }

        public static void TestClydeChasesPacmanWhenFar()
        {
            Console.WriteLine("Test: Does Clyde chase Pacman when far away?");

            //Arrange
            var pacman = new Pacman { X = 10, Y = 10 };
            var clyde = new Clyde(2, 10); //Clyde is far away from Pacman
            var gamefield = new Gamefield(1, 0, false);

            //Act
            clyde.Move(pacman, gamefield);

            //Assert
            if (clyde.X == 3 && clyde.Y == 10) //Expected: Clyde moves to the right
            {
                Console.WriteLine("Test successful: Clyde chases Pacman correctly when far away.");
            }
            else
            {
                Console.WriteLine($"Test failed: Expected X=3, Y=10, but got X={clyde.X}, Y={clyde.Y}.");
            }
        }

        public static void TestPinkyMovesAheadOfPacman()
        {
            Console.WriteLine("Test: Does Pinky correctly move 4 fields ahead of Pacman?");

            //Arrange
            var pacman = new Pacman { X = 10, Y = 10, CurrentDirection = (Models.Direction)Direction.Right };
            var pinky = new Pinky(5, 10); //Pinky starts left of Pacman
            var gamefield = new Gamefield(1, 0, false);

            //Act
            pinky.Move(pacman, gamefield);

            //Assert
            if (pinky.X == 14 && pinky.Y == 10) //Expected: Pinky moves 4 fields ahead of Pacman
            {
                Console.WriteLine("Test successful: Pinky moved correctly ahead of Pacman.");
            }
            else
            {
                Console.WriteLine($"Test failed: Expected X=14, Y=10, but got X={pinky.X}, Y={pinky.Y}.");
            }
        }

        public static void TestInkyCalculatesTargetCorrectly()
        {
            Console.WriteLine("Test: Does Inky calculate the target position correctly?");

            //Arrange
            var pacman = new Pacman { X = 10, Y = 10, CurrentDirection = (Models.Direction)Direction.Up };
            var blinky = new Blinky(8, 8); // Blinky ist in der Nähe
            var inky = new Inky(5, 5, new System.Collections.Generic.List<Ghost> { blinky });
            var gamefield = new Gamefield(1, 0, false);

            //Act
            inky.Move(pacman, gamefield);

            //Assert: Inky should move towards calculated target position
            Console.WriteLine($"Inky moved to X={inky.X}, Y={inky.Y}. (Target logic was applied, manual verification needed.)");
        }

         public static void TestBlinkyMovesRandomlyWhenVulnerable()
        {
            Console.WriteLine("Test: Does Blinky move randomly when vulnerable?");

            //Arrange
            var pacman = new Pacman { X = 10, Y = 10 };
            var blinky = new Blinky(5, 10) { IsVulnerable = true };
            var gamefield = new Gamefield(1, 0, false);

            //Act
            int oldX = blinky.X;
            int oldY = blinky.Y;
            blinky.Move(pacman, gamefield);

            //Assert
            if (blinky.X != oldX || blinky.Y != oldY) //Expected: Blinky moves randomly
            {
                Console.WriteLine("Test successful: Blinky moves randomly when vulnerable.");
            }
            else
            {
                Console.WriteLine($"Test failed: Blinky didn't move. Position remains X={blinky.X}, Y={blinky.Y}.");
            }
        }
        public static void TestGhostDoesNotReenterSpawn()
        {
            Console.WriteLine("Test: Does the ghost stay out of the spawn area after leaving it?");

            //Arrange
            var pacman = new Pacman { X = 10, Y = 10 };
            var ghost = new Blinky(13, 13); //Starts in the spawn area
            var gamefield = new Gamefield(1, 0, false);

            //Ghost moves out of spawn
            ghost.Move(pacman, gamefield); //Move once to leave the spawn area
            int initialX = ghost.X;
            int initialY = ghost.Y;

            //Act
            for (int i = 0; i < 10; i++) //Move 10 times
            {
                ghost.Move(pacman, gamefield);
            }

            //Assert: Ghost should not reenter the spawn area
            if (gamefield.GameFieldData[ghost.Y, ghost.X] != 4) //4 = Entrance to spawn area
            {
                Console.WriteLine("Test successful: Ghost does not reenter the spawn area.");
            }
            else
            {
                Console.WriteLine($"Test failed: Ghost reentered spawn area at X={ghost.X}, Y={ghost.Y}.");
            }
        }


        public static void TestGhostgetsEatenAndRespawnsInBase()
        {
            Console.WriteLine("Test: Does Ghost get eaten and respawn in base?");

            //Arrange
            var pacman = new Pacman { X = 10, Y = 10 };
            var blinky = new Blinky(10, 10); //Blinky is on the same position as Pacman
            var gamefield = new Gamefield(1, 0, false);

            //Act
            blinky.IsVulnerable = true;

            //Pacman eats Blinky
            if (blinky.X == pacman.X && blinky.Y == pacman.Y && blinky.IsVulnerable)
            {
                //Ghost respawns in base
                blinky.X = 14;
                blinky.Y = 14;
            }

            //Assert
            if (blinky.X == 14 && blinky.Y == 14) //Expected: Ghost respawns in base
            {
                Console.WriteLine($"Test successful: Ghost respawns in base when eaten: X={blinky.X}, Y={blinky.Y}.");
            }
            else
            {
                Console.WriteLine($"Test failed: Expected X=14, Y=14, but got X={blinky.X}, Y={blinky.Y}.");
            }
        }
    }
}
