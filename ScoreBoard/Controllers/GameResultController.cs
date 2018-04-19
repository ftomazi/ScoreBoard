using ScoreBoard.Data;
using ScoreBoard.Model;
using System.Web.Http;

namespace ScoreBoard.Controllers
{
    public class GameResultController : ApiController
    {
        private readonly GameRepository _gameRepository = null;

        public GameResultController(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]PlayerModel value)
        {
            // joga na fila MSMQ p processamento
            _gameRepository.InsertPoints(value);
        }

    }
}
