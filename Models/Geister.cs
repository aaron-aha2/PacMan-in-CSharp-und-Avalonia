using System;

namespace PacManGame.Models
{
    //Abstrakte Basisklasse für Geister
    public abstract class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; } = 1;
        public bool IsVulnerable { get; set; } = false; // Standardmäßig nicht verwundbar
        public bool IsDead { get; set; } = false; // Standardmäßig nicht tot
        public string Name { get; set; }
        public abstract void Move(Pacman pacman, Gamefield gamefield);
    }
}
