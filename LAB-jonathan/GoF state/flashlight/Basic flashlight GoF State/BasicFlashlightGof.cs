using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashlight
{

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


    interface IGoFFlashlightState
    {
        void HandlePower(GoFFlashlight fl);
    }

    internal class ON : IGoFFlashlightState
    {

        public void HandlePower(GoFFlashlight fl)
        {
 	        fl.LigthOff();
            fl.SetState(GoFFlashlight.Soff);
        }
    }

    internal class OFF : IGoFFlashlightState
    {

        public void HandlePower(GoFFlashlight fl)
        {
 	        fl.LigthOn();
            fl.SetState(GoFFlashlight.Son);
        }
    }


    class GoFFlashlight
    {
        
        static internal IGoFFlashlightState Son = new ON();
        static internal IGoFFlashlightState Soff = new OFF();

        internal IGoFFlashlightState state = Soff;

        private IFlashlightController FLcontroller;

        public GoFFlashlight(IFlashlightController flc)
        {
            FLcontroller = flc;
        }

        public void Power()
        {
            state.HandlePower(this);
        }

        internal void LigthOn()
        {
            FLcontroller.ligthON();
        }

        internal void LigthOff()
        {
            FLcontroller.ligthOFF();
        }

        internal void SetState(IGoFFlashlightState newState)
        {
            state = newState;
        }
    }
}
