using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
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


        // var authString = new StringBuilder();
        // authString.AppendFormat("{0}-{1} ", SCHEME, ALGORITHM);
        // authString.AppendFormat("Credential={0}/{1}, ", awsAccessKey, scope);
        // authString.AppendFormat("SignedHeaders={0}, ", canonicalizedHeaderNames);
        // authString.AppendFormat("Signature={0}", signatureString);
        // Exemple
        // "SCHEME=AWS4, 
        // ALGO=HMAC-SHA256, 
        // Credential=123/20180921/us-west-2/s3/aws4_request, 
        // SignedHeaders=content-type;host;x-amz-content-sha256;x-amz-date, 
        // Signature=62895a6e71b22d5ef1d0010bfdf713ea11bee30ebe157169802c35d4b2c37dc3"

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //if (!Request.Headers.ContainsKey("Authorization"))
            //    return AuthenticateResult.Fail("L'en-tete Authozisation est introuvable.");

            try
            {
                // récupération du Authorization Header et ses élements
                //var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                //var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                //var credentials = Encoding.UTF8.GetString(credentialBytes).Split(',');
                var credentialBytes = "SCHEME = AWS4, ALGO = HMAC - SHA256, Credential = 123 / 20180921 / us - west - 2 / s3 / aws4_request, SignedHeaders = content - type; host; x - amz - content - sha256; x - amz - date, Signature = fa4422fdd6cec37f69da4e827792348c700e99f21ded80a0756c8739857f7657";
                var credentials = credentialBytes.Replace(" ", "").Split(',');

                var awsKey = credentials[2].Split('/')[0];

                if(!awsKey.Equals(_appSettings.AwsKey))
                    return AuthenticateResult.Fail("La clé de sécurité est incorrecte.");

                // Vérification de l'existance de la clé d'authorisation


                // Récupération de la valeur du secret

                // Création de la signature à partir de ces élements

                // Match des signatures

            }
            catch
            {
                return AuthenticateResult.Fail("L'en-tete Authozisation n'est pas valide.");
            }

            var identity = new ClaimsIdentity(null, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
