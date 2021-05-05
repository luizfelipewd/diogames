using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using diogames.InputModel;
using diogames.ViewModel;

namespace diogames.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Get(int page, int quantity);
        Task<GameViewModel> Get(Guid id);
        Task<GameViewModel> Post(GameInputModel id);
        Task Put(Guid id, GameInputModel game);
        Task Patch(Guid id, double price);
        Task Delete(Guid id);
    }
}