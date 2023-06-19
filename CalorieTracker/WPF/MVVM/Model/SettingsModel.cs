using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model
{
    internal class SettingsModel
    {
        public int dailyCalorieGoal;
        public string? filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.json");

        public int DailyCalorieGoal
        {
            get { return dailyCalorieGoal; }
            set { dailyCalorieGoal = value; }
        }

        public string? FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
    }
}
