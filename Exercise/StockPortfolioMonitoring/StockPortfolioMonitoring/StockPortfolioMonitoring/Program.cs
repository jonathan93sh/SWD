using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StockService;

namespace StockPortfolioMonitoring
{
    /**
     * Denne klasse bruges til at give lidt liv til de forskellige stocks.
     * 
     **/
    class LifeOfStock
    {
        private Stock stock_;
        Random r;
        public LifeOfStock(Stock stock, int seed)
        {
            r = new Random(seed);
            stock_ = stock;
        }

        public LifeOfStock(Stock stock)
        {
            r = new Random();
            stock_ = stock;
        }
        public void run()
        {
            
            while(true)
            {
                System.Threading.Thread.Sleep(r.Next(1000,10000));

                stock_.Value = stock_.Value * (1.0f + ((float)r.Next(-50, 50) / 1000.0f));

            }

        }

    }

    class Program
    {
        /**
         * Et lille test program hvor der bliver oprettet en lille liste af stocks, herefter to portfolio'er og et enkelt display.
         * Det skal være muligt at skifte mellem de to portfolio'er med tasterne '1' og '2'. Alt skulle være live update.
         **/
        static void Main(string[] args)
        {
            Random seed = new Random();
            List<Stock> stocks = new List<Stock>();

            stocks.Add(new Stock("Google", 10.5f));
            stocks.Add(new Stock("Sony", 50.2f));
            stocks.Add(new Stock("IBM", 20.2f));
            stocks.Add(new Stock("Facebook", 2320.2f));
            stocks.Add(new Stock("Ebay", 420.2f));
            stocks.Add(new Stock("Airconsole", 620.2f));
            stocks.Add(new Stock("Fitness World", 210.2f));
            stocks.Add(new Stock("Fronter", -202.2f));
            stocks.Add(new Stock("Evil robots", 500.2f));

            List<LifeOfStock> los = new List<LifeOfStock>();
            List<Thread> threads = new List<Thread>();
            foreach(Stock s in stocks)
            {
                los.Add(new LifeOfStock(s, seed.Next()));
            }
            foreach(LifeOfStock l in los)
            {
                threads.Add(new Thread(new ThreadStart(l.run)));
            }
            foreach(Thread t in threads)
            {
                t.Start();
            }


            Portfolio big = new Portfolio("Big portfolio");
            Portfolio small = new Portfolio("Small portfolio");

            foreach(Stock s in stocks)
            {
                big.addStock(s);
            }

            small.addStock(stocks[0]);
            small.addStock(stocks[1]);
            

            PortfolioDisplay display = new PortfolioDisplay("press 1 to see big portfolio, press 2 to see small portfolio");

            display.CurrentPortfolio = big;

            ConsoleKeyInfo consoleKeyInfo;
            while (true)
            {
                consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.D2)
                {
                    display.CurrentPortfolio = small;
                }
                if (consoleKeyInfo.Key == ConsoleKey.D1)
                {
                    display.CurrentPortfolio = big;
                }
            }

            


        }
    }
}
