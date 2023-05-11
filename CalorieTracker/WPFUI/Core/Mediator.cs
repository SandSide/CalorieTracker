using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Core
{
    internal class Mediator
    {
        private static readonly Mediator instance = new Mediator();

        public static Mediator Instance
        {
            get { return instance; }
        }

        private Mediator()
        {
        }

        public event EventHandler<EventArgs> MessageReceived;

        public void SendMessage(object sender, EventArgs args)
        {
            MessageReceived?.Invoke(sender, args);
        }
    }
}
