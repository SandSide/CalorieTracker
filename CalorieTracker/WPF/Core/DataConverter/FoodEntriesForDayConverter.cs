using CalorieTrackerLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;
using WPF.MVVM.ViewModel;

namespace WPF.Core.DataConverter
{
    internal class FoodEntriesForDayConverter
    {

        public List<FoodItemViewModel> ConvertToFoodItemViewModelCollection(FoodEntriesForDay data)
        {
            return data.FoodEntries.Select(food => new FoodItemViewModel(food)).ToList();
        }

        public FoodEntriesForDay ConvertToFoodEntriesForDay(List<FoodItemViewModel> data, DateTime date)
        {

            List<FoodItemModel> foodEntries = data.Select(food => food.FoodItemModel).ToList();

            return new FoodEntriesForDay(date, foodEntries);
        }
    }
}
