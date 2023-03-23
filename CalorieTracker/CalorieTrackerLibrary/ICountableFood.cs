using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerLibrary
{
    public interface ICountableFood: IFood, ICountable
    {
        int TotalCalories { get; set; }

        int CalculateTotalCalories(int calories, int count);
    }
}
