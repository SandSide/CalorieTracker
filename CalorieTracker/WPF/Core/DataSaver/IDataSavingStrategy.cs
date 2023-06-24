using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Core.DataSaver
{
    internal interface IDataSavingStrategy<T>
    {
        void Save(T data);
    }
}
