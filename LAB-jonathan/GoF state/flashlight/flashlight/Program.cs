using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashlight
{
    class Program
    {

        static void Main(string[] args)
        {
            BasicFlashlightSTM bfl = new BasicFlashlightSTM(new consoleFlashlight());
            

            while(true)
            {
                Console.ReadLine();
                bfl.HandleEvent(Event.Power);


            }

        }
    }
}
