using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerLibrary
{
    internal interface Food
    {
        string Name { get; set; }
        int calories { get; set; }

    }
}
