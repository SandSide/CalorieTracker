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
            }
        }

        public int TotalCalories
        {
            get { return FoodItems.Sum(item => item.Calories * item.Count); }
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
