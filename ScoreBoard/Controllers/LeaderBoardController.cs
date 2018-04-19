using ScoreBoard.Data;
using ScoreBoard.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace ScoreBoard.Controllers
{
    public class LeaderBoardController : ApiController
    {
        private readonly GameRepository _gameRepository = null;

        public LeaderBoardController(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public IEnumerable<BoardModel> Get()
        {
            var board = _gameRepository.GetBoard();
            return board;
        }
    }
}
