using System;

namespace DIOGames.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
        public Game(Guid id, string name, string producer, decimal price)
        {
            Id = id;
            Name = name;
            Producer = producer;
            Price = price;
        }
    }
}

