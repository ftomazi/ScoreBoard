using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    [SerializableAttribute]
    public class MessageModel
    {
        public int PayerId { get; set; }
        public int GameId { get; set; }
        public int Win { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
