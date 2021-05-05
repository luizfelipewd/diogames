using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using diogames.Entities;
using Microsoft.Extensions.Configuration;

namespace diogames.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {

        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task Delete(Guid id)
        {
            var command = $"remove from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task<List<Game>> Get(int page, int quantity)
        {
            var games = new List<Game>();
            var command = $"select * from Games by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Company = (string)sqlDataReader["Company"],
                    Price = (double)sqlDataReader["Price"]
                });
            }
            await sqlConnection.CloseAsync();

            return games;

        }

        public async Task<Game> Get(Guid id)
        {
            Game game = null;
            var command = $"select * from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Company = (string)sqlDataReader["Company"],
                    Price = (double)sqlDataReader["Price"]
                };
            }
            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> Get(string name, string company)
        {
            var games = new List<Game>();
            var command = $"select * from Games where Name = '{name}' and Company = '{company}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Company = (string)sqlDataReader["Company"],
                    Price = (double)sqlDataReader["Price"]
                });
            }
            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task Post(Game game)
        {
            var command = $"insert Games (Id, Name, Company, Price) values ({game.Id},{game.Name},{game.Company}, {game.Price})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }

        public async Task Put(Game game)
        {
            var command = $"update Games set Name={game.Name}, Company={game.Company}, Price={game.Price.ToString()} where Id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }
    }
}