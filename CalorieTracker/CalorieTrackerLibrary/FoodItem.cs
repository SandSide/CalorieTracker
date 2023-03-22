using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerLibrary
{
    internal class FoodItem : Food
    {
        public string Name { get; set; }
        
        public int calories { get; set; }

        int totalCalories { get; set; }

        int quantity = 1;


        public FoodItem(string name, int calories, int totalCalories)
        {
            Name = name;
            this.calories = calories;
            this.totalCalories = totalCalories;
        }

        public void CalculateTotal() => totalCalories = calories * quantity;

        public void IncrementQuantity() => quantity++;

        public void DecrementQuantity() => quantity--;

    }
}
