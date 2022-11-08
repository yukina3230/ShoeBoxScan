using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;
using System.Windows;
using System.Collections.ObjectModel;

namespace ShoeBoxScan.Models.Helpers
{
    public static class ExcelHelper
    {
        private static List<string> _ImportColumns = new List<string>()
        {
            "PO Number", "Total Qty", "UPC", "Customer Size", "User"
        };

        public static ObservableCollection<ImportDataModel> ReadExcel(string filePath)
        {
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                DataTable dataTable = new DataTable();
                bool firstRow = true;
                List<string> columnList = new List<string>();
                int rowIndex = 0, colIndex = 0;

                ObservableCollection<ImportDataModel> dataList = new ObservableCollection<ImportDataModel>();

                foreach (IXLRow row in workSheet.Rows())
                {
                    foreach (IXLCell cell in row.Cells())
                    {
                        if (colIndex < _ImportColumns.Count)
                        {
                            //Mark first row
                            if (firstRow && cell.Value.ToString() == _ImportColumns[colIndex])
                            {
                                dataTable.Columns.Add(cell.Value.ToString());
                                columnList.Add(cell.Address.ColumnLetter.ToString());
                                colIndex++;
                            }
                            else if (!firstRow && columnList.Count > 0 && cell.Address.ColumnLetter.ToString() == columnList[colIndex] && !String.IsNullOrEmpty(cell.Value.ToString()))
                            {
                                dataTable.Rows[rowIndex - 1][colIndex] = cell.Value.ToString();
                                colIndex++;
                            }
                        }
                    }
                    dataTable.Rows.Add();
                    firstRow = false;
                    rowIndex++;
                    colIndex = 0;
                }

                if (columnList.Count != _ImportColumns.Count)
                {
                    MessageBox.Show("Table is invalid");
                }
                else
                {
                    dataTable = dataTable.Rows.Cast<DataRow>()
                        .Where(row => !row.ItemArray
                        .All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string)))
                        .CopyToDataTable();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        dataList.Add(new ImportDataModel
                        {
                            PO_Number = row["PO Number"].ToString(),
                            UPC = row["UPC"].ToString(),
                            Customer_Size = row["Customer Size"].ToString(),
                            Total_Qty = row["Total Qty"].ToString(),
                            User = row["User"].ToString()
                        });
                    }
                }

                return dataList;
            }
        }
    }
}
