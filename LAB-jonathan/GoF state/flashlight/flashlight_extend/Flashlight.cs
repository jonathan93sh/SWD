using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashlight
{
    public enum State { ON, OFF };
    public enum SubState {SOLID, FLASH};
    public enum Event { Power, onEnter };

    interface IFlashlightController
    {
        void ligthON();
        void ligthOFF();
        void solidLEDs();
        void flashLEDs();
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


        public void solidLEDs()
        {
            Console.WriteLine("solid LEDs");
        }

        public void flashLEDs()
        {
            Console.WriteLine("flash LEDs");
        }
    }


    class BasicFlashlightSTM
    {
        

        internal State state = State.OFF;
        internal SubState subState = SubState.SOLID;

        private IFlashlightController FLcontroller;

        public BasicFlashlightSTM(IFlashlightController flc)
        {
            FLcontroller = flc;
            FLcontroller.ligthOFF();

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
                            FLcontroller.solidLEDs();
                            state = State.ON;
                            subState = SubState.SOLID;
                            break;
                        default:
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
                        case Event.onEnter:
                            switch (subState)
                            {
                                case SubState.SOLID:
                                    FLcontroller.flashLEDs();
                                    subState = SubState.FLASH;
                                    break;
                                case SubState.FLASH:
                                    FLcontroller.solidLEDs();
                                    subState = SubState.SOLID;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;

            }
        }

    }
}
