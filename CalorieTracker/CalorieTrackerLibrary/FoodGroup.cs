using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerLibrary
{
    public class FoodGroup : ICountable
    {
        public int Count {get; set;}   
        string name;
        int totaCalories;

        List<Food> foodList;

        public FoodGroup(string name)
        {
            this.name = name;
            foodList = new List<Food>();
        }

        public void AddFood(string foodName, int calories)
        {
            foodList.Add(Factory.CreateFoodItem(foodName, calories));
        }

        public Food RemoveFood(string foodName)
        {
            Food output = null;

            foreach(Food food in foodList)
            {
                if (food.Name == foodName)
                {
                    output = food;
                    foodList.Remove(food);
                }   
            }

            return output;
        }

        public void CalculateTotalCalories()
        {
            int total = 0; ;
            foreach(var food in foodList)
            {
                total += food.TotalCalories;
            }
        }

        public void Decrement()
        {
            throw new NotImplementedException();
        }

        public void Increment()
        {
            throw new NotImplementedException();
        }
    }
}
