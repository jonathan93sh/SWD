using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;  

namespace chatroom_client
{
    /**
     * Denne klasse står for håndtering af forbindelsen mellem klient og server, fra klients side.
     **/
    class chatroom_client
    {
        // den socket der bliver brugt af klienten.
        Socket _sender;
        // en buffer til at modtage beskeder.
        byte[] buffer = new byte[1000];

        private bool close = false;
        // opretter forbindelsen mellem klient og server med et callback hver gang den modtager en ny besked fra server.
        // param remoteIP : ip adressen på serveren.
        // param port : porten der skal bruges.
        public chatroom_client(IPAddress remoteIP, uint port)
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];  
            IPEndPoint remoteEP = new IPEndPoint(remoteIP,(int)port);
            try
            {
                _sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _sender.Connect(remoteEP);
                Console.WriteLine("Socket connected to {0}",
                        _sender.RemoteEndPoint.ToString());

                _sender.BeginReceive(buffer, 0, 1000, 0, new AsyncCallback(receiveCallback), this);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                close = true;
                Console.WriteLine("Cant connect to server!");
            }
            
        }
        // står for håndtering af at sende beskeder.
        public void run()
        {

            while(true)
            {
                if (close)
                    break;
                string read = Console.ReadLine();
                try
                {
                    _sender.Send(Encoding.ASCII.GetBytes(read));
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    break;
                }
                
            }
        }
        // står for håndtering af de beskeder der bliver modtaget.
        private static void receiveCallback(IAsyncResult ar)
        {
            chatroom_client client = (chatroom_client)ar.AsyncState;

            try
            {
                if(client._sender == null)
                {
                    client.close = true;
                    Console.WriteLine("server shutdown");
                    return;
                }

                int rec = client._sender.EndReceive(ar);

                string str = Encoding.ASCII.GetString(client.buffer, 0, rec);

                Console.WriteLine("Received: " + str);

                client._sender.BeginReceive(client.buffer, 0, 1000, 0, new AsyncCallback(receiveCallback), client);
            }
            catch(System.Net.Sockets.SocketException e)
            {
                client._sender.Disconnect(true);
                client.close = true;
                Console.WriteLine("server shutdown");
            }
            

        }
    }
}
