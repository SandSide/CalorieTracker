using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using WPF.Core;
using WPF.MVVM.Model;
using WPF.MVVM.View;
using WPF.Core.DataSaver;

namespace WPF.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        private object _currentView;
		private DateTime _currentDate;

		private SettingsModel _settingsModel;

        public static FoodEntriesViewModel? FoodEntryVM  { get; set; }

        public object CurrentView
		{
			get { return _currentView; }
			set 
			{ 
				_currentView = value;
				OnPropertyChanged();
			}
		}


        public MainViewModel() 
		{
			string tempFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.json");

            var foodEntriesLoader = new DateBasedFoodEntriesForDayLoader(DateTime.Today, tempFilePath);
			var foodEntriesSavingStrategy = new DateBasedFoodEntriesForDaySavingStrategy(tempFilePath);

            FoodEntryVM = new FoodEntriesViewModel(foodEntriesLoader, foodEntriesSavingStrategy);
			CurrentView = FoodEntryVM;
        }
    }
}
