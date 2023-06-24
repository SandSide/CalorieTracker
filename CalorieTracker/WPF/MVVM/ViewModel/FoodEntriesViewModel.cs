﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using WPF.Core;
using WPF.Core.DataConverter;
using WPF.MVVM.Model;
using System.Collections.Specialized;
using System.ComponentModel;
using WPF.Core.DataSaver;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

namespace WPF.MVVM.ViewModel
{
    internal class FoodEntriesViewModel : ObservableObject
    {

        private IDataLoader<FoodEntriesForDay> _loader;
        private IDataSavingStrategy<FoodEntriesForDay> _saver;
        private FoodEntriesForDayConverter _converter;

        private ObservableCollection<FoodItemViewModel> _foodItems;
        private DateTime _date;

        public ObservableCollection<FoodItemViewModel> FoodItems
        {
            get { return _foodItems; }
            set
            {
                if(_foodItems != value )
                {
                    _foodItems = value;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalCalories));
                    OnPropertyChanged(nameof(CalorieIntakeProgress));
                    OnPropertyChanged(nameof(CalorieIntakeProgressColour));
                }
            }
        }

        /// <summary>
        /// Calcualtes and returns the total calories
        /// </summary>
        /// <returns>
        /// Combined total calories of all food items
        /// </returns>
        public int TotalCalories => FoodItems?.Sum(item => item.Calories * item.Count) ?? 0;


        /// <summary>
        /// Date in  string format
        /// </summary>
        /// <returns>
        /// Formated date string
        /// </returns>
        public string DateString => _date.ToString("dd MMM");

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(DateString));
            }
        }


        /// <summary>
        /// Get Calorie Intake as a percentage based on your daily max food intake
        /// </summary>
        /// <returns>
        /// Total Calories consumed as a percerntage to your maximum daily food intake
        /// </returns>
        public int CalorieIntakeProgress => (int)Math.Round((double)(100 * TotalCalories) / 2000);


        private static readonly BrushConverter brushConverter = new BrushConverter();
        public System.Windows.Media.Brush CalorieIntakeProgressColour => CalorieIntakeProgress switch
        {
            > 90 => (Brush)brushConverter.ConvertFrom("#e84118") ?? Brushes.Red,
            > 75 => (Brush)brushConverter.ConvertFrom("#e1b12c") ?? Brushes.Yellow,
            >= 0 => (Brush)brushConverter.ConvertFrom("#4cd137") ?? Brushes.Green,
            _ => (Brush)brushConverter.ConvertFrom("#e84118") ?? Brushes.Red,
        };


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
                OnPropertyChanged(nameof(FoodItems));
                OnPropertyChanged(nameof(TotalCalories));
                OnPropertyChanged(nameof(CalorieIntakeProgress));
                OnPropertyChanged(nameof(CalorieIntakeProgressColour));
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
            if(foodItem is FoodItemViewModel item)
            {
                FoodItems.Remove(item);
                OnPropertyChanged(nameof(FoodItems));
                OnPropertyChanged(nameof(TotalCalories));
                OnPropertyChanged(nameof(CalorieIntakeProgress));
                OnPropertyChanged(nameof(CalorieIntakeProgressColour));
            }
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

            if (_loader is DateBasedFoodEntriesForDayLoader dateBasedLoader)
            {
                dateBasedLoader.EntryDate = Date;
            }

            FoodEntriesForDay? data = _loader.Load();

            if (data != null)
            {
                FoodEntriesForDayConverter _converter = new FoodEntriesForDayConverter();
                var t = _converter.ConvertToFoodItemViewModelCollection(data);
                FoodItems = new ObservableCollection<FoodItemViewModel>(_converter.ConvertToFoodItemViewModelCollection(data));
            }
            else
            {
                FoodItems = new ObservableCollection<FoodItemViewModel>()
                {
                    new FoodItemViewModel(new FoodItemModel())
                };
            }

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
            FoodEntriesForDay data = _converter.ConvertToFoodEntriesForDay(FoodItems.ToList(), Date);
            _saver.Save(data);
        }

        public RelayCommand AddNewFoodItemCommand { get; set; }
        public RelayCommand LoadNextDayCommand { get; set; }
        public RelayCommand LoadPreviousDayCommand { get; set; }

        public FoodEntriesViewModel(IDataLoader<FoodEntriesForDay> loader, IDataSavingStrategy<FoodEntriesForDay> saver)
        {

            _loader = loader;
            _saver = saver;

            _converter = new FoodEntriesForDayConverter();


            Load(DateTime.Today);


            // Add new Food Item Command
            AddNewFoodItemCommand = new RelayCommand(o =>
            {
                FoodItems.Add(new FoodItemViewModel(new FoodItemModel()));
                OnPropertyChanged(nameof(FoodItems));
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
