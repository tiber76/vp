using System;
using VP_Test_WebApp.Common;
using VP_Test_WebApp.Services.Interface;

namespace Services
{
    public class AuthService : IAuthService
    {
        public bool IsAuthenticate(string login, string password)
        {
            bool isAuthenticate = false;

            try
            {
                if (login.Equals(AuthModel.email) && password.Equals(AuthModel.password)) isAuthenticate = true;
            }
            catch(Exception ex)
            {
                //log
                throw (ex);
            }

            return isAuthenticate;
        }
    }
}
