using Services;
using Xunit;

namespace Vp_Test_WebApp.Tests
{
    public class AuthServiceTest
    {
        // Attention, Xunit RunnerVisualStudio n'est pas encore compatible avec Net.Core > 1.0, il faut lancer les tests en mode console.

        [Fact]
        public void Get_ShouldBe_True()
        {
            AuthService authService = new AuthService();
            Assert.True(authService.IsAuthenticate("lebair.jeremy@gmail.com", "ilovevp"));
        }

        [Fact]
        public void Get_ShouldBe_False()
        {
            AuthService authService = new AuthService();
            Assert.False(authService.IsAuthenticate("lebair.jeremy@gmail.com", "ilovevpppp"));
            Assert.False(authService.IsAuthenticate("lebair.jeremy@test.com", "ilovevpppp"));
        }
    }
}
