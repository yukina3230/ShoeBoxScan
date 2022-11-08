using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoeBoxScan.Models.Helpers;
using ShoeBoxScan.Models.Services.Login;
using ShoeBoxScan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShoeBoxScan.ViewModels.Login
{
    public class LoginViewModel : ObservableObject
    {
        private string _UserId = "16235";
        public string UserId { get => _UserId; set { _UserId = value; OnPropertyChanged(); } }

        private string _Password = "16235";
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public RelayCommand<object> LoginCommand { get; }

        private LoginService _LoginService;

        public LoginViewModel()
        {
            _LoginService = new LoginService();

            LoginCommand = new RelayCommand<object>(o => Login(o), o => true);
        }

        private void Login(object o)
        {
            Window thisView = o as Window;
            if (_LoginService.Login(UserId, Password))
            {
                DataHelper.UserId = UserId;
                MainView mainView = new MainView();
                mainView.Show();
                thisView.Close();
            }
        }
    }
}
