using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;  

namespace chatroom_server
{
    class Program
    {
        static void Main(string[] args)
        {
            var chatserver = new chatroom_server(3000u);

            chatserver.startServer();

            while (true) { }

        }
    }
}
