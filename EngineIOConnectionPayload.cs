using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIOWSClient
{
    public class EngineIOConnectionPayload
    {
        public required string sid { get; set; }
        public required string[] upgrades { get; set; }
        public int pingInterval { get; set; } = -1;
        public int pingTimeout { get; set; } = -1;
        public int maxPayload { get; set; }
    }
}
