using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIOGames.ViewModel;
using DIOGames.InputModel;

namespace DIOGames.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<ViewModelGame>> Get(int page, int quantity);
        Task<ViewModelGame> Get(Guid id);
        Task<ViewModelGame> Insert(InputModelGame game);
        Task Update(Guid id, InputModelGame game);
        Task Update(Guid id, decimal price);
        Task Delete(Guid id);
    }
}
