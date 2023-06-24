using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;
using System.IO;

namespace WPF.Core.Data_Saver
{
    internal class DateBasedFoodEntriesForDaySavingStrategy : IDateBasedDataSavingStrategy<FoodEntriesForDay>
    {
        public DateTime Date { get; set; }

        public void Save(FoodEntriesForDay data, string filePath)
        {

           // List<FoodEntriesForDay> allFoodEntriesForDay = DataLoader.LoadAllEntries(filePath);

            bool updated = false;
/*
            // Find food entry for todays date
            foreach (var day in dailyIntakeEntries)
            {
                if (day.Date.ToString("dd mm yyyy") == data.Date.ToString("dd mm yyyy"))
                {
                    day.FoodEntries = data.FoodEntries;
                    updated = true;
                    break;
                }
            }

            if (!updated)
                dailyIntakeEntries.Add(new DaysFoodIntake(date, foodModels));


            var json = JsonSerializer.Serialize(dailyIntakeEntries);
            File.WriteAllText(path, json);*/
        }
    }
}
