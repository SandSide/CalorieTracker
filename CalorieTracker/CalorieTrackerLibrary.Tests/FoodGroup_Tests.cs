using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CalorieTrackerLibrary;

namespace CalorieTrackerLibrary.Tests
{
    public class FoodGroup_Tests
    {

        [Fact]
        public void Increment_ShouldIncrement()
        {

            FoodGroup temp = new FoodGroup("Breakfast");
            temp.Increment();

    
            // expected 
            int expected = 1;


            // Actual
            int actual = temp.Count;


            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Decrement_ShouldDecrement()
        {

            FoodGroup temp = new FoodGroup("Breakfast");
            temp.AddFood(new Food("Apple", 45));
            temp.Decrement();

            // expected 
            int expected = 0;


            // Actual
            int actual = temp.Count;


            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AddFood_ShouldWork()
        {
            FoodGroup temp = new FoodGroup("Breakfast");
            temp.AddFood(new Food("Apple", 34));
            temp.AddFood(new Food("Apple", 34));

            // expected 
            int expected = 2;


            // Actual
            int actual = temp.FoodList.Count;


            // Assert
            Assert.Equal(expected, actual);

        }


        [Fact]
        public void RemoveFood_ShouldWork()
        {
            FoodGroup temp = new FoodGroup("Breakfast");
            temp.AddFood(new Food("Apple", 34));
            temp.AddFood(new Food("Sausage", 34));

            temp.RemoveFood("Sausage");

            // expected 
            int expected = 1;


            // Actual
            int actual = temp.FoodList.Count;


            // Assert
            Assert.Equal(expected, actual);
   
        }
    }
}
