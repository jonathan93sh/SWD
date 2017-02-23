using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
