using System;

namespace diogames.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public double Price { get; set; }
    }
}