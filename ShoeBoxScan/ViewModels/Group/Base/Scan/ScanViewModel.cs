using CommunityToolkit.Mvvm.ComponentModel;
using ShoeBoxScan.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeBoxScan.ViewModels.Group.Base.Scan
{
    public partial class ScanViewModel : ObservableObject
    {
        public ScanViewModel()
        {
            OrderList = DataHelper.GetOrderList(DataHelper.DataTable);
        }
    }
}
