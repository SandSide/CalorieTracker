using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Core;
using WPFUI.MVVM.Model;

namespace WPFUI.MVVM.ViewModel
{
    internal class FoodItemViewModel : ObservableObject
    {

        private FoodItemModel _foodItemModel;

        public string Name 
        {
            get { return _foodItemModel.Name; }
            set 
            {
                _foodItemModel.Name = value;
                OnPropertyChanged();
            }
        }

        public int Calories 
        { 
            get { return  (int)_foodItemModel.Calories; }
            set
            {
                _foodItemModel.Calories = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalCalories");
                Mediator.Instance.SendMessage(this, "TotalCalories");
            }
        }

        public int Count 
        { 
            get { return (int) _foodItemModel.Count; }
            set 
            { 
                _foodItemModel.count = value; 
                OnPropertyChanged();
                OnPropertyChanged("TotalCalories");
                Mediator.Instance.SendMessage(this, "TotalCalories");
            }
        }

        public int TotalCalories 
        { 
            get { return Calories * Count; }
        }

        public RelayCommand RemoveFoodItemCommand { get; set; }

        public FoodItemViewModel(FoodItemModel food)
        {
            _foodItemModel = food;
            OnPropertyChanged();

            // Remove this FoodItem from list command
            RemoveFoodItemCommand = new RelayCommand(o =>
            {
                // Send message to remove this food item
                Mediator.Instance.SendMessage(this, "RemoveFoodItem");
            });
        }
    }
}
