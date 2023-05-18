using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Core
{
    internal interface IMediator
    {
        void SendMessage(object sender, string message);
    }
}
