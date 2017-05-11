using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;  

// Laimonas & Jonathan - Gruppe 11, E6SWD.

namespace chatroom_server
{
    class Program
    {
        static void Main(string[] args)
        {
            // opretter server.
            var chatserver = new chatroom_server(3000u);

            // starter server.
            chatserver.startServer();

            while (true) { }

        }
    }
}
