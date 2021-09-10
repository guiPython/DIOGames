using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIOGames.Entities;

namespace DIOGames.Repository
{
    public interface IGameRepository : IDisposable, IRepository<Game>
    {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> Get(string name, string producer);
    }
}
