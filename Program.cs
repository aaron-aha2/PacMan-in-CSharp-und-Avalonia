using System; //importiert System-Namespace 

class Program
{
    static Spielfeld spielfeld = new Spielfeld(); 
    static Pacman pacman = new Pacman(spielfeld); 

    static void Main(string[] args)
    {
        bool spielLaeuft = true;

        while (spielLaeuft)
        {
            Console.Clear(); //Löscht die Konsole, um das Spielfeld zu aktualisieren bzw. neu zu zeichen
            spielfeld.ZeichneSpielfeld(); 
            ConsoleKey eingabe = Console.ReadKey(true).Key; //Liest Eingabe ein
            pacman.BewegePacman(eingabe);
        }
    }
}
