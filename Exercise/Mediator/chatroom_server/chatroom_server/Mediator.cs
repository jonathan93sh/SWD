using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{

    interface IColleague <T>
    {
        void receiveMsg(T msg);
        void broadcastMsg(T msg);
    }

    interface IMediator <T>
    {
        void broadcastMsg(IColleague<T> from, T msg);
        void register(IColleague<T> newColleague);
        void unregister(IColleague<T> Colleague);
    }

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
