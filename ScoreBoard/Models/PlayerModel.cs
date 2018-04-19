using System;

namespace ScoreBoard.Model
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int Win { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
