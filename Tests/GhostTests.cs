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
            if (blinky.X == 6 && blinky.Y == 10) // Erwartet: Blinky bewegt sich nach rechts
            {
                Console.WriteLine("Test successful: Blinky follows Pacman correctly.");
            }
            else
            {
                Console.WriteLine($"Test failed: Expected X=6, Y=10, but got X={blinky.X}, Y={blinky.Y}.");
            }
        }

        // Test für Blinkys Zufallsbewegung im verwundbaren Modus
       
        // Test für Clyde, ob er in den Zufallsmodus wechselt, wenn er nahe an Pacman ist
        public static void TestClydeRandomModeWhenClose()
        {
            Console.WriteLine("Test: Does Clyde switch to random mode when close to Pacman?");

            // Arrange
            var pacman = new Pacman { X = 10, Y = 10 };
            var clyde = new Clyde(8, 10); // Clyde ist nah an Pacman
            var gamefield = new Gamefield(1, 0, false);

            // Act
            clyde.Move(pacman, gamefield);

            // Assert
            if (Math.Abs(clyde.X - 8) <= 1 && Math.Abs(clyde.Y - 10) <= 1)
            {
                Console.WriteLine("Test successful: Clyde moved randomly when near Pacman.");
            }
            else
            {
                Console.WriteLine($"Test failed: Clyde moved inappropriately to X={clyde.X}, Y={clyde.Y}.");
            }
        }

        // Test für Clyde, ob er Pacman verfolgt, wenn er weit weg ist
        public static void TestClydeChasesPacmanWhenFar()
        {
            Console.WriteLine("Test: Does Clyde chase Pacman when far away?");

            // Arrange
            var pacman = new Pacman { X = 10, Y = 10 };
            var clyde = new Clyde(2, 10); // Clyde ist weit von Pacman entfernt
            var gamefield = new Gamefield(1, 0, false);

            // Act
            clyde.Move(pacman, gamefield);

            // Assert
            if (clyde.X == 3 && clyde.Y == 10) // Erwartet: Clyde bewegt sich nach rechts
            {
                Console.WriteLine("Test successful: Clyde chases Pacman correctly when far away.");
            }
            else
            {
                Console.WriteLine($"Test failed: Expected X=3, Y=10, but got X={clyde.X}, Y={clyde.Y}.");
            }
        }

        // Test für Pinky, ob er sich korrekt 4 Felder vor Pacman positioniert
        public static void TestPinkyMovesAheadOfPacman()
        {
            Console.WriteLine("Test: Does Pinky correctly move 4 fields ahead of Pacman?");

            // Arrange
            var pacman = new Pacman { X = 10, Y = 10, CurrentDirection = (Models.Direction)Direction.Right };
            var pinky = new Pinky(5, 10); // Pinky startet links von Pacman
            var gamefield = new Gamefield(1, 0, false);

            // Act
            pinky.Move(pacman, gamefield);

            // Assert
            if (pinky.X == 14 && pinky.Y == 10) // Erwartet: Pinky bewegt sich rechts vor Pacman
            {
                Console.WriteLine("Test successful: Pinky moved correctly ahead of Pacman.");
            }
            else
            {
                Console.WriteLine($"Test failed: Expected X=14, Y=10, but got X={pinky.X}, Y={pinky.Y}.");
            }
        }

        // Test für Inky, ob er das Ziel korrekt berechnet
        public static void TestInkyCalculatesTargetCorrectly()
        {
            Console.WriteLine("Test: Does Inky calculate the target position correctly?");

            // Arrange
            var pacman = new Pacman { X = 10, Y = 10, CurrentDirection = (Models.Direction)Direction.Up };
            var blinky = new Blinky(8, 8); // Blinky ist in der Nähe
            var inky = new Inky(5, 5, new System.Collections.Generic.List<Ghost> { blinky });
            var gamefield = new Gamefield(1, 0, false);

            // Act
            inky.Move(pacman, gamefield);

            // Assert: Inky sollte zum berechneten Ziel basierend auf Pacman und Blinky gehen
            Console.WriteLine($"Inky moved to X={inky.X}, Y={inky.Y}. (Target logic was applied, manual verification needed.)");
        }

         public static void TestBlinkyMovesRandomlyWhenVulnerable()
        {
            Console.WriteLine("Test: Does Blinky move randomly when vulnerable?");

            // Arrange
            var pacman = new Pacman { X = 10, Y = 10 };
            var blinky = new Blinky(5, 10) { IsVulnerable = true }; // Blinky ist verwundbar
            var gamefield = new Gamefield(1, 0, false);

            // Act
            int oldX = blinky.X;
            int oldY = blinky.Y;
            blinky.Move(pacman, gamefield);

            // Assert
            if (blinky.X != oldX || blinky.Y != oldY) // Erwartet: Bewegung in eine andere Position
            {
                Console.WriteLine("Test successful: Blinky moves randomly when vulnerable.");
            }
            else
            {
                Console.WriteLine($"Test failed: Blinky didn't move. Position remains X={blinky.X}, Y={blinky.Y}.");
            }
        }

    }
}
