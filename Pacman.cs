public class Pacman 
{
    private Spielfeld spielfeld;
    public int pacmanX = 1; //Startposition Pacman x-Koordinate
    public int pacmanY = 1; //Startposition Pacman y-Koordinate

    public int score = 0;
    public int leben = 3;

    public Pacman(Spielfeld spielfeld)
    {
        this.spielfeld = spielfeld;
    }

    public void BewegePacman(ConsoleKey eingabe) 
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

        if (spielfeld.Feld[neueY, neueX] != '#')
        {
            if (spielfeld.Feld[neueY, neueX] == '.')
            {
                score += 10;
            }
            spielfeld.Feld[pacmanY, pacmanX] = ' '; //Alte Position cleanen
            pacmanX = neueX; //Koordinaten werden neu gesetzt
            pacmanY = neueY;
            spielfeld.Feld[pacmanY, pacmanX] = 'P'; //Pacman auf die neue Position setzen
        }
    }
}
