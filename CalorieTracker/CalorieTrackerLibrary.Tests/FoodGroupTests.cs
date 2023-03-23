using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CalorieTrackerLibrary;

namespace CalorieTrackerLibrary.Tests
{
    public class FoodGroupTests
    {

        [Theory]
        [InlineData("Orange", 45, 1)]
        [InlineData("Sausage", 45, 3)]
        public void IncrementQuantity_ShouldIncrement(string name, int calories, int quantity)
        {

            Food test = new Food(name, calories, quantity);
            test.Increment();

            // expected 
            int expected = quantity + 1;


            // Actual
            int actual = test.Count;


            // Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("Orange", 45, 5)]
        [InlineData("Sausage", 45, 10)]
        public void IncrementQuantity_ShouldDecrement(string name, int calories, int quantity)
        {

            Food test = new Food(name, calories, quantity);
            test.Decrement() ;

            // expected 
            int expected = quantity - 1;


            // Actual
            int actual = test.Count;


            // Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("Orange", 45, 1)]
        public void IncrementQuantity_ShouldNotDecrement(string name, int calories, int quantity)
        {

            Food test = new Food(name, calories, quantity);
            test.Decrement();

            // expected 
            int expected = quantity;


            // Actual
            int actual = test.Count;


            // Assert
            Assert.Equal(expected, actual);

        }


        [Theory]
        [InlineData(45, 1)]
        [InlineData(45, 3)]
        [InlineData(65, 0)]
        public void CalculateTotalCalories_ShouldPass( int calories, int quantity)
        {


            // expected 
            int expected = calories * quantity;


            // Actual
            int actual = new Food().CalculateTotalCalories(calories, quantity);


            // Assert
            Assert.Equal(expected, actual);




        }
    }
}
