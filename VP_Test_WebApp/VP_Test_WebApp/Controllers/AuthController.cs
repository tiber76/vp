using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using VP_Test_WebApp.Services.Interface;

namespace VP_Test_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="AuthController">
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        // true example : https://localhost:44397/api/authenticate/lebair.jeremy@gmail.com/ilovevp 
        // false example : https://localhost:44397/api/authenticate/toto/ilovevp
        [HttpGet("/api/authenticate/{email}/{password}")]
        [AllowAnonymous]
        public ActionResult<bool> Authenticate(string email, string password)
        {
            bool isAuthenticate = false;

            try
            {
                isAuthenticate = _authService.IsAuthenticate(email, password);
            }
            catch (Exception)
            {
                return StatusCode(500, "Une erreur est survenue durant l'authentification via Login et Password.");
            }

            return Ok(isAuthenticate);
        }

        // true example :  
        // false example : 
        [HttpGet("/api/confidentials/{email}")]
        [Authorize]
        public ActionResult<bool> Confidentials(string email)
        {
            bool isConfidentials = false;

            try
            {
                isConfidentials = true;
            }
            catch (Exception)
            {
                return StatusCode(500, "Une erreur est survenue durant l'authentification securise.");
            }

            return Ok(isConfidentials);
        }
    }
}
