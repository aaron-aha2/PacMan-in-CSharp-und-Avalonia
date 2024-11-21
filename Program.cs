using Avalonia;
using System;

namespace PacManGame;

class Program
{
    
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    

    /*Tests 

    public static void Main(string[] args)
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
    }*/
}
