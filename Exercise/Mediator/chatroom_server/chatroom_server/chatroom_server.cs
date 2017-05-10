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
    /**
     * Denne klasse står for håndtering af klienter. beskeder frem og tilbage mellem server og klienter.
     **/
    class chatroom_client: IColleague<string>
    {
        private bool _close = false;

        // bruges til at se om klienten har afsluttet forbindelsen.
        public bool close
        {
            private set
            {
                _close = value;
            }

            get
            {
                return _close;
            }
            
        }

        // Den socket klienten køre på.
        Socket _handler;
        // en buffer der bruges til at modtage payload fra klienten.
        byte[] _buffer = new byte[1000];
        // den mediator der bliver brugt. Som står for håndtering af beskeder.
        IMediator<string> _chatroomHandler;
        // param handler : den specifikke socket klienten køre på.
        // param chatroomHandler : den mediator der skal bruges.
        public chatroom_client(Socket handler, IMediator<string> chatroomHandler)
        {
            _handler = handler;
            _chatroomHandler = chatroomHandler;

            _chatroomHandler.register(this);
        }


        // Denne metode venter på modtage en besked fra klient, og derefter sende den igennem mediatoren.
        public void run()
        {


            string msg = null;
            

                try
                {
                    int rec = _handler.Receive(_buffer);
                    msg += Encoding.ASCII.GetString(_buffer, 0, rec);
                    broadcastMsg(msg);
                }
                catch(System.Net.Sockets.SocketException e)
                {
                    Console.WriteLine("client exit");
                    _chatroomHandler.unregister(this);
                    close = true;
                    
                }



            

        }

        // Denne metode bliver kaldt af mediatoren(chatroom handler) hver gang der bliver sendt en ny besked rundt.
        public void receiveMsg(string msg)
        {

            byte[] rawmsg = Encoding.ASCII.GetBytes(msg);
            try
            {
                _handler.Send(rawmsg);
            }
            catch(System.Net.Sockets.SocketException e)
            {
                Console.WriteLine("can't send msg to client!!!");
                close = true;
            }
            
        }

        // Bruges til at sende besked rundt til de andre klienter.
        public void broadcastMsg(string msg)
        {
            _chatroomHandler.broadcastMsg(this, msg);
        }
    }

    /**
     * Denne klasse står for oprettelse af server, og styring af sockets.
     **/
    class chatroom_server
    {
        // mediator bruges som en chatroom handler.
        ConcreteMediator_ThreadSafe<string> _chatroomHandler = new ConcreteMediator_ThreadSafe<string>();

        // den port serveren skal køre på.
        private uint _port; 
        public chatroom_server(uint port)
        {
            _port = port;
        }

        // denne metode skal kaldes for at starte serveren. hvor efter oprettelse vil den køre velkomst socket på en anden tråd, og hver klient der forbinder sig til serveren vil få deres egen tråd at køre på.
        public void startServer()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, (int)_port);

            // oprettelse af socket.
            Socket listener;
            try
            {
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);
            }
            catch(SocketException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Server Error: server not started!!!");
                return;
            }

            

            List<Task> clientTasks = new List<Task>();

            //Socket handler = listener.Accept();
            //Console.WriteLine("new client connected");

            // laver en ny task som serveren kan køre på.
            Task server = new Task(() =>
            {
                while(true)
                {
                    // venter på at en klient opretter forbindelse.
                    Socket handler;
                    try
                    {
                        handler = listener.Accept();
                        Console.WriteLine("new client connected");
                    }
                    catch(SocketException e)
                    {
                        Console.WriteLine(e.ToString());
                        Console.WriteLine("Server Error: server shutdown!!!");
                        return;
                    }
                     
                    
                    // opretter en ny task når en klient har oprettet forbindelse.
                    Task client = new Task(() =>
                    {
                        // Der bliver oprettet et chatroom_client objekt, som bliver linket til den socket klienten har og til mediatoren(chatroom handler).
                        var ClientHandler = new chatroom_client(handler, _chatroomHandler);

                        // life of client.
                        while(true)
                        {
                            ClientHandler.run();
                            if (ClientHandler.close)
                                break;
                        }

                    });
                    clientTasks.Add(client);
                    // starter den nye klient task.
                    client.Start();
                }
            });
            // starter serveren.
            server.Start();
            
        }
    }
}
