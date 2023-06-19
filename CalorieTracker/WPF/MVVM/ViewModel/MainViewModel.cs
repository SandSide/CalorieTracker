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

            FoodEntryLoader FoodEntriesLoader = new FoodEntryLoader(DateTime.Today, tempFilePath);

            FoodEntryVM = new FoodEntriesViewModel(1);
			CurrentView = FoodEntryVM;
        }
    }
}
