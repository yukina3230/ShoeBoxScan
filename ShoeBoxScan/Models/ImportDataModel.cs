using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeBoxScan.Models
{
    public class ImportDataModel
    {
        public string PO_Number { get; set; }

        public string UPC { get; set; }

        public string Customer_Size { get; set; }

        public string Total_Qty { get; set; }

        public string User { get; set; }

        public bool IsChecked { get; set; }
    }
}
