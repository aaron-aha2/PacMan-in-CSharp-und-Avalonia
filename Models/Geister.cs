using System;

namespace PacManGame.Models
{
    //Abstrakte Basisklasse für Geister
    public abstract class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; } = 1;
        public string Name { get; set; }
        public abstract void Move(Pacman pacman, Gamefield gamefield);
    }

    
    
}
