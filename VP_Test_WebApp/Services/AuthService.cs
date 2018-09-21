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
                if (login.Equals(AuthModel.Email) && password.Equals(AuthModel.Password)) isAuthenticate = true;
            }
            catch(Exception ex)
            {
                //todo Ajouter des log ( fichier ou Cloud ) via ex
                throw (ex);
            }

            return isAuthenticate;
        }

        public bool IsConfidential(string email)
        {
            bool isConfidential = false;

            try
            {
                if(email.Equals(AuthModel.Email)) isConfidential = true;
            }
            catch (Exception ex)
            {
                //todo Ajouter des log ( fichier ou Cloud ) via ex
                throw (ex);
            }

            return isConfidential;
        }
    }
}
