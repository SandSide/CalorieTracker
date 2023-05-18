using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Core;
using CalorieTrackerLibrary;

namespace WPF.MVVM.Model
{
    internal class FoodItemModel : ICountableFood
    {

        public string name;
        public int calories;
        public int count;

        public string Name
        {
            get { return name; }
            set
            {
                  name = value;
            }
        }

        public int Calories
        {
            get { return calories; }
            set
            {
                calories = value;
            }
        }

        public int TotalCalories
        {
            get { return Calories * Count; }
        }

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
            }
        }

        public FoodItemModel()
        {
            Name = string.Empty;
            Calories = 0;
            Count = 0; 
        }

        public FoodItemModel(string name, int calories, int count)
        {
            Name = name;
            Calories = calories;
            Count = 1;
        }

        public void Increment()
        {
            Count++;
        }

        public void Decrement()
        {
            if(Count > 0)
                Count++;
        }
    }
}
