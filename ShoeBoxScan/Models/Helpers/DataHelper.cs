using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeBoxScan.Models.Helpers
{
    public static class DataHelper
    {
        public static string UserId;

        public static string Line;

        public static ObservableCollection<ImportDataModel> DataTable;

        public static ObservableCollection<string> OrderList;

        public static ObservableCollection<string> GetOrderList(ObservableCollection<ImportDataModel> dataTable)
        {
            string currentItem = "";
            ObservableCollection<string> list = new ObservableCollection<string>();

            if (dataTable != null)
            {
                foreach (var item in dataTable)
                {
                    if (currentItem != item.PO_Number)
                    {
                        currentItem = item.PO_Number;
                        list.Add(currentItem);
                    }
                }
            }

            return list;
        }
    }
}
