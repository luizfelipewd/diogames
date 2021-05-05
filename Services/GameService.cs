using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diogames.Entities;
using diogames.Exceptions;
using diogames.InputModel;
using diogames.Repositories;
using diogames.ViewModel;

namespace diogames.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task Delete(Guid id)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null)
                throw new GameDoesNotExistsException();

            await _gameRepository.Delete(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }

        public async Task<List<GameViewModel>> Get(int page, int quantity)
        {
            var games = await _gameRepository.Get(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Company = game.Company,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Company = game.Company,
                Price = game.Price
            };
        }

        public async Task Patch(Guid id, double price)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null)
                throw new GameDoesNotExistsException();

            gameEntity.Price = price;

            await _gameRepository.Put(gameEntity);
        }

        public async Task<GameViewModel> Post(GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(game.Name, game.Company);

            if (gameEntity.Count > 0)
                throw new GameAlreadyExistsException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Company = game.Company,
                Price = game.Price
            };

            await _gameRepository.Post(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Company = game.Company,
                Price = game.Price
            };
        }

        public async Task Put(Guid id, GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null)
                throw new GameDoesNotExistsException();

            gameEntity.Name = game.Name;
            gameEntity.Company = game.Company;
            gameEntity.Price = game.Price;

            await _gameRepository.Put(gameEntity);
        }
    }
}