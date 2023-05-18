using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Core;
using WPF.MVVM.Model;

namespace WPF.MVVM.ViewModel
{

    /// <summary>
    ///  This class stores and manages todays eaten food itmes. Calcualtes total calories. Allows one to add/remove food items in the list.
    /// </summary>
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

        /// <summary>
        /// Calcualtes and returns the total calories
        /// </summary>
        /// <returns>
        /// Combined total calories of all food items
        /// </returns>
        public int TotalCalories
        {
            get { return FoodItems.Sum(item => item.Calories * item.Count); }
        }

        private DateTime _date;


        /// <summary>
        /// Date in  string format
        /// </summary>
        /// <returns>
        /// Formated date string
        /// </returns>
        public string DateString
        {
            get { return _date.ToString("dd MMM");}
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

        /// <summary>
        /// Handles Messages recieved from Mediator
        /// </summary>
        /// <param name="sender">
        /// Sender of the message
        /// </param>
        /// <param name="msg">
        /// Message recieved
        /// </param>
        public void OnMessageRecieved (object sender, string msg)
        {
            if (msg == "TotalCalories")
                OnPropertyChanged(nameof(TotalCalories));
            else if (msg == "RemoveFoodItem")
                RemoveFoodItem(sender);
        }

  
        public RelayCommand AddNewFoodItemCommand { get; set; }


        /// <summary>
        /// Remove Food Item from the list
        /// </summary>
        /// <param name="foodItem">
        /// Food Item to Delete
        /// </param>
        public void RemoveFoodItem(object foodItem)
        {
            // Check if it contains the food item
            if (!FoodItems.Contains((FoodItemViewModel)foodItem))
                return;

            FoodItems.Remove((FoodItemViewModel) foodItem);
            OnPropertyChanged(nameof(FoodItems));
            OnPropertyChanged(nameof(TotalCalories));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Cancel the closure
            e.Cancel = true;

       /*     DataSaver.SaveData("Test.txt");*/

            e.Cancel = false;
        }

        public FoodItemsViewModel()
        {
            FoodItems = new ObservableCollection<FoodItemViewModel>
            {
                new FoodItemViewModel(new FoodItemModel("Banana", 90, 1)),
                new FoodItemViewModel(new FoodItemModel("Chocolate", 901, 1)),
                new FoodItemViewModel(new FoodItemModel("Apple", 45, 2))
            };

            Date = DateTime.Today;

            // Add new Food Item Command
            AddNewFoodItemCommand = new RelayCommand(o =>
            {
                FoodItems.Add(new FoodItemViewModel(new FoodItemModel()));
                OnPropertyChanged(nameof(FoodItems));
            });

            DataSaver.SaveData("Test.json", FoodItems, _date);


            Mediator.Instance.MessageReceived += OnMessageRecieved;
        }
    }
}
