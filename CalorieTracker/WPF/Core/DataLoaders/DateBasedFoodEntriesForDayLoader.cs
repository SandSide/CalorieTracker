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

namespace WPF.Core
{
    internal class DateBasedFoodEntriesForDayLoader : IDataLoader<FoodEntriesForDay>
    {

        public DateTime EntryDate { get; set; }
        public string FilePath { get; set; }

        public DateBasedFoodEntriesForDayLoader(DateTime date, string filePath)
        {
            EntryDate = date;
            FilePath = filePath;
        }

        /// <summary>
        /// Loads todays date food entry from file
        /// </summary>
        /// <returns> Food Entires for the day</returns>
        public FoodEntriesForDay Load() 
        {

            // Read File
            using StreamReader sr = new(FilePath);
            var json = sr.ReadToEnd();

            // Convert Entires to list
            List<FoodEntriesForDay>? allFoodEntires = JsonConvert.DeserializeObject<List<FoodEntriesForDay>>(json) ?? null;

            // Find food entry for todays date
            foreach (FoodEntriesForDay day in allFoodEntires)
            {
                if(day.Date.ToString("dd mm yyyy") == EntryDate.Date.ToString("dd mm yyyy"))
                    return day;
 
            }

            return null;
        }
    }
}
