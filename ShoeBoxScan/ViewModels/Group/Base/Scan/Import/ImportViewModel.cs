using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using ShoeBoxScan.Models.Helpers;
using ShoeBoxScan.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShoeBoxScan.ViewModels.Group.Base.Scan.Import
{
    public class ImportViewModel : ObservableObject
    {
        private DataTable _ImportTable;
        public DataTable ImportTable { get => _ImportTable; set { _ImportTable = value; OnPropertyChanged(); } }

        public RelayCommand ImportExcelCommand { get; }
        public RelayCommand<Window> SaveExcelCommand { get; }

        public ImportViewModel()
        {
            ImportTable = new DataTable();

            ImportExcelCommand = new RelayCommand(ImportExcel);
            SaveExcelCommand = new RelayCommand<Window>(o => SaveExcel(o), o => true);
        }

        private void ImportExcel()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            fileDialog.Multiselect = false;

            if ((bool)fileDialog.ShowDialog())
            {
                ImportTable = ExcelHelper.ReadExcel(fileDialog.FileName);
            }
        }

        private void SaveExcel(Window window)
        {
            DataHelper.ExcelTable = ImportTable;
            window.Close();
        }
    }
}
