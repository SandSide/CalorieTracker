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
            string path = Path.Combine(docPath, filename);

            List<FoodItemModel> foodModels = foodItems.Select(o => o.FoodItemModel).ToList();
            List<DaysFoodIntake> dailyIntakeEntries = DataLoader.LoadAllEntries(filename);

            bool updated = false;

            // Find food entry for todays date
            foreach (var day in dailyIntakeEntries)
            {
                if (day.Date.ToString("dd mm yyyy") == DateTime.Today.ToString("dd mm yyyy"))
                {
                    day.FoodItems = foodModels;
                    updated = true;
                    break;
                }
            }

            if (!updated)
                dailyIntakeEntries.Add(new DaysFoodIntake(DateTime.Today, foodModels));


            var json = JsonSerializer.Serialize(dailyIntakeEntries);
            File.WriteAllText(path, json);
        }

    }
}
