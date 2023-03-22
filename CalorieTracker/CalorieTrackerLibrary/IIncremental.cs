using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerLibrary
{
    public interface IIncremental
    {
        int Count { get; }

        void Increment();
        void Decrement();

    }
}
