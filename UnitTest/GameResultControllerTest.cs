using FakeItEasy;
using ScoreBoard.Controllers;
using ScoreBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Common;
using Xunit;

namespace UnitTest
{
    public class GameResultControllerTest
    {
        private readonly GameRepository _gameRepository = null;

        private readonly GameResultController _GameResultController;

        public GameResultControllerTest()
        {
            _gameRepository = A.Fake<GameRepository>();

            _GameResultController = new GameResultController(_gameRepository);
        }

        [Fact]
        public void ContactsShouldListAllContacts()
        {
            var expectedGame = new[] { Constants.Gustavo, Constants.Tuany };
            A.CallTo(() => _contactsQuery.Run()).Returns(expectedContacts);
            Assert.Equal(_contactController.List(), expectedContacts);
            A.CallTo(() => _contactsQuery.Run()).MustHaveHappened();

        }

        [Fact]
        public void GamePointsShouldBeAdded()
        {
            _GameResultController.Post(Constants.player1);

            A.CallTo(() => _gameRepository.Run(Constants.Gustavo)).MustHaveHappened();
        }  

    }
}
