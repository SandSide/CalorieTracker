using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFUI.Core;

namespace WPFUI.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public RelayCommand ChangeStringCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
		

        private object _currentView;

		public object CurrentView
		{
			get { return _currentView; }
			set 
			{ 
				_currentView = value;
				OnPropertyChanged();
			}
		}

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

        public MainViewModel() 
		{ 
			HomeVM = new HomeViewModel();
			CurrentView = HomeVM;

            TestString = "Hello World!";

			ChangeStringCommand = new RelayCommand(o => 
			{
				TestString = "Die";
            });
        }
    }
}
