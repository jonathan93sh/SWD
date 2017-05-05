using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_program
{

    interface IColleague <T>
    {
        void receiveMsg(T msg);
        void sendMsg(IColleague <T> otherColleague,T msg);
        void broadcastMsg(T msg);
    }

    interface IMediator <T>
    {
        void sendMsg(IColleague<T> to, T msg);
        void broadcastMsg(IColleague<T> from, T msg);
        void register(IColleague<T> newColleague);
    }

    class ConreteMediator<T> : IMediator<T>
    {
        private List<IColleague<T>> _colleagues = new List<IColleague<T>>();

        public ConreteMediator()
        {
        }

        public void sendMsg(IColleague<T> to, T msg)
        {
 	        foreach(IColleague<T> c in _colleagues)
            {
                if(c == to)
                {
                    to.receiveMsg(msg);
                    break;
                }
            }
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
    }
}
