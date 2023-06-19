using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using WPF.Core;
using WPF.MVVM.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Documents;

namespace WPF.MVVM.ViewModel
{
    internal class FoodEntriesViewModel : ObservableObject
    {

        private ObservableCollection<FoodItemViewModel> _foodItems;
        private DateTime _date;
        private IDataLoader<List<FoodItemModel>> _loader;

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



        /// <summary>
        /// Get Calorie Intake as a percentage based on your daily max food intake
        /// </summary>
        /// <returns>
        /// Total Calories consumed as a percerntage to your maximum daily food intake
        /// </returns>
        public int CalorieIntakeProgress
        {
            get { return (int)Math.Round((double)(100 * TotalCalories) / 2000); }
        }


        public System.Windows.Media.Brush CalorieIntakeProgressColour
        {
            get 
            {
                BrushConverter brushConverter = new BrushConverter();

                return CalorieIntakeProgress switch
                {
                    > 90 => (Brush)brushConverter.ConvertFrom("#e84118") ?? Brushes.Red,
                    > 75 => (Brush)brushConverter.ConvertFrom("#e1b12c") ?? Brushes.Yellow,
                    >= 0 => (Brush)brushConverter.ConvertFrom("#4cd137") ?? Brushes.Green,
                    _ => (Brush)brushConverter.ConvertFrom("#e84118") ?? Brushes.Red,
                };
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
        public void OnMessageRecieved(object sender, string msg)
        {
            if (msg == "TotalCalories")
            {
                OnPropertyChanged(nameof(TotalCalories));
                OnPropertyChanged(nameof(CalorieIntakeProgress));
                OnPropertyChanged(nameof(CalorieIntakeProgressColour));
            }
            else if (msg == "RemoveFoodItem")
                RemoveFoodItem(sender);

           // Save();
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
            OnPropertyChanged(nameof(CalorieIntakeProgressColour));
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

            if (_loader is DateBasedFoodEntryLoader dateBasedLoader)
            {
                dateBasedLoader.EntryDate = Date; // Change the stored date in the loader
            }

            var data = _loader.Load();

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
            OnPropertyChanged(nameof(CalorieIntakeProgressColour));
        }


        /// <summary>
        /// Save food list to a file
        /// </summary>
        public void Save()
        {
            //DataSaver.SaveData("Test.json", FoodItems, _date);
        }

        public RelayCommand AddNewFoodItemCommand { get; set; }
        public RelayCommand LoadNextDayCommand { get; set; }
        public RelayCommand LoadPreviousDayCommand { get; set; }

        public FoodEntriesViewModel(IDataLoader<List<FoodItemModel>> loader)
        {

            _loader = loader;

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
