using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeBoxScan.ViewModels.Group.Base.Scan
{
    public partial class ScanViewModel
    {
        private ObservableCollection<string> _OrderList;
        public ObservableCollection<string> OrderList { get => _OrderList; set { _OrderList = value; OnPropertyChanged(); } }
    }
}
