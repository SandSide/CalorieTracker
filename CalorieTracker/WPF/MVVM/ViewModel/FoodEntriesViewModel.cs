using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WPF.Core;
using WPF.MVVM.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF.MVVM.ViewModel
{
    internal class FoodEntriesViewModel : ObservableObject
    {

        private ObservableCollection<FoodItemViewModel> _foodItems;

        public ObservableCollection<FoodItemViewModel> FoodItems
        {
            get { return _foodItems; }
            set
            {
                _foodItems = value;
                OnPropertyChanged("TotalCalories");
                OnPropertyChanged(nameof(CalorieIntakeProgress));
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
            get
            {
                if (FoodItems != null)
                    return FoodItems.Sum(item => item.Calories * item.Count);
                else return 0;
            }
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
            get { return _date.ToString("dd MMM"); }
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


        public int CalorieIntakeProgress
        {
            get { return (int)Math.Round((double)(100 * TotalCalories) / 2000); }
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
        public void OnMessageRecieved(object sender, string msg)
        {
            if (msg == "TotalCalories")
            {
                OnPropertyChanged(nameof(TotalCalories));
                OnPropertyChanged(nameof(CalorieIntakeProgress));
            }
            else if (msg == "RemoveFoodItem")
                RemoveFoodItem(sender);

            Save();
        }


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

            FoodItems.Remove((FoodItemViewModel)foodItem);
            OnPropertyChanged(nameof(FoodItems));
            OnPropertyChanged(nameof(TotalCalories));
            OnPropertyChanged(nameof(CalorieIntakeProgress));
        }


        /// <summary>
        /// Load food entires for todays date
        /// </summary>
        /// <param name="date">
        /// Date of food list to load
        /// </param>
        public void Load(DateTime date)
        {

            Date = date;
            var data = DataLoader.Load("Test.json", date);

            FoodItems = new ObservableCollection<FoodItemViewModel>();

            if (data != null)
            {
                foreach (FoodItemModel food in data)
                {
                    FoodItems.Add(new FoodItemViewModel(food));
                }
            }
            else
                FoodItems = new ObservableCollection<FoodItemViewModel>
                {
                    new FoodItemViewModel(new FoodItemModel())
                };


            OnPropertyChanged(nameof(FoodItems));
            OnPropertyChanged(nameof(TotalCalories));
            OnPropertyChanged(nameof(CalorieIntakeProgress));
        }


        /// <summary>
        /// Save food list to a file
        /// </summary>
        public void Save()
        {
            DataSaver.SaveData("Test.json", FoodItems, _date);
        }

        public RelayCommand AddNewFoodItemCommand { get; set; }
        public RelayCommand LoadNextDayCommand { get; set; }
        public RelayCommand LoadPreviousDayCommand { get; set; }

        public FoodEntriesViewModel()
        {

            Load(DateTime.Today);

            // Add new Food Item Command
            AddNewFoodItemCommand = new RelayCommand(o =>
            {
                FoodItems.Add(new FoodItemViewModel(new FoodItemModel()));
                OnPropertyChanged(nameof(FoodItems));
                OnPropertyChanged(nameof(CalorieIntakeProgress));
                Save();
            });

            // Load Next Day
            LoadNextDayCommand = new RelayCommand(o =>
            {
                Load(_date.AddDays(1));
            });

            // Load Previous day
            LoadPreviousDayCommand = new RelayCommand(o =>
            {
                Load(Date.AddDays(-1));
            });

            Mediator.Instance.MessageReceived += OnMessageRecieved;
        }

    }
}
