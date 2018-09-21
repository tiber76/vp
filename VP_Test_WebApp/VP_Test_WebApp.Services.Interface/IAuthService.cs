using System;

namespace VP_Test_WebApp.Services.Interface
{
    public interface IAuthService
    {
        bool IsAuthenticate(string login, string password);
    }
}
