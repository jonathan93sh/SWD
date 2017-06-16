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


    abstract class IGoFFlashlightState
    {
        virtual public void onEnter(IstateFlashligth fl) { }
        virtual public void HandlePower(IstateFlashligth fl) { }
        virtual public void HandleOnEnter(IstateFlashligth fl) { }
    }

    internal class ON : IGoFFlashlightState
    {
        override public void onEnter(IstateFlashligth fl) 
        {
            fl.LigthOn();
            fl.SetState(GoFFlashlight.Ssolid);
        }
        override public void HandlePower(IstateFlashligth fl)
        {
            fl.SetState(GoFFlashlight.Soff);
        }
    }

    internal class SOLID : ON
    {
        override public void onEnter(IstateFlashligth fl)
        {
            fl.solidLEDs();   
        }
        override public void HandleOnEnter(IstateFlashligth fl)
        {
            fl.SetState(GoFFlashlight.Sflashing);
        }
    }

    internal class FLASHING : ON
    {
        override public void onEnter(IstateFlashligth fl)
        {
            fl.flashLEDs();
        }
        override public void HandleOnEnter(IstateFlashligth fl)
        {
            fl.SetState(GoFFlashlight.Ssolid);
        }
    }

    internal class OFF : IGoFFlashlightState
    {
        override public void onEnter(IstateFlashligth fl)
        {
            fl.LigthOff();
        }
        override public void HandlePower(IstateFlashligth fl)
        {
            fl.SetState(GoFFlashlight.Son);
            
        }
    }


    interface IuserFlashligth
    {
        void Power();
        void onEnter();
    }

    interface IstateFlashligth
    {


        void LigthOn();
        
        void LigthOff();

        void solidLEDs();
        void flashLEDs();

        void SetState(IGoFFlashlightState newState);
    }

    class GoFFlashlight : IuserFlashligth, IstateFlashligth
    {

        static public IGoFFlashlightState Son = new ON();
        static public IGoFFlashlightState Sflashing = new FLASHING();
        static public IGoFFlashlightState Ssolid = new SOLID();

        static public IGoFFlashlightState Soff = new OFF();

        private IGoFFlashlightState state = Soff;

        private IFlashlightController FLcontroller;

        public GoFFlashlight(IFlashlightController flc)
        {
            FLcontroller = flc;
            SetState(Soff);
        }

        public void Power()
        {
            state.HandlePower(this);
        }

        public void onEnter()
        {
            state.HandleOnEnter(this);
        }

        public void LigthOn()
        {
            FLcontroller.ligthON();
        }

        public void LigthOff()
        {
            FLcontroller.ligthOFF();
        }

        public void solidLEDs()
        {
            FLcontroller.solidLEDs();
        }
        public void flashLEDs()
        {
            FLcontroller.flashLEDs();
        }

        public void SetState(IGoFFlashlightState newState)
        {
            state = newState;
            state.onEnter(this);
        }
    }
}
