using System; //importiert System-Namespace 

class Program
{
    static Spielfeld spielfeld = new Spielfeld();
    static int pacmanX = 1; //Startposition von Pacman (X-Koordinate)
    static int pacmanY = 1; //Startposition von Pacman (Y-Koordinate)

    static void Main(string[] args) //Main Funktion
    {
        bool spielLaeuft = true; //Zustandsvariable

        while (spielLaeuft) //Hauptschleife des Spiels
        {
            Console.Clear(); //Löscht die Konsole, um das Spielfeld zu aktualisieren bzw. neu zu zeichen
            spielfeld.ZeichneSpielfeld(); 
            ConsoleKey eingabe = Console.ReadKey(true).Key; //Liest Eingabe ein ohne Konsolenausgabe 
            BewegePacman(eingabe); 
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

        if (spielfeld.Feld[neueY, neueX] != '#') //Prüfen, ob es eine Wand ist
        {
            spielfeld.Feld[pacmanY, pacmanX] = ' '; //Alte Position cleanen
            pacmanX = neueX; //Koordinaten werden neu gesetzt 
            pacmanY = neueY;
            spielfeld.Feld[pacmanY, pacmanX] = 'P'; //Pacman auf die neue Position setzen
        }
    }
}