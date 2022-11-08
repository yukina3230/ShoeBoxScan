using ShoeBoxScan.Models.Repositories.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeBoxScan.Models.Services.Login
{
    public class LoginService
    {
        private LoginRepository _LoginRepository;

        public LoginService()
        {
            _LoginRepository = new LoginRepository();
        }

        public bool Login(string userId, string password)
        {
            if (userId == "16235" && password == "16235")
            {
                return true;
            }

            return false;
        }
    }
}
