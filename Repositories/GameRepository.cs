using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diogames.Entities;

namespace diogames.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("3742879d-1af4-4453-9170-b2db296ba529"), new Game{Id = Guid.Parse("3742879d-1af4-4453-9170-b2db296ba529"), Name = "Red Dead Redemption II", Company = "Rockstar Games", Price =250}},
            {Guid.Parse("fef7f18a-6f4f-4899-85dc-21b1e6f1b22b"), new Game{Id = Guid.Parse("fef7f18a-6f4f-4899-85dc-21b1e6f1b22b"), Name = "Hellblade: Senua's Sacrifice", Company = "Ninja Theory", Price =150}},
            {Guid.Parse("ca651129-c465-4bba-ac51-78a15c769107"), new Game{Id = Guid.Parse("ca651129-c465-4bba-ac51-78a15c769107"), Name = "Resident Evil 4", Company = "Capcom", Price =60}},
            {Guid.Parse("40013f4a-31d9-4137-9319-f90075544183"), new Game{Id = Guid.Parse("40013f4a-31d9-4137-9319-f90075544183"), Name = "Pokémon Sword and Shield", Company = "Nintendo", Price =300}},
            {Guid.Parse("68385597-b768-4154-a4b2-9d4f759dcbfc"), new Game{Id = Guid.Parse("68385597-b768-4154-a4b2-9d4f759dcbfc"), Name = "God Of War", Company = "Santa Monica", Price =70}},
            {Guid.Parse("8f095161-8f42-45da-ad74-c8c9e9e2f245"), new Game{Id = Guid.Parse("8f095161-8f42-45da-ad74-c8c9e9e2f245"), Name = "The Last Of Us", Company = "Naughty Dog", Price =250}}
        };

        public Task Delete(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // fecha conexão com o banco
        }

        public Task<List<Game>> Get(int page, int quantity)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Game> Get(Guid id)
        {
            if (!games.ContainsKey(id))
                return null;
            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Get(string name, string company)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Company.Equals(company)).ToList());
        }

        public Task Post(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;

        }

        public Task Put(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }
    }
}