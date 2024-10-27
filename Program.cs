using System; //importiert System-Namespace 

class Program
{
    static char[,] spielfeld = { //'#' als Wand und '.' als einsammelbaren Punkt 
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        { '#', '.', '.', '.', '.', '.', '.', '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#' },
        { '#', '.', '#', '#', '#', '#', '.', '#', '.', '#', '#', '#', '#', '#', '.', '#', '#', '.', '#' },
        { '#', '.', '.', '.', '.', '#', '.', '.', '.', '.', '.', '.', '.', '#', '.', '.', '.', '.', '#' },
        { '#', '#', '#', '#', '.', '#', '#', '#', '#', '#', '#', '#', '.', '#', '#', '#', '#', '.', '#' },
        { '#', '.', '.', '#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#', '.', '.', '#' },
        { '#', '.', '#', '#', '#', '#', '.', '#', '#', '#', '#', '#', '#', '#', '.', '#', '#', '#', '#' },
        { '#', '.', '.', '.', '.', '#', '.', '.', '.', '.', '.', '.', '.', '#', '.', '.', '.', '.', '#' },
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
    };

    static int pacmanX = 1; //Startposition von Pacman (X-Koordinate)
    static int pacmanY = 1; //Startposition von Pacman (Y-Koordinate)

    static void Main(string[] args) //Main Funktion
    {
        bool spielLaeuft = true; //Zustandsvariable

        while (spielLaeuft) //Hauptschleife des Spiels
        {
            Console.Clear(); //Löscht die Konsole, um das Spielfeld zu aktualisieren bzw. neu zu zeichen
            ZeichneSpielfeld(); 
            ConsoleKey eingabe = Console.ReadKey(true).Key; //Liest Eingabe ein ohne Konsolenausgabe 
            BewegePacman(eingabe); 
        }
    }

    static void ZeichneSpielfeld() 
    {
        for (int y = 0; y < spielfeld.GetLength(0); y++) //Schleife Zeilen
        {
            for (int x = 0; x < spielfeld.GetLength(1); x++) //Schleife Spalte
            {
                Console.Write(spielfeld[y, x]); //Ausgabe 2D-Array "Spielfeld" 
            }
            Console.WriteLine(); //Neue Zeile nach jeder Zeile im Spielfeld
        }
    }

    static void BewegePacman(ConsoleKey eingabe) 
    {
        int neueX = pacmanX; //Aktuelle Koordinaten werden gesetzt 
        int neueY = pacmanY;

        switch (eingabe)
        {
            case ConsoleKey.W:
                neueY--; //nach oben
                break;
            case ConsoleKey.S:
                neueY++; //nach unten
                break;
            case ConsoleKey.A:
                neueX--; //nach links
                break;
            case ConsoleKey.D:
                neueX++; //nach rechts
                break;
        }

        if (spielfeld[neueY, neueX] != '#') //Prüfen, ob es eine Wand ist
        {
            spielfeld[pacmanY, pacmanX] = ' '; //Alte Position cleanen
            pacmanX = neueX; //Koordinaten werden neu gesetzt 
            pacmanY = neueY;
            spielfeld[pacmanY, pacmanX] = 'P'; //Pacman auf die neue Position setzen
        }
    }
}