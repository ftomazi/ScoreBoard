using DbRepository;
using ScoreBoard.Model;
using MsmqManager;
using System.Collections.Generic;
using System.Linq;

namespace ScoreBoard.Data
{
    public class GameRepository
    {
        private readonly ScoreRepository _scoreRepository;

        public GameRepository(ScoreRepository scoreRepository)
        {
            _scoreRepository = new ScoreRepository();
        }

        // insere no repositorio da fila msmq
        public bool InsertPoints(PlayerModel player)
        {
            var queue = new ManagerQueue();

            queue.AddItem(new MessageModel()
            {
                GameId = player.GameId,
                PayerId = player.PlayerId,
                Win = player.Win,
                TimeStamp = player.TimeStamp
            });

            return true;
        }

        // busca do banco
        public IEnumerable<BoardModel> GetBoard()
        {
            var scores = _scoreRepository.GetScoreBoard();

            var dados = (from c in scores.Where(p => true)
                         select new BoardModel
                         {
                             Balance = c.Balance,
                             PlayerId = c.PlayerId,
                             TimeStamp = c.LastUpdate
                         }).ToList();

            return dados;
        }
    }
}
