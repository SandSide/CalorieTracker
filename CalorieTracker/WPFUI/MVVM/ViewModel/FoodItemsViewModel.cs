using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFUI.Core;
using WPFUI.MVVM.Model;

namespace WPFUI.MVVM.ViewModel
{
    internal class FoodItemsViewModel : ObservableObject
    {

        private ObservableCollection<FoodItemModel> _foodItems;

        public ObservableCollection<FoodItemModel> FoodItems
        {
            get { return _foodItems; }
            set
            {
                _foodItems = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalCalories");

            }
        }

        private int _totalCalories;

        public int TotalCalories
        {
            get { return FoodItems.Sum(item => item.Calories * item.Count); }
        }

        private DateTime _date;

        public string DateString
        {
            get { return _date.ToString("dd-MM-yy");}
        }

        public DateTime Date
        {
            get { return _date; }
            set 
            { 
                _date = value;
                OnPropertyChanged("DateString");
            }
        }


        public FoodItemsViewModel()
        {
            FoodItems = new ObservableCollection<FoodItemModel>
            {
                new FoodItemModel("Banana", 90, 1),
                new FoodItemModel("Chocolate", 143, 1),
                new FoodItemModel("Crisps", 130, 1),
                new FoodItemModel("Apple", 45, 1)
            };

            Date = DateTime.Today;
        }

        public int CalculateTotalCalories()
        {

            int total = 0;

            foreach(var food in FoodItems)
            {
                total += food.TotalCalories;
            }

            return total;
        }
    }
}
