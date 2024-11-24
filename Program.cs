using Avalonia;
using System;

namespace PacManGame;

class Program
{
    
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    

    //Tests

    /*public static void Main(string[] args)
    {
        /*Test: Movement Pacman
        PacManGame.Tests.PacManTests.TestPacmanMoves();
        */

        /*Tests: Pacman used Portal
        PacManGame.Tests.PacManTests.TestPacmanPortalLEFT();
        PacManGame.Tests.PacManTests.TestPacmanPortalRIGHT();
        */

        /*Tests: Blinky follows Pacman
        PacManGame.Tests.GhostTests.TestBlinkyFollowsPacman();
        PacManGame.Tests.GhostTests.TestClydeRandomModeWhenClose();
        PacManGame.Tests.GhostTests.TestClydeChasesPacmanWhenFar();
        PacManGame.Tests.GhostTests.TestPinkyMovesAheadOfPacman();
        PacManGame.Tests.GhostTests.TestInkyCalculatesTargetCorrectly();
        PacManGame.Tests.GhostTests.TestBlinkyMovesRandomlyWhenVulnerable();
        PacManGame.Tests.GhostTests.TestGhostgetsEatenAndRespawnsInBase();
    }*/
}
