using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerLibrary
{
    public class Food : ICountableFood
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Count { get; set; }
        public int TotalCalories { get; set; }

        public Food()
        {

        }

        public Food(string name, int calories)
        {
            Name = name;
            Calories = calories;
            Count = 1;
        }

        public Food(string name, int calories, int count)
        {
            Name = name;
            Calories = calories;
            Count = count;

            TotalCalories = CalculateTotalCalories(calories, count);
        }

        public void Increment()
        {
            Count++;
            TotalCalories = CalculateTotalCalories(Calories, Count);
        }

        public void Decrement()
        {
            if(Count > 1)
            {
                Count--;
                TotalCalories = CalculateTotalCalories(Calories, Count);
            }
        }

        public int CalculateTotalCalories(int calories, int count) => calories * count;
    }
}
