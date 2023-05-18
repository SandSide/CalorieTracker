using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Core
{
    internal class Mediator : IMediator
    {
        private static readonly Mediator instance = new Mediator();

        public static Mediator Instance
        {
            get { return instance; }
        }

        private Mediator()
        {
        }

        public event EventHandler<string> MessageReceived;

        public void SendMessage(object sender, string message)
        {
            MessageReceived?.Invoke(sender, message);
        }
    }
}
