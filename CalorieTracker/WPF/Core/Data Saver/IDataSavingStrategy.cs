using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Core.Data_Saver
{
    internal interface IDataSavingStrategy<T>
    {
        void Save(T data, string filePath);
    }
}
