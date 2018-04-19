using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbRepository
{
    public class ScoreRepository
    {
        private readonly string _connectionString;

        public ScoreRepository()
        {
            _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fausto\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\v11.0\PlayerScore.mdf;Integrated Security=True";
        }

        public IEnumerable<ScoreModel> GetScoreBoard()
        {
            var query = @" SELECT top 100 idplayer as PlayerId, Score as Balance, LastUpdate as LastUpdate FROM tableScore order by score desc";
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var scores = sqlConnection.Query<ScoreModel>(query);
                return scores;
            }
        }

        public bool UpdateScorePlayer(int playerId, int points, DateTime lastUpdate)
        {
            string queryUpdate = "update tableScore set score = ((select score from tablescore where idPlayer= @id) + @score), LastUpdate = @lastUpdate where idPlayer=@id";
            string queryInsert = "insert into tableScore values (@id, @score, @lastUpdate)";
            string querySelect = "Select idplayer as PlayerId, Score as Balance, LastUpdate as LastUpdate from tableScore where idplayer = @Id";

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var player = sqlConnection.Query<ScoreModel>(querySelect, new { Id = playerId }).SingleOrDefault();

                if (player != null)
                    sqlConnection.Query<ScoreModel>(queryUpdate, new { id = playerId, score = points, lastUpdate = lastUpdate });
                else
                    sqlConnection.Query<ScoreModel>(queryInsert, new { id = playerId, score = points, lastUpdate = lastUpdate });
            }

            return true;
        }

    }

}

