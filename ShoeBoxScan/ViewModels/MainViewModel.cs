using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeBoxScan.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand ImportCommand { get; }

        public RelayCommand ScanCommand { get; }

        public MainViewModel()
        {
            ImportCommand = new RelayCommand(LaunchImportView);
            ScanCommand = new RelayCommand(LaunchScanView);
        }

        private void LaunchImportView()
        {

        }

        private void LaunchScanView()
        {

        }
    }
}
