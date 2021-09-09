using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIOGames.Entities;

namespace DIOGames.Repository
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<Game> Get(string name, string producer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
