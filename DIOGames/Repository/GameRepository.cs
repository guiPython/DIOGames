using DIOGames.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DIOGames.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameRepository(IConfiguration configuration)
        {
            sqlConnection =  new SqlConnection(configuration.GetConnectionString("sql_server"));
        }
        public async Task<List<Game>> Get(int page, int quantity)
        {
            var games = new List<Game>();
            var query = $"select * from games order by id offset {(page -1) * quantity} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read()) 
                games.Add(new Game(Guid.Parse((string) reader["id"]), (string) reader["name"], (string) reader["producer"], (decimal) reader["price"]));

            await sqlConnection.CloseAsync();
            return games;
        }

        public async Task<Game> Get(Guid id)
        {
            var query = $"select * from games where id = '{id}'";
            Game game =  null;

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read())
                game = new Game(Guid.Parse((string)reader["id"]), (string) reader["name"], (string) reader["producer"], (decimal) reader["price"]);

            await sqlConnection.CloseAsync();
            return game;
        }

        public async Task<Game> Get(string name, string producer)
        {
            var query = $"select * from games where name = '{name}' and producer = '{producer}'";
            Game game = null;

            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read())
                game = new Game(Guid.Parse((string)reader["id"]), (string) reader["name"], (string) reader["producer"], (decimal) reader["price"]);

            await sqlConnection.CloseAsync();
            return game;
        }

        public async Task Insert(Game game)
        {
            var query = $"insert into games (id, name, producer, price) values('{game.Id}','{game.Name}','{game.Producer}','{game.Price.ToString().Replace(',', '.')}')";

            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand(query, sqlConnection);

            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var query = $"update games set name = '{game.Name}', producer = '{game.Producer}', price = '{game.Price.ToString().Replace(',','.')}' where id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand(query, sqlConnection);

            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Delete(Guid id)
        {
            var query = $"delete from games where id='{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand(query, sqlConnection);

            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}
