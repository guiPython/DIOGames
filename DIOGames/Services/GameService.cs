using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIOGames.InputModel;
using DIOGames.Repository;
using DIOGames.ViewModel;
using DIOGames.Entities;
using DIOGames.Exceptions;

namespace DIOGames.Services
{
    public class GameService : IGameService, IDisposable
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async  Task<List<ViewModelGame>> Get(int page, int quantity)
        {
            var games = await _gameRepository.Get(page, quantity);

            var query = from game in games select new ViewModelGame(game);

            return query.ToList();;
        }

        public async Task<ViewModelGame> Get(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null) return null;

            return new ViewModelGame(game);
        }

        public async Task<ViewModelGame> Insert(InputModelGame game)
        {
            var gameEntity = await _gameRepository.Get(game.Name, game.Producer);

            if(gameEntity != null) throw new GameAlreadyExistsException();

            var gameInsert = new Game(Guid.NewGuid(), game.Name, game.Producer, game.Price);

            await _gameRepository.Insert(gameInsert);

            return new ViewModelGame(gameInsert);
        }

        public async Task Update(Guid id, InputModelGame game)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null) throw new GameNotExistsException();

            gameEntity.Name = game.Name;
            gameEntity.Price = game.Price;
            gameEntity.Producer = game.Producer;

            await _gameRepository.Update(gameEntity); 
        }

        public async Task Update(Guid id, decimal price)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null) throw new GameNotExistsException();

            gameEntity.Price = price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task Delete(Guid id)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null) throw new GameNotExistsException();

            await _gameRepository.Delete(id);
        }

        public void Dispose()
        {
            _gameRepository.Dispose();
        }

    }
}
