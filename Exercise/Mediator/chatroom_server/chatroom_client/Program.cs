using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace chatroom_client
{
    class Program
    {
        static void Main(string[] args)
        {

            byte[] ipraw = new byte[4];


            if(args.Length == 1 && args[0].Split('.').Length == 4)
            {
                for (var i = 0; i < 4; i++ )
                {
                    ipraw[i] = (byte)int.Parse(args[0].Split('.')[i]);
                }
                    

            }
            else
            {
                ipraw[0] = 127;
                ipraw[1] = 0;
                ipraw[2] = 0;
                ipraw[3] = 1;
            }

            

            IPAddress remote = new IPAddress(ipraw);

            var client = new chatroom_client(remote, 3000u);

            client.run();

        }
    }
}
