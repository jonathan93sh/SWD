using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_program
{
    class Program
    {
        static void Main(string[] args)
        {
            var chatroom = new ConcreteMediator<string>();

            var LifeOfBo = new Colleague("Bo", chatroom);
            var LifeOfIb = new Colleague("Ib", chatroom);
            var LifeOfOle = new Colleague("Ole", chatroom);

            LifeOfOle.broadcastMsg("Hey i'm Ole");

            var LifeOfBrian = new Colleague("Brian", chatroom);
            LifeOfOle.broadcastMsg("any one?");

            LifeOfBrian.broadcastMsg("Hello Ole i'm Brian");
            LifeOfBrian.leave();
          

            LifeOfOle.broadcastMsg("Oh Brian leaved :(");

            Console.ReadLine();
        }
    }

    class Colleague : IColleague<string>
    {

        private string _name;
        IMediator<string> _chatroom;
        public string GetName()
        {
            return _name;
        }

        public Colleague(string name, IMediator<string> chatroom)
        {
            _name = new string(name.ToCharArray());
            _chatroom = chatroom;

            _chatroom.register(this);
            Console.WriteLine(_name + ": has enter a chatroom");
        }

        ~Colleague()
        {
            _chatroom.unregister(this);
            Console.WriteLine(_name + ": has leaved the chatroom");
        }

        public void leave()
        {
            _chatroom.unregister(this);
            Console.WriteLine(_name + ": has leaved the chatroom");
        }

        public void receiveMsg(string msg)
        {
            Console.WriteLine(_name + " received this message: " + msg);
        }


        public void broadcastMsg(string msg)
        {
            Console.WriteLine(_name + ": send this message: " + msg);
            _chatroom.broadcastMsg(this, msg);
        }
    }

}
