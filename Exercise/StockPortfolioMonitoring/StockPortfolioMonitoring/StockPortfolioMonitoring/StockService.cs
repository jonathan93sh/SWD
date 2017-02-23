using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeMade.Observer;
using System.Threading;

namespace StockService
{


    class Stock : subject_HM<Stock>
    {
        private string Name_;

        private float Value_;

        public float Value
        {
            get { return Value_; }
            set 
            {
                Value_ = value;
                Notify(this);
            }
        }

        public Stock(string name, float value)
        {
            Name_ = name;
            Value = value;
        }

        public string getName()
        {
            return Name_;
        }

    }


    class Portfolio : IObserver_HM<Stock>
    {
        private List<Stock> stocks_ = new List<Stock>();
        private string Name_;
        private PortfolioDisplay display_;


        public PortfolioDisplay display
        {
            private get
            {
                return display_;
            }
            set
            {
                display_ = value;
            }
        }

        public Portfolio(string name)
        {
            display = null;
            Name_ = name;
        }

        public void addStock(Stock stock)
        {
            stock.Attach(this);
            stocks_.Add(stock);
        }

        public void removeStock(Stock stock)
        {
            stock.Detach(this);
            stocks_.Remove(stock);
        }

        public List<Stock> getStocks()
        {
            return stocks_;
        }

        public void displayUpdate()
        {
            if (display == null)
                return;
            display.update(this);
        }

        public void update(Stock subject)
        {
            if (display == null)
                return;

            display.update(this, subject);
        }
        public string getName()
        {
            return Name_;
        }
    }

    class PortfolioDisplay
    {
        private static Mutex mut = new Mutex();

        string note_;

        public PortfolioDisplay(string note)
        {
            note_ = note;
        }

        public void update(Portfolio p, Stock last)
        {
            mut.WaitOne();
            {
                Console.Clear();
                if(note_ != "")
                {
                    Console.WriteLine("Note: " + note_);
                    Console.WriteLine("-----------------------------");
                }
                
                Console.WriteLine("Portfolio name : " + p.getName());
                if (last != null)
                {
                    Console.WriteLine("Lastest news : stock name '" + last.getName() + "' current value is: " + last.Value);
                }
                Console.WriteLine("------------------- Stock list -----------------------");
                foreach (Stock s in p.getStocks())
                {
                    Console.WriteLine("stock name '" + s.getName() + "' current value is: " + s.Value);
                }
                Console.WriteLine("------------------- Stock list end -------------------");
            }
            mut.ReleaseMutex();
        }

        public void update(Portfolio p)
        {
            update(p, null);
        }
    }

}
