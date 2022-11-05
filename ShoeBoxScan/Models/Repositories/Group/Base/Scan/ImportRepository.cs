using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Oracle.ManagedDataAccess.Client;
using ShoeBoxScan.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShoeBoxScan.Models.Repositories.Group.Base.Scan
{
    public class ImportRepository
    {
        private string _ConnStr;
        private string _CurrentKey;
        private bool _Result;
        private bool _SerialFlag = true;
        private OracleConnection _Conn;
        private OracleCommand _Command;

        public ImportRepository()
        {
            _ConnStr = DBHelper.OracleConnStr;
            _Conn = new OracleConnection(_ConnStr);
        }

        public bool ImportTable(ObservableCollection<ImportDataModel> dataTable)
        {
            string cmdStr = FileHelper.GetSQLString("ImportExcelData");
            _Result = false;

            try
            {
                _Conn.Open();
                foreach (var item in dataTable)
                {
                    _Command = new OracleCommand(cmdStr, _Conn);
                    _Command.Parameters.Add("serial_key", GetSerialKey());
                    _Command.Parameters.Add("total_qty", item.Total_Qty);
                    _Command.Parameters.Add("po_id", item.PO_Number);
                    _Command.Parameters.Add("upc", item.UPC);
                    _Command.Parameters.Add("customer_size", item.Customer_Size);
                    _Command.Parameters.Add("user_id", item.User);
                    _Command.Parameters.Add("import_date", DateTime.Now.ToString("dd/MM/yyyy"));
                    _Command.Parameters.Add("status", "A");
                    _Result = (_Command.ExecuteNonQuery() > 0) ? true : false;
                }
                _Conn.Close();
                return _Result;
            }
            catch (Exception ex)
            {
                _Conn.Close();
                MessageBox.Show(ex.ToString());
            }

            return _Result;
        }

        private string GetSerialKey()
        {
            string cmdStr = FileHelper.GetSQLString("GetLatestID");
            string result = "", fillZero = "";
            int idNumber = 0;

            try
            {
                if (_SerialFlag)
                {
                    _Command = new OracleCommand(cmdStr, _Conn);
                    result = _Command.ExecuteScalar().ToString();
                    _SerialFlag = false;
                }
                else
                {
                    result = _CurrentKey;
                }
            }
            catch (Exception ex)
            {
                _Conn.Close();
                MessageBox.Show(ex.ToString());
            }

            idNumber = int.Parse(result.Substring(2)) + 1;

            for (int i = 0; i < result.Length - 2 - idNumber.ToString().Length; i++)
            {
                fillZero += "0";
            }

            _CurrentKey = String.Concat("IM", fillZero, idNumber.ToString());

            return _CurrentKey;
        }
    }
}
