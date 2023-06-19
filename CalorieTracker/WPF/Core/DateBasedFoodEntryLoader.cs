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
    internal class DateBasedFoodEntryLoader : IDataLoader<List<FoodItemModel>>
    {

        public DateTime EntryDate { get; set; }
        public string FilePath { get; set; }

        public DateBasedFoodEntryLoader(DateTime date, string filePath)
        {
            EntryDate = date;
            FilePath = filePath;
        }

        /// <summary>
        /// Loads todays date food entry from file
        /// </summary>
        /// <param name="filename">
        /// Filename which contains list of food entries
        /// </param>
        public List<FoodItemModel>? Load() 
        {

            // Read File
            using StreamReader sr = new(FilePath);
            var json = sr.ReadToEnd();

            // Convert Entires to list
            List<DaysFoodIntake> dailyIntakeEntries = JsonConvert.DeserializeObject<List<DaysFoodIntake>>(json);

            // Find food entry for todays date
            foreach (var day in dailyIntakeEntries)
            {
                if(day.Date.ToString("dd mm yyyy") == EntryDate.Date.ToString("dd mm yyyy"))
                    return day.FoodItems;
 
            }

            return null;
        }

    }
}
