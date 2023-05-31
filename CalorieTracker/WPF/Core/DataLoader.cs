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

        /// <summary>
        /// Loads todays date food entry from file
        /// </summary>
        /// <param name="filename">
        /// Filename which contains list of food entries
        /// </param>
        public static List<FoodItemModel> Load(string filename) 
        {
            
            List<DayFoodIntake> dailyIntakeEntries = LoadAllEntries(filename);

            // Find food entry for todays date
            foreach (var day in dailyIntakeEntries)
            {
                if(day.date.ToString("dd mm yyyy") == DateTime.Today.ToString("dd mm yyyy"))
                    return day.foodItems;
 
            }

            return null;
        }

        /// <summary>
        /// Loads all entires of daily food intake from file
        /// </summary>
        /// <param name="filename">
        /// Filename which contains list of food entries
        /// </param>
        /// <returns>
        /// List of FoodIntake for each day
        /// </returns>
        public static List<DayFoodIntake> LoadAllEntries(string filename)
        {
            // Get filepath
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = Path.Combine(docPath, filename);

            // Read File
            using StreamReader sr = new(path);
            var json = sr.ReadToEnd();

            // Convert Entires to list
            List<DayFoodIntake> dailyIntake = JsonConvert.DeserializeObject<List<DayFoodIntake>>(json);

            return dailyIntake;
        }
    }
}
