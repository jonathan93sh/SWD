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
            var chatroom = new ConreteMediator<string>();

            var LifeOfBo = new Colleague("Bo", chatroom);
            var LifeOfIb = new Colleague("Ib", chatroom);
            var LifeOfOle = new Colleague("Ole", chatroom);
            var LifeOfBrian = new Colleague("Brian", chatroom);

            LifeOfOle.broadcastMsg("Hey i'm Ole");

            LifeOfBrian.sendMsg(LifeOfOle, "Hello Ole i'm Brian");

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
        }

        public void receiveMsg(string msg)
        {
            Console.WriteLine(_name + " modtog beskeden: " + msg);
        }

        public void sendMsg(IColleague<string> otherColleague, string msg)
        {
            _chatroom.sendMsg(otherColleague, msg);
        }

        public void broadcastMsg(string msg)
        {
            _chatroom.broadcastMsg(this, msg);
        }
    }

}
