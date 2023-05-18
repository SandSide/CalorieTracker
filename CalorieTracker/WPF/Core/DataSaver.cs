using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;

namespace WPF.Core
{
    internal static class DataSaver
    {
        public static void SaveData(string filename, List<FoodItemModel> foodItems)
        {

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            using (StreamWriter writer = new StreamWriter(Path.Combine(docPath, filename)))
            {
                
            }
        }
    }

}
