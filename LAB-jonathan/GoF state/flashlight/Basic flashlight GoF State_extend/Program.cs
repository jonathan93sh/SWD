using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flashlight;

namespace Basic_flashlight_GoF_State
{
    class Program
    {
        static void Main(string[] args)
        {

            IuserFlashligth Goffl = new GoFFlashlight(new consoleFlashlight());

            
            while (true)
            {
                int key = Console.Read();
                if ((char)key == 'p')
                {
                    Goffl.Power();
                }
                else if ((char)key == 'e')
                {
                    Goffl.onEnter();
                }
                
            }


        }
    }
}
