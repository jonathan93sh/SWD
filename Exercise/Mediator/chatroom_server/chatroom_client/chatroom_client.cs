using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;  

namespace chatroom_client
{
    class chatroom_client
    {
        Socket _sender;

        public chatroom_client(IPAddress remoteIP, uint port)
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];  
            IPEndPoint remoteEP = new IPEndPoint(remoteIP,(int)port);

            _sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _sender.Connect(remoteEP);
            Console.WriteLine("Socket connected to {0}",
                    _sender.RemoteEndPoint.ToString()); 
        }



    }
}
