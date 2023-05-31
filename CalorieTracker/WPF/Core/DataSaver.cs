using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF.MVVM.Model;
using WPF.MVVM.ViewModel;

namespace WPF.Core
{
    internal static class DataSaver
    {
        public static void SaveData(string filename, ObservableCollection<FoodItemViewModel> foodItems)
        {

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            List<FoodItemModel> foodModels = foodItems.Select(o => o.FoodItemModel).ToList();

            string path = Path.Combine(docPath, filename);

            DayFoodIntake temp = new DayFoodIntake(DateTime.Today, foodModels);

            List<DayFoodIntake> list = new List<DayFoodIntake> { temp };

            var json = JsonSerializer.Serialize(list);
            File.WriteAllText(path, json);
        }

        public record class DayFoodIntake(
            DateTime date,
            List<FoodItemModel> foodItems
            );
    }
}
