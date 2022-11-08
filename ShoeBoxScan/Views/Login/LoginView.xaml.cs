using ShoeBoxScan.ViewModels.Login;
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

namespace ShoeBoxScan.Views.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel _ViewModel;

        public LoginView()
        {
            InitializeComponent();
            _ViewModel = new LoginViewModel();
            DataContext = _ViewModel;
            passwordBox.Password = "16235";
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _ViewModel.Password = passwordBox.Password;
        }
    }
}
