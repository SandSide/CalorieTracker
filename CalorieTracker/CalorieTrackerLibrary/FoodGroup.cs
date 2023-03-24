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

        List<Food> foodList;

        public List<Food> FoodList
        {
            get { return foodList; }
            set { foodList = value; }
        }

        public FoodGroup(string name)
        {
            this.name = name;
            foodList = new List<Food>();
            Count = 1;
        }

        public void AddFood(Food food)
        {
            if (food == null)
                throw new NullReferenceException();
     
            foodList.Add(food);
        }

        public Food RemoveFood(string foodName)
        {
            foreach(Food food in foodList)
            {
                if (food.Name == foodName)
                {
                    foodList.Remove(food);
                    return food;
                }   
            }

            return null;
        }

        public int CalculateTotalCalories()
        {
            int total = 0; ;
            foreach(var food in foodList)
            {
                total += food.TotalCalories;
            }

            return total * Count;
        }

        public void Decrement()
        {
            if(Count > 0)
                Count--;
        }

        public void Increment()
        {
            Count++;
        }
    }
}
