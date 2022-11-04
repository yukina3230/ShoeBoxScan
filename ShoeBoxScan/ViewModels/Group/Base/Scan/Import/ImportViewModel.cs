using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using ShoeBoxScan.Models;
using ShoeBoxScan.Models.Helpers;
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

        public RelayCommand ImportExcelCommand { get; }
        public RelayCommand<Window> SaveExcelCommand { get; }

        public ImportViewModel()
        {
            ImportTable = new ObservableCollection<ImportDataModel>();
            ImportTableView = CollectionViewSource.GetDefaultView(ImportTable);
            ImportTableView.Filter = new Predicate<object>(Filter);

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
                ImportTableView = CollectionViewSource.GetDefaultView(ImportTable);
                ImportTableView.Filter = new Predicate<object>(Filter);
            }
        }

        private void SaveExcel(Window window)
        {
            DataHelper.DataTable = ImportTable;
            window.Close();
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
