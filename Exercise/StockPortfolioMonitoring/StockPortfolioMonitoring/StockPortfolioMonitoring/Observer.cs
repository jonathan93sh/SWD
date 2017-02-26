using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Har lavet vores egen Observer, ikke fordi den der allerede eksistere ikke var god nok, men mere fordi vi var i tvivl om vi måtte bruge den eller om vi selv skulle lave en.
 * Der er lavet en generisk Push Observer klasse, og en subject klasse som også er generisk.
 * Observer klassen bruges til at overvåge et/flere subject, og få nofikationer når der sker en ændring. Efter som det er en push Observer vil det betyde at Observeren vil modatage data fra det subject der sender en nofikation.
 **/
namespace HomeMade.Observer
{
    interface IObserver_HM<T>
    {
        void update(T subject);
    }
    
    class subject_HM<T> 
    {
        List<IObserver_HM<T>> Observers_ = new List<IObserver_HM<T>>();

        public subject_HM()
        {

        }

        public void Attach(IObserver_HM<T> Observer)
        {
            Observers_.Add(Observer);
        }

        public void Detach(IObserver_HM<T> Observer)
        {
            Observers_.Remove(Observer);
        }

        public void Notify(T subject)
        {

            foreach (IObserver_HM<T> o in Observers_)
            {
                o.update(subject);
            }
        }
    }
}
