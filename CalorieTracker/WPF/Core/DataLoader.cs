using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WPF.MVVM.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static WPF.Core.DataSaver;

namespace WPF.Core
{
    internal static class DataLoader
    {

        public static List<FoodItemModel> Load(string filename) 
        {
            
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = Path.Combine(docPath, filename);

            using StreamReader sr = new(path);
            var json  = sr.ReadToEnd();


            List<DayFoodIntake> dailyIntake = JsonConvert.DeserializeObject<List<DayFoodIntake>>(json);

            foreach(var day in dailyIntake)
            {
                if(day.date.ToString("dd mm yyyy") == DateTime.Today.ToString("dd mm yyyy"))
                    return day.foodItems;
 
            }

            return null;

        }

        public static List<DayFoodIntake> LoadAllEntries(string filename)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = Path.Combine(docPath, filename);

            using StreamReader sr = new(path);
            var json = sr.ReadToEnd();

            List<DayFoodIntake> dailyIntake = JsonConvert.DeserializeObject<List<DayFoodIntake>>(json);

            return null;

        }
    }
}
