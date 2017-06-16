using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashlight
{
    public enum State { ON, OFF };
    public enum Event { Power };

    interface IFlashlightController
    {
        void ligthON();
        void ligthOFF();
    }

    class consoleFlashlight : IFlashlightController
    {

        public void ligthON()
        {
            Console.WriteLine("ligth ON");
        }

        public void ligthOFF()
        {
            Console.WriteLine("ligth OFF");
        }
    }


    class BasicFlashlightSTM
    {
        

        internal State state = State.OFF;

        private IFlashlightController FLcontroller;

        public BasicFlashlightSTM(IFlashlightController flc)
        {
            FLcontroller = flc;
        }

        public void HandleEvent(Event e)
        {
            switch(state)
            {
                case State.OFF:
                    switch(e)
                    {
                        case Event.Power:
                            FLcontroller.ligthON();
                            state = State.ON;
                            break;
                    }
                    break;
                case State.ON:
                    switch (e)
                    {
                        case Event.Power:
                            FLcontroller.ligthOFF();
                            state = State.OFF;
                            break;
                    }
                    break;

            }
        }

    }
}
