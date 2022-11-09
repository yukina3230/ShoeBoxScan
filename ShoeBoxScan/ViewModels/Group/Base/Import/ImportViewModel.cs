using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using ShoeBoxScan.Models;
using ShoeBoxScan.Models.Helpers;
using ShoeBoxScan.Models.Services.Group.Base.Scan;
using ShoeBoxScan.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ShoeBoxScan.ViewModels.Group.Base.Scan.Import
{
    public class ImportViewModel : ObservableObject
    {
        private string _FilterString;
        public string FilterString { get => _FilterString; set { _FilterString = value; OnPropertyChanged(); FilterData(); } }

        private ICollectionView _ImportTableView;
        public ICollectionView ImportTableView { get => _ImportTableView; private set { _ImportTableView = value; OnPropertyChanged(); } }

        private ObservableCollection<ImportDataModel> _ImportTable;
        public ObservableCollection<ImportDataModel> ImportTable { get => _ImportTable; set { _ImportTable = value; OnPropertyChanged(); } }

        public RelayCommand LoadExcelCommand { get; }
        public RelayCommand<Window> ImportDataCommand { get; }

        public RelayCommand DeleteRowCommand { get; }

        private ImportService _ImportService;

        public ImportViewModel()
        {
            ImportTable = new ObservableCollection<ImportDataModel>();
            ImportTableView = CollectionViewSource.GetDefaultView(ImportTable);
            ImportTableView.Filter = new Predicate<object>(Filter);

            _ImportService = new ImportService();

            LoadExcelCommand = new RelayCommand(ImportExcel);
            ImportDataCommand = new RelayCommand<Window>(o => SaveExcel(o), o => true);
            DeleteRowCommand = new RelayCommand(DeleteRow);
        }

        private void ImportExcel()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            fileDialog.Multiselect = false;

            if ((bool)fileDialog.ShowDialog())
            {
                ImportTable = ExcelHelper.ReadExcel(fileDialog.FileName);
                ImportTableView = CollectionViewSource.GetDefaultView(ImportTable);
                ImportTableView.Filter = new Predicate<object>(Filter);
            }
        }

        private void SaveExcel(Window window)
        {
            DataHelper.DataTable = ImportTable;
            DataHelper.OrderList = DataHelper.GetOrderList(ImportTable);

            if (_ImportService.ImportData(ImportTable))
            {
                MessageBox.Show("Done");
            }
            window.Close();
        }

        private void DeleteRow()
        {
            foreach (var item in ImportTable.ToList())
            {
                if (item.IsChecked)
                {
                    ImportTable.Remove(item);
                }
            }
            ImportTableView = CollectionViewSource.GetDefaultView(ImportTable);
            ImportTableView.Refresh();
        }

        private void FilterData()
        {
            if (ImportTableView != null) ImportTableView.Refresh();
        }

        public bool Filter(object o)
        {
            var data = o as ImportDataModel;

            if (data != null)
            {
                if (!string.IsNullOrEmpty(FilterString))
                {
                    return data.PO_Number.Contains(FilterString, StringComparison.OrdinalIgnoreCase);
                }
                return true;
            }

            return false;
        }
    }
}
