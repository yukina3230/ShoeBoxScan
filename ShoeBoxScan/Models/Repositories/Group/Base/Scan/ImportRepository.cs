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
        private string _CurrentKey, _Key;
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
                    GetSerialKey();
                    _Command = new OracleCommand(cmdStr, _Conn);
                    _Command.Parameters.Add("serial_key", _Key);
                    _Command.Parameters.Add("po_id", item.PO_Number);
                    _Command.Parameters.Add("upc", item.UPC);
                    _Command.Parameters.Add("customer_size", item.Customer_Size);
                    _Command.Parameters.Add("total_qty", item.Total_Qty);
                    _Command.Parameters.Add("user_id", item.User);
                    _Command.Parameters.Add("import_date", DateTime.Now.ToString("dd/MM/yyyy"));
                    _Command.Parameters.Add("status", "A");

                    _Command.ExecuteNonQuery();
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

        private void GetSerialKey()
        {
            string cmdStr = FileHelper.GetSQLString("GetLatestID");
            string result = "", fillZero = "";
            int idNumber = 0;

            OracleConnection Conn = new OracleConnection(_ConnStr);
            OracleCommand Command = new OracleCommand(cmdStr, Conn);

            try
            {
                if (_SerialFlag)
                {
                    Conn.Open();
                    Command = new OracleCommand(cmdStr, Conn);
                    result = Command.ExecuteScalar().ToString();
                    _SerialFlag = false;
                }
                else
                {
                    result = _CurrentKey;
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                Conn.Close();
                MessageBox.Show(ex.ToString());
            }

            idNumber = int.Parse(result.Substring(2)) + 1;

            fillZero = String.Format("{0:0000000000000}", idNumber);

            _CurrentKey = String.Concat("IM", fillZero);

            _Key = _CurrentKey;
        }
    }
}
