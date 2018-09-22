using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using VP_Test_WebApp.Authentification.AWS_Tools.Signers;
using VP_Test_WebApp.Settings;

namespace VP_Test_WebApp.Authentification
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AppSettings _appSettings;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            IOptions<AppSettings> appSettings,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            this._appSettings = appSettings.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("L'en-tete Authozisation est introuvable.");

            try
            {
                // récupération du Authorization Header et ses élements
                var credentials = Request.Headers["Authorization"].ToString().Replace(" ", "").Split(',');
                var awsKey = credentials[0].Split('=')[1].Split('/')[0];
                var canonicalizedHeaderNames = credentials[1].Split('=')[1];
                var signatureStringtoSign = credentials[2].Split('=')[1];

                // Vérification de l'existance de la clé d'authorisation
                if (!awsKey.Equals(_appSettings.AwsKey))
                    return AuthenticateResult.Fail("La clé de sécurité est incorrecte.");

                // Récupération de la date utilisé par le client
                var requestDateTime = DateTime.ParseExact(Request.Headers.FirstOrDefault(a => a.Key.Equals("X-Amz-Date")).Value.FirstOrDefault(), "yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture);
                var dateTimeStamp = Request.Headers.FirstOrDefault(a => a.Key.Equals("X-Amz-Date")).Value.FirstOrDefault();
                var dateStamp = requestDateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
                // Création de l'uri via les éléments de la request
                Uri requestUri = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}");

                // Appel à la classe de création de la signature
                AWS4SignerForAuthorizationHeaderServer aWS4SignerForAuthorizationHeader = new AWS4SignerForAuthorizationHeaderServer()
                {
                    EndpointUri = requestUri,
                    HttpMethod = "GET",
                    Service = "",
                    Region = ""
                };

                var stringToSign = aWS4SignerForAuthorizationHeader.ComputeSignature(Request.Headers,
                    dateTimeStamp,
                    dateStamp,
                    requestUri,
                    Request.Method,
                    Request.QueryString.Value,
                    _appSettings.AwsValue,
                    canonicalizedHeaderNames);

                // Match des signatures
                if (stringToSign.Equals(signatureStringtoSign))
                {
                    var identity = new ClaimsIdentity(null, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Les signatures ne correspondent pas.");
                }
            }
            catch(Exception ex)
            {
                return AuthenticateResult.Fail("L'en-tete Authozisation n'est pas valide.");
            }
        }  
    }
}
