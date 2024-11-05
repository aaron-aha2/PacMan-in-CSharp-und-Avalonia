
public class Geist
{
    private int x; //Position auf der x-Achse
    private int y; //Position auf der y-Achse
    private readonly char symbol; //Buchstabe
    //getter und setter von x,y und symbol
    public int getsetX {
        get { return x; }
        private set { x = value; }
    }
    public int getsetY {
        get { return y; }
        private set { y = value; }
    }
    public char Symbol {
        get { return symbol; }
    }
    //Konstruktor
    public Geist(int startX, int startY, char symbol) {
        this.x = startX; //Setze die x-Position
        this.y = startY; //Setze die y-Position
        this.symbol = symbol; //Setze das Symbol
    }
    
   public void Bewegeaggresiv(int pacmanX, int pacmanY, char[,] spielfeld) {
    int dx = pacmanX - getsetX; //Unterschied X-Richtung
    int dy = pacmanY - getsetY; //Unterschied Y-Richtung
    int neueX = getsetX;  
    int neueY = getsetY; 

    //Je nach dem welcher Abstand größer ist, bewegt sich der Geist in diese Richtung
    if (Math.Abs(dx) > Math.Abs(dy)) {
        if (dx > 0) {
            neueX += 1; //Bewege nach rechts
        } else {
            neueX -= 1; //Bewege nach links
        }

        //Überprüfe, ob die neue Position nach der Bewegung eine Wand ist
        if (spielfeld[neueY, neueX] == '#') {
            //Wenn rechts eine Wand ist, versuche nach oben oder unten zu gehen
            if (neueY > 0 && spielfeld[neueY - 1, neueX] != '#') {
                neueY -= 1; //Bewege nach oben
            } else if (neueY < spielfeld.GetLength(0) - 1 && spielfeld[neueY + 1, neueX] != '#') {
                neueY += 1; //Bewege nach unten
            }
        }
    } else {
        if (dy > 0) {
            neueY += 1; //Bewege nach unten
        } else {
            neueY -= 1; //Bewege nach oben
        }

        //Überprüfe, ob die neue Position nach der Bewegung eine Wand ist
        if (spielfeld[neueY, neueX] == '#') {
            //Wenn unten eine Wand ist, versuche nach links oder rechts zu gehen
            if (neueX > 0 && spielfeld[neueY, neueX - 1] != '#') {
                neueX -= 1; //Bewege nach links
            } else if (neueX < spielfeld.GetLength(1) - 1 && spielfeld[neueY, neueX + 1] != '#') {
                neueX += 1; //Bewege nach rechts
            }
        }
    }

    //Aktualisiere die Position, wenn es keine Wand gibt
    if (spielfeld[neueY, neueX] != '#') {
        getsetX = neueX; //Aktualisiere die x-Position
        getsetY = neueY; //Aktualisiere die y-Position
    }
}
}
