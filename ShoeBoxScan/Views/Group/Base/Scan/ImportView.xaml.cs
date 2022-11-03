using ShoeBoxScan.ViewModels.Group.Base.Scan.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShoeBoxScan.Views.Group.Base.Scan
{
    /// <summary>
    /// Interaction logic for ImportView.xaml
    /// </summary>
    public partial class ImportView : Window
    {
        private ImportViewModel _ViewModel;
        public ImportView()
        {
            InitializeComponent();
            _ViewModel = new ImportViewModel();
            DataContext = _ViewModel;
        }
    }
}
