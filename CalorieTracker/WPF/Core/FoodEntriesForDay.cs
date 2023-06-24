using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;

namespace WPF.Core
{
    internal class FoodEntriesForDay
    {
        public DateTime Date { get; set; }

        public List<FoodItemModel> FoodEntries { get; set; }

        public FoodEntriesForDay(DateTime date, List<FoodItemModel> foodEntries)
        {
            Date = date;
            FoodEntries = foodEntries;
        }
    }
}
