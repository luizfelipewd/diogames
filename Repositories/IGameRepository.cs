using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using diogames.Entities;

namespace diogames.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string company);
        Task Post(Game id);
        Task Put(Game game);
        Task Delete(Guid id);
    }
}