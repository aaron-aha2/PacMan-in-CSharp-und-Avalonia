
using System;
using System.Collections.Generic;



class Program
{
    
    static Spielfeld spielfeld = new Spielfeld();
    public static Pacman pacman = new Pacman(spielfeld);
    static List<Geist> geister = new List<Geist>();

        static void Main(string[] args)
        {
        
            //Initialisiere einen Geist
            geister.Add(new Geist(17, 7, 'G')); //Beispielposition und Symbol für den Geist
        
            bool spielLaeuft = true;

            while (spielLaeuft)
            {
                Console.Clear(); //Löscht die Konsole, um das Spielfeld zu aktualisieren bzw. neu zu zeichnen
                spielfeld.ZeichneSpielfeld(geister); //Liste der Geister übergeben
                    foreach (var geist in geister)
                    {
                        geist.Bewegeaggresiv(pacman.pacmanX, pacman.pacmanY, spielfeld.Feld);
                    }
                    ConsoleKey eingabe = Console.ReadKey(true).Key; //Liest Eingabe ein
                    pacman.BewegePacman(eingabe);
                    }

        }
}