using ScoreBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Common
{
    public class Constants
    {
        private Constants()
        { }

        public static readonly PlayerModel player1 = new PlayerModel { GameId = 1, PlayerId = 1, TimeStamp = DateTime.Today, Win = 100 };

    }
}
