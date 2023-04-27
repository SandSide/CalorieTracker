using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerLibrary
{
    public static class Factory
    {

        public static Food CreateFoodItem(string name, int calories)
        {
            return new Food(name, calories);
        }

    }
}
