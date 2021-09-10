using System;
using System.Threading.Tasks;

namespace DIOGames.Repository
{
    public interface IRepository<Entity>
    {
        Task<Entity> Get(Guid id);
        Task Insert(Entity entity);
        Task Update(Entity entity);
        Task Delete(Guid id);
    }
}
