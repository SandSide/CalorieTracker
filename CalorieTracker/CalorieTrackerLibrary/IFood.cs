using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerLibrary
{
    public interface IFood
    {
        string Name { get; set; }
        int Calories { get; set; }

    }
}
