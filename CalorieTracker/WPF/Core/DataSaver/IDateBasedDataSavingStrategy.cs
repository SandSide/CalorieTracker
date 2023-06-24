using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Core.DataSaver
{
    internal interface IDateBasedDataSavingStrategy<T> : IDataSavingStrategy<T>
    {
        public DateTime Date { get; set; }
    }
}
