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
                
                int key = Console.Read();
                if((char)key == 'p')
                {
                    bfl.HandleEvent(Event.Power);
                }
                else if((char)key == 'e')
                {
                    bfl.HandleEvent(Event.onEnter);
                }
                


            }

        }
    }
}
