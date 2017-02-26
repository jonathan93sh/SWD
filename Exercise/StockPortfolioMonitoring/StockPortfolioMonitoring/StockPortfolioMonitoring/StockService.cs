using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeMade.Observer;
using System.Threading;

namespace StockService
{

    /**
     * Stock klassen er en simpel klasse der nedarver subject så det kan overvåges.
     * klassen er sat til at sende nofikationer hver gang dens værdi ændre sig.
     **/
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

        public string Name { get { return Name_; } private set { Name_ = value; } }


        public Stock(string name, float value)
        {
            Name = name;
            Value = value;
        }

    }

    /**
     * Portfolio klassen indeholder en liste over hvilke stocks der bliver fulgt. Klassen er både et subject og en observer af stock.
     * Grunden til at den er et subject, er fordi PortfolioDisplay skal vide hvornår der er sket en ændring i det gældende portfolio.
     * Den er selvfølgelig er den en observer af stock, efter som den er interesseret i at vide hvornår der sker ændringer i de stocks som der bliver fulgt.
     **/
    class Portfolio : subject_HM<Portfolio>, IObserver_HM<Stock>
    {
        private Mutex mut = new Mutex();
        private List<Stock> stocks_ = new List<Stock>();
        private string Name_;
        private Stock LastStock_ = null;

        public string Name { get { return Name_; } private set { Name_ = value; } }

        public Portfolio(string name)
        {
            Name = name;
        }

        public void addStock(Stock stock)
        {
            stock.Attach(this);
            stocks_.Add(stock);
        }

        public Stock getLastUpdate()
        {
            mut.WaitOne();
            Stock tmp = LastStock_;
            mut.ReleaseMutex();
            return tmp;
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

        public void update(Stock subject)
        {
            mut.WaitOne();
            LastStock_ = subject;
            mut.ReleaseMutex();
            Notify(this);
        }

    }


    /**
     * PortfolioDisplay bruges til at opdatere displayet hver gang der sker en ændring i en kurs i den gældende portfolio.
     * Displayet er lavet til kun at vise et Portfolio ad gangen.
     **/
    class PortfolioDisplay : IObserver_HM<Portfolio>
    {
        private static Mutex mut = new Mutex();

        private string note_;

        private Portfolio portfolio_ = null;

        public Portfolio CurrentPortfolio
        {
            set
            {
                if(portfolio_ != null)
                    portfolio_.Detach(this);

                portfolio_ = value;
                portfolio_.Attach(this);
                update(portfolio_);
            }
            get
            {
                return portfolio_;
            }
        }


        public PortfolioDisplay(string note)
        {
            note_ = note;
        }

        public void update(Portfolio subject)
        {
            if(subject != portfolio_)
            {
                Console.WriteLine("ERROR: Wrong Portfolio trying to use this display");
                subject.Detach(this);
                return;
            }


            mut.WaitOne();
            {
                Console.Clear();
                if(note_ != "")
                {
                    Console.WriteLine("Note: " + note_);
                    Console.WriteLine("-----------------------------");
                }
                
                Console.WriteLine("Portfolio name : " + subject.Name);
                Stock last = subject.getLastUpdate();
                if (last != null)
                {
                    Console.WriteLine("Lastest news : stock name '" + last.Name + "' current value is: " + last.Value);
                }
                Console.WriteLine("------------------- Stock list -----------------------");
                foreach (Stock s in subject.getStocks())
                {
                    Console.WriteLine("stock name '" + s.Name + "' current value is: " + s.Value);
                }
                Console.WriteLine("------------------- Stock list end -------------------");
            }
            mut.ReleaseMutex();
        }

    }

}
