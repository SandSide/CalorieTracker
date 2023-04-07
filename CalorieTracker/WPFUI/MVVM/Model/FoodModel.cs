using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Core;
using CalorieTrackerLibrary;

namespace WPFUI.MVVM.Model
{
    internal class FoodModel : ObservableObject, ICountableFood
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
                OnPropertyChanged();
            }
        }

        public int Calories
        {
            get { return calories; }
            set
            {
                calories = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalCalories");
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
                OnPropertyChanged();
                OnPropertyChanged("TotalCalories");
            }
        }


        public FoodModel(string name, int calories, int count)
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
