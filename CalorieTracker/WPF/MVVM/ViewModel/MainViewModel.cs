using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Core;
using WPF.MVVM.Model;
using WPF.MVVM.View;

namespace WPF.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        private object _currentView;
		private DateTime _currentDate;

        public RelayCommand NextDayCommand { get; set; }
		public RelayCommand PreviousDayCommand { get; set; }

        public static FoodItemsViewModel? FoodItemsVM  { get; set; }

        public object CurrentView
		{
			get { return _currentView; }
			set 
			{ 
				_currentView = value;
				OnPropertyChanged();
			}
		}

        private int myVar;

        public String Test
        {
            get { return _currentDate.ToString(); }
        }


        public MainViewModel() 
		{
            _currentDate = DateTime.Today;

            FoodItemsVM = new FoodItemsViewModel();
			CurrentView = FoodItemsVM;

            // Add new Food Item Command
            NextDayCommand = new RelayCommand(o =>
            {
                _currentDate = _currentDate.AddDays(1);
                Mediator.Instance.SendMessage(this, "LoadNextDayEntires");

            });

            PreviousDayCommand = new RelayCommand(o =>
            {
                Mediator.Instance.SendMessage(this, "LoadPreviousDayEntires");
            });
        }
    }
}
