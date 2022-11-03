﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;
using System.Windows;

namespace ShoeBoxScan.Models.Helpers
{
    public static class ExcelHelper
    {
        private static List<string> _ImportColumns = new List<string>()
        {
            "PO Number", "Total Qty", "UPC", "Label ID", "Customer Size", "Manufaturer Size", "Scan Qty", "User"
        };

        public static DataTable ReadExcel(string filePath)
        {
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                DataTable dt = new DataTable();
                bool firstRow = true;
                List<string> columnList = new List<string>();
                int rowIndex = 0, colIndex = 0;

                foreach (IXLRow row in workSheet.Rows())
                {
                    foreach (IXLCell cell in row.Cells())
                    {
                        if (colIndex < _ImportColumns.Count)
                        {
                            //Mark first row
                            if (firstRow && cell.Value.ToString() == _ImportColumns[colIndex])
                            {
                                dt.Columns.Add(cell.Value.ToString());
                                columnList.Add(cell.Address.ColumnLetter.ToString());
                                colIndex++;
                            }
                            else if (!firstRow && columnList.Count > 0 && cell.Address.ColumnLetter.ToString() == columnList[colIndex] && !String.IsNullOrEmpty(cell.Value.ToString()))
                            {
                                dt.Rows[rowIndex - 1][colIndex] = cell.Value.ToString();
                                colIndex++;
                            }
                        }
                    }
                    dt.Rows.Add();
                    firstRow = false;
                    rowIndex++;
                    colIndex = 0;
                }

                if (columnList.Count != _ImportColumns.Count)
                {
                    MessageBox.Show("Table is invalid");
                }

                return dt;
            }
        }
    }
}
