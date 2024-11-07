namespace PacManGame.Models
{
    // Klasse, die das Spielfeld im Pac-Man-Spiel repräsentiert
    public class Gamefield
    {
        // Ein zweidimensionales Array, das die Struktur des Spielfelds speichert
        public int[,] GameFieldData { get; set; }

        // Konstruktor, der die Spielfeldstruktur initialisiert
        public Gamefield()
        {
            // Beispiel-Spielfeldstruktur: 0 = leer, 1 = Wand, 2 = Punkt
            GameFieldData = new int[,] 
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 1, 0, 2, 0, 2, 0, 1, 0, 2, 2, 0, 0, 2, 1},
                { 1, 2, 0, 1, 0, 0, 2, 2, 1, 1, 0, 0, 0, 1},
                { 1, 0, 2, 0, 2, 0, 1, 0, 2, 2, 0, 0, 2, 1},
                { 1, 2, 0, 1, 0, 0, 2, 2, 1, 1, 0, 0, 0, 1},
                { 1, 0, 2, 0, 2, 0, 1, 0, 2, 2, 0, 0, 2, 1},
                { 1, 2, 0, 1, 0, 0, 2, 2, 1, 1, 0, 0, 0, 1},
                { 1, 0, 2, 0, 2, 0, 1, 0, 2, 2, 0, 0, 2, 1},
                { 1, 2, 0, 1, 0, 0, 2, 2, 1, 1, 0, 0, 0, 1},
                { 1, 0, 2, 0, 2, 0, 1, 0, 2, 2, 0, 0, 2, 1},
                { 1, 2, 0, 1, 0, 0, 2, 2, 1, 1, 0, 0, 0, 1},
                { 1, 0, 2, 0, 2, 0, 1, 0, 2, 2, 0, 0, 2, 1},
                { 1, 0, 2, 0, 2, 0, 1, 0, 2, 2, 0, 0, 2, 1},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            };

        }
    }
}
