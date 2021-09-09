using System;
using DIOGames.Entities;

namespace DIOGames.ViewModel
{
    public class ViewModelGame
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }

        public ViewModelGame(Game game)
        {
            Id = game.Id;
            Name = game.Name;
            Producer = game.Producer;
            Price = game.Price;
        }
    }
}
