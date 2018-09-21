using System;

namespace Vp_Test_WebApp.ConfidentialTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n************************************************");
            GetConfidential.Run("api/confidentials", "lebair.jeremy@gmail.com", "123", "toto"); // true
            GetConfidential.Run("api/confidentials", "lebair.jeremy@test.com", "123", "toto"); // false
            GetConfidential.Run("api/confidentials", "lebair.jeremy@gmail.com", "123", "titi"); // false
        }
    }
}
