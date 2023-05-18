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
        public static void SaveData(string filename, ObservableCollection<FoodItemViewModel> foodItems, DateTime date)
        {

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            List<FoodItemModel> foodModels = foodItems.Select(o => o.FoodItemModel).ToList();

            string path = Path.Combine(docPath, filename);

/*            if (!File.Exists(path))
            {*/

                using(StreamWriter sw = File.AppendText(path))
                {
                    var json = JsonSerializer.Serialize(new { date, foodModels });
                    sw.WriteLine(json);
                }

/*            }*/

        }
    }

}
