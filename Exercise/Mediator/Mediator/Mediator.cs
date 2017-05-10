using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    /**
     * Et interface der kan bruges af et objekt der ønsker at tilslutte til en mediator.
     **/
    interface IColleague <T>
    {
        // Denne metode bliver kaldt af mediatoren hver gang der kommer en nu besked.
        // param T msg: er den besked mediatoren omdeler.
        void receiveMsg(T msg);
        // Denne metode kan bruges til at sende en besked rundt til alle der subscriber til en bestemt mediator.
        // param T msg: beskeden der skal sendes til de andre objekter.
        void broadcastMsg(T msg);
    }

    /**
     * Dette er interfacet der kan bruges af en mediator. 
     * En mediator gør det muligt at for Colleague at sende beskeder rundt uden at de skal have kendskab til hinanden.
     **/
    interface IMediator <T>
    {
        // Denne metode bliver kaldt når der skal sendes en ny besked rundt.
        // param from : giver mulighed for angive en afsender, så afsenderen ikke modtager sin egen besked. Kan også skrive null istedet og alle vil modtage beskeden. Eller hvis der er server besked der skal sendes rundt til alle Colleagues.
        // param msg : Den besked der skal sendes rundt.
        void broadcastMsg(IColleague<T> from, T msg);
        // Denne metode kan bruges til at registere et nyt objekt til en mediator.
        // param newColleague : det objekt der ønsker at modtage beskeder fra mediatoren.
        void register(IColleague<T> newColleague);
        // Denne metode bruges til at fjerne et objekt fra mediatoren.
        // param Colleague : det objekt der skal fjernes.
        void unregister(IColleague<T> Colleague);
    }

    /**
     * En normal mediator klasse.
     **/
    class ConcreteMediator<T> : IMediator<T>
    {
        private List<IColleague<T>> _colleagues = new List<IColleague<T>>();

        public ConcreteMediator()
        {
        }

        public void broadcastMsg(IColleague<T> from, T msg)
        {
 	        foreach(IColleague<T> c in _colleagues)
            {
                if(c != from || from == null)
                {
                    c.receiveMsg(msg);
                }
            }
        }

        public void register(IColleague<T> newColleague)
        {
 	        _colleagues.Add(newColleague);
        }


        public void unregister(IColleague<T> Colleague)
        {
            _colleagues.Remove(Colleague);
        }
    }
    /**
     * En tråde beskyttet mediator.
     **/
    class ConcreteMediator_ThreadSafe<T> : IMediator<T>
    {
        private List<IColleague<T>> _colleagues = new List<IColleague<T>>();
        private object _lockObj = new object();
        public ConcreteMediator_ThreadSafe()
        {
        }

        public void broadcastMsg(IColleague<T> from, T msg)
        {
            lock (_lockObj)
            {
                foreach (IColleague<T> c in _colleagues)
                {
                    if (c != from || from == null)
                    {
                        c.receiveMsg(msg);
                    }
                }
            }
        }

        public void register(IColleague<T> newColleague)
        {
            lock (_lockObj)
            {
                _colleagues.Add(newColleague);
            }
            
        }


        public void unregister(IColleague<T> Colleague)
        {
            lock (_lockObj)
            {
                _colleagues.Remove(Colleague);
            }
        }
    }
}
