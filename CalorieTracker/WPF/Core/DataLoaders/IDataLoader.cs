using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Core
{
    internal interface IDataLoader<T>
    {
        T Load();
    }
}
