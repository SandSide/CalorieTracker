using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFUI.Core;
using WPFUI.MVVM.Model;

namespace WPFUI.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public HomeViewModel HomeVM { get; set; }
		
        private object _currentView;

        public ObservableCollection<FoodModel> foods
        {
            get;
            set;
        }

        public RelayCommand IncrementCommand { get; set; }



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
			HomeVM = new HomeViewModel();
			CurrentView = HomeVM;

            foods = new ObservableCollection<FoodModel>()
            {
                new FoodModel("Orange", 51, 1),
                new FoodModel("Apple", 51, 4),
                new FoodModel("Soda", 51, 1),
                new FoodModel("Crisps", 51, 1)
            };

            IncrementCommand = new RelayCommand(o =>
            {
                FoodModel temp = (FoodModel)o;
                temp.Increment();
            });
        }
    }
}
