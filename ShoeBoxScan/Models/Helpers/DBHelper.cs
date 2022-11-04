using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeBoxScan.Models.Helpers
{
    public static class DBHelper
    {
        public static string OracleConnStr = @"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.1.1.44)(PORT=1521)))(CONNECT_DATA=(SID = SPGVN))); PERSIST SECURITY INFO=True;USER ID=at00;PASSWORD=12345678";
    }
}
