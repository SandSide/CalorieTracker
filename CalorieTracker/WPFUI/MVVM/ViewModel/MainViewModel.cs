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
using WPFUI.MVVM.View;

namespace WPFUI.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
		private static MainViewModel _instance;
		
		public static MainViewModel Instance
		{
			get
			{
				return _instance;
			}
		}


        private object _currentView;

        public static FoodItemsViewModel FoodItemsVM  { get; set; }

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
            _instance = this;

			FoodItemsVM = new FoodItemsViewModel();
			CurrentView = FoodItemsVM;
        }
    }
}
