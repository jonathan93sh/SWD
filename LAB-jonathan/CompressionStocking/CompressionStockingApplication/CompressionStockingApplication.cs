using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompressionStocking;
using System.Threading.Tasks;


namespace CompressionStockingApplication
{

    class StubCompressionCtrl : ICompressionCtrl
    {
        public void Compress()
        {
            Console.WriteLine("StubCompressionCtrl::Compress() called");
        }

        public void Decompress()
        {
            Console.WriteLine("StubCompressionCtrl::Decompress() called");
        }
    }

    class RealCompressionCtrl : ICompressionCtrl
    {
        
        private ICompression compressor_;
        ILed green_;
        ILed red_;
        IVibrate vibrateD_;
        public RealCompressionCtrl(ICompression com, ILed green, ILed red, IVibrate vibrateD)
        {
            green_ = green;
            red_ = red;
            vibrateD_ = vibrateD;
            compressor_ = com;
        }

        public void Compress()
        {
            if(!compressor_.isRunning())
            {
                
                green_.turnOn();
                vibrateD_.turnOn();
                compressor_.startPump();
                Console.WriteLine("Done.");
                green_.turnOff();
                vibrateD_.turnOff();
            }
            else
            {
                Console.WriteLine("Is compress.");
            }
        }

        public void Decompress()
        {
            if (compressor_.isRunning())
            {
                
                red_.turnOn();
                vibrateD_.turnOn();
                compressor_.stopPump();
                Console.WriteLine("Done.");
                red_.turnOff();
                vibrateD_.turnOff();
            }
            else
            {
                Console.WriteLine("Are all ready decompress.");
            }
        }

        
    }

    interface ICompression
    {
        bool isRunning();
        void startPump();
        void stopPump();
    }

    class RealCompression : ICompression
    {
        private bool running_;

        public RealCompression()
        {
            running_ = false;
        }

        public bool isRunning()
        {
            return running_;
        }

        public void startPump()
        {
            Console.WriteLine("Start pumping");
            System.Threading.Thread.Sleep(5000);
            running_ = true;
        }
        public void stopPump()
        {
            Console.WriteLine("stop pumping");
            System.Threading.Thread.Sleep(2000);
            running_ = false;
        }
    }

    class LaceCompression : ICompression
    {
        private bool running_;

        public LaceCompression()
        {
            running_ = false;
        }

        public bool isRunning()
        {
            return running_;
        }

        public void startPump()
        {
            Console.WriteLine("Start Lace'ing");
            
            running_ = true;
        }
        public void stopPump()
        {
            Console.WriteLine("stop Lace'ing");
            for (int i = 0; i < 30; i++ )
            {
                Console.Write("click ");
                System.Threading.Thread.Sleep(100);
            }
            Console.Write("\r\n");
            running_ = false;
        }
    }

    interface ILed
    {
        void turnOn();
        void turnOff();
    }

    class ColorLed : ILed
    {
        private readonly string Color_;

        public ColorLed(string Colorname)
        {
            Color_ = Colorname;
        }

        public void turnOn()
        {
            Console.WriteLine(Color_ + "-LED is on");
        }

        public void turnOff()
        {
            Console.WriteLine(Color_ + "-LED is off");
        }

    }

    interface IVibrate
    {
        void turnOn();

        void turnOff();
    }

    class VibrateDevice : IVibrate
    {
        public void turnOn()
        {
            Console.WriteLine("Vibrate is on");
        }

        public void turnOff()
        {
            Console.WriteLine("Vibrate is off");
        }
    }

    class CompressionStockingApplication
    {
        static void Main(string[] args)
        {
            var compressionStockingstocking = new StockingCtrl(new RealCompressionCtrl(new LaceCompression(),
                                                                                        new ColorLed("GREEN"), 
                                                                                        new ColorLed("RED"), 
                                                                                        new VibrateDevice()));
            ConsoleKeyInfo consoleKeyInfo;
            
            Console.WriteLine("Compression Stocking Control User Interface");
            Console.WriteLine("A:   Compress");
            Console.WriteLine("Z:   Decompress");
            Console.WriteLine("ESC: Terminate application");

            do
            {
                consoleKeyInfo = Console.ReadKey(true); // true = do not echo character
                if (consoleKeyInfo.Key == ConsoleKey.A)  compressionStockingstocking.StartBtnPushed();
                if (consoleKeyInfo.Key == ConsoleKey.Z)  compressionStockingstocking.StopBtnPushed();

            } while (consoleKeyInfo.Key != ConsoleKey.Escape);
        }
    }
}
