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

        private ObservableCollection<FoodItemViewModel> _foodItems;

        public ObservableCollection<FoodItemViewModel> FoodItems
        {
            get { return _foodItems; }
            set
            {
                _foodItems = value;
                OnPropertyChanged("TotalCalories");
            }
        }

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

        public void RaisePropertyTotalCaloriesChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("TotalCalories");
            TestString = "TC Raised";
        }

        public RelayCommand AddNewFoodItemCommand { get; set; }

        private string testString;

        public string TestString
        {
            get { return testString; }
            set 
            { 
                testString = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RemoveFoodItemCommand { get; set; }


        public FoodItemsViewModel()
        {
            FoodItems = new ObservableCollection<FoodItemViewModel>
            {
                new FoodItemViewModel(new FoodItemModel("Banana", 90, 1)),
                new FoodItemViewModel(new FoodItemModel("Chocolate", 901, 1)),
                new FoodItemViewModel(new FoodItemModel("Apple", 45, 2))
            };

            Date = DateTime.Today;

            AddNewFoodItemCommand = new RelayCommand(o =>
            {
                FoodItems.Add(new FoodItemViewModel(new FoodItemModel()));
            });


            Mediator.Instance.MessageReceived += RaisePropertyTotalCaloriesChanged;
        }
    }
}
