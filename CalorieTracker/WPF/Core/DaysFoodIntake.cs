using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;

namespace WPF.Core
{
    internal class DaysFoodIntake
    {

        public DateTime Date { get; set; }
        public List<FoodItemModel> FoodItems { get; set; }

        public DaysFoodIntake(DateTime date, List<FoodItemModel> foodItems)
        {
            Date = date;
            FoodItems = foodItems;
        }
    }
}
