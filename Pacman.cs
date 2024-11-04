public class Pacman 
{
    private Spielfeld spielfeld;
    public int pacmanX = 1; //Startposition Pacman x-Koordinate
    public int pacmanY = 1; //Startposition Pacman y-Koordinate

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
                neueX--; //nach oben
                break;
            case ConsoleKey.S:
                neueX++; //nach unten
                break;
            case ConsoleKey.A:
                neueY--; //nach links
                break;
            case ConsoleKey.D:
                neueY++; //nach rechts
                break;
        }

        if (spielfeld.Feld[neueX, neueY] != '#') //Prüfen, ob es eine Wand ist
        {
            spielfeld.Feld[pacmanX, pacmanY] = ' '; //Alte Position cleanen
            pacmanX = neueX; //Koordinaten werden neu gesetzt
            pacmanY = neueY;
            spielfeld.Feld[pacmanX, pacmanY] = 'P'; //Pacman auf die neue Position setzen
        }
    }
}
