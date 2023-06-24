using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;
using System.IO;
using System.Text.Json;
using System.Windows.Shapes;

namespace WPF.Core.DataSaver
{
    /// <summary>
    /// This is a class which saves food entires based on the date
    /// </summary>
    internal class DateBasedFoodEntriesForDaySavingStrategy : IDateBasedDataSavingStrategy<FoodEntriesForDay>
    {
        public DateTime Date { get; set; }
        public string FilePath { get; set; }

        public DateBasedFoodEntriesForDaySavingStrategy(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Saves food entires for the day to a file
        /// </summary>
        /// <param name="data">
        /// Data which contains the date and list of food items to save
        /// </param>
        public void Save(FoodEntriesForDay data)
        {

            string json;

            // Read File
            using (StreamReader sr = new(FilePath))
            {
                json = sr.ReadToEnd();
            }

            // Convert Entires to list
            List<FoodEntriesForDay> allFoodEntires = JsonSerializer.Deserialize<List<FoodEntriesForDay>>(json);

            bool newEntry = true;

            // Find food entry for todays date
            foreach (FoodEntriesForDay day in allFoodEntires)
            {
                if (day.Date.ToString("dd mm yyyy") == data.Date.ToString("dd mm yyyy"))
                {
                    day.FoodEntries = data.FoodEntries;
                    newEntry = false;
                    break;
                }
            }

            if(newEntry)
                allFoodEntires.Add(data);

            // Sort List
            allFoodEntires = allFoodEntires.OrderBy(entires => entires.Date).ToList();

            json = JsonSerializer.Serialize(allFoodEntires);

            File.WriteAllText(FilePath, json);
        }
    }
}
