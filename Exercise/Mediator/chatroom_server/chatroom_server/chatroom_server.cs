using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Mediator;

namespace chatroom_server
{

    class chatroom_client: IColleague<string>
    {
        Socket _handler;
        byte[] _buffer = new byte[1000];
        IMediator<string> _chatroomHandler;
        public chatroom_client(Socket handler, IMediator<string> chatroomHandler)
        {
            _handler = handler;
            _chatroomHandler = chatroomHandler;

            _chatroomHandler.register(this);
        }

        public void run()
        {


            string msg = null;
            
            while(true)
            {
                int rec = _handler.Receive(_buffer);

                msg += Encoding.ASCII.GetString(_buffer, 0, rec);

                if(msg.IndexOf("<EOF>") > -1)
                {
                    break;
                }

            }

            broadcastMsg(msg);

        }

        public void receiveMsg(string msg)
        {

            byte[] rawmsg = Encoding.ASCII.GetBytes(msg);
            _handler.Send(rawmsg);
        }

        public void broadcastMsg(string msg)
        {
            _chatroomHandler.broadcastMsg(this, msg);
        }
    }


    class chatroom_server
    {

        ConcreteMediator<string> _chatroomHandler = new ConcreteMediator<string>();

        object _locking = new object();

        private uint _port; 
        public chatroom_server(uint port)
        {
            _port = port;
        }

        public void startServer()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(20);

            List<Task> clientTasks = new List<Task>();

            Task server = new Task(() =>
            {
                while(true)
                {
                    Socket handler = listener.Accept();
                    Console.WriteLine("new client connected");
                    
                    Task client = new Task(() =>
                    {
                        var ClientHandler = new chatroom_client(handler, _chatroomHandler);

                        while(true)
                        {
                            ClientHandler.run();
                        }

                    });
                    clientTasks.Add(client);
                    client.Start();
                }
            });

            server.Start();
            
        }
    }
}
